using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;
public class PhysicPlayer : MonoBehaviour
{

    #region Variables
    public float thrust;
    Rigidbody2D rb;
    Transform m_transform;
    Vector3 ms;
    public Color hurtColor;
    public Color normalColor;
    public float life = 10.0f;
    public float maxSpeed = 2.0f;
    float cachedMaxSpeed;
    public float forceSphereRadius = 10.0f;
    public SpriteRenderer sr;
    public Text life_Text;
    public Text result_Text;
    bool isSpeedUp = false;
    public ParticleSystem speedUpPs;
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
        m_transform = this.GetComponent<Transform>();

        ApplyLife(0);
        MessageDispatcher.AddListener("SPEEDUP", OnSPEEDUP);
    }

    // Update is called once per frame
    void Update()
    {

        float a = Mathf.Lerp(0.0f, 1.0f, life / 4.0f);

        Color c = sr.color;
        c.a = a;
        sr.color = c;

        //test
        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(transform.up * 1000);

    }

    void FixedUpdate()
    {
        ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);
        ms.z = 0;
        Vector3 dir = ms - m_transform.position;
        //thrust = dir*0.1f;

        Debug.DrawLine(ms, -dir.normalized * forceSphereRadius + ms, Color.red);

        // if(dir.sqrMagnitude > forceSphereRadius*forceSphereRadius){
        // 	rb.AddForce(dir*thrust);
        // 	float s = Mathf.Clamp(rb.velocity.magnitude,0.0f,maxSpeed);
        // 	rb.velocity = rb.velocity.normalized*s;
        // }
        // else{
        // 	//Vector3.Lerp(Vector3.zero, dir, dir.magnitude/)
        // }


        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        Vector2 v = Vector2.Lerp(Vector2.zero, dir.normalized * maxSpeed, dir.magnitude / forceSphereRadius);
        rb.velocity = v;
        //rb.AddForce(v);


    }

    void ApplyLife(int num)
    {
        life += num;
        if (num < 0)
        {
            LeanTween.cancel(this.gameObject);
            LeanTween.value(gameObject, updateValueExampleCallback, hurtColor, normalColor, .7f).setEase(LeanTweenType.linear);
        }

        life_Text.text = "Life: " + life;

        if (life <= 0)
        {
            result_Text.text = "GAME OVER";

        }



    }
    void updateValueExampleCallback(Color val)
    {
        sr.color = val;
    }


    void OnSPEEDUP(IMessage rMessage)
    {
        Debug.Log("Speedup");
        cachedMaxSpeed = maxSpeed; maxSpeed *= 1.7f;
        Timer timer = this.gameObject.GetComponent<Timer>();
        //fix this: 如果同时触发多个不同类型的timer可能会出错。。注意
        if (timer == null)
        {
            this.gameObject.AddComponent<Timer>().Setup(TIMERTYPE.SPEEDUP, OnUnSPEEDUP);
            speedUpPs.Play();
        }
        else
            timer.Refresh(TIMERTYPE.SPEEDUP);
        //timer.timesUp.AddListener(OnUnFreeze);


    }

    void OnUnSPEEDUP()
    {
        //Debug.Log((string)rMessage.Data);
        // isSpeedUp = false;
        maxSpeed = cachedMaxSpeed;
        speedUpPs.Stop();
    }

    void OnDestroy()
    {
        MessageDispatcher.RemoveListener("SPEEDUP", OnSPEEDUP);
    }


    //   void OnCollisionEnter2D(Collision2D collision)
    // {
    // 	Debug.Log("111");
    // 	rb.force
    // }
    #endregion
}
