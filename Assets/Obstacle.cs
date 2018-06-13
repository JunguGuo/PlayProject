using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
public class Obstacle : MonoBehaviour
{

    #region Variables
    #endregion
    // public float randomRange;
    Transform m_transform;
    public Vector3 bgSize;
    public float kickbackPower = 10.0f;
    bool isFreezed = false;
    public float directionCorrectionStrength;//control how storng the corrction is
    public float heightScale = 1.0F;
    public float xScale = 1.0F;
    float randomXOffset;
    float randomYOffset;
    public GameObject freezePs;
    #region Methods
    void Start()
    {
        m_transform = GetComponent<Transform>();
        randomXOffset = Random.value * 10000;
        //randomXOffset = (randomXOffset - (int)randomXOffset)*1000;
        //Debug.Log(randomXOffset);
        randomYOffset = Random.value * 10000;
        //randomYOffset = (randomYOffset - (int)randomYOffset)*1000;
        //Debug.Log(randomYOffset);
        MessageDispatcher.AddListener("FREEZE", OnFreeze);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFreezed)
            Move();


    }

    void Move()
    {
        float x = Mathf.PerlinNoise((Time.time + randomXOffset) * xScale, 0) - 0.465f;
        float y = Mathf.PerlinNoise((Time.time + randomYOffset) * xScale, 0) - 0.465f;
        //Vector3 target = new Vector3(Random.value * randomRange, Random.value * randomRange,0);
        //Debug.Log("y:"+y);
        Vector3 mov = new Vector3(x, y * 1.6f, 0); //more horizontal moving spaces

        mov = (mov + directionCorrection() * directionCorrectionStrength) * heightScale;
        //Debug.Log(mov);
        // directionCalculate(ref mov);
        m_transform.position += mov;
    }

    Vector3 directionCorrection()
    {
        //Vector3 dir = new Vector3(1,1,0);

        float amt = 0.3f; //how near the border is the correction start to work, the bigger the farther away from the border 
        Vector3 relPos = m_transform.position + bgSize / 2;


        Vector3 dir = new Vector3(0, 0, 0);

        if (relPos.x < bgSize.x * amt)
        {

            dir.x = Mathf.Lerp(1.0f, 0.0f, relPos.x / (bgSize.x * amt));
            //Debug.Log("near left: "+ dir.x);
        }
        if (relPos.x > bgSize.x * (1 - amt))
        {

            dir.x = Mathf.Lerp(0.0f, -1.0f, (bgSize.x - relPos.x) / (bgSize.x * amt));
            //Debug.Log("near right: "+ dir.x);
        }
        if (relPos.y < bgSize.y * amt)
        {

            dir.y = Mathf.Lerp(1.0f, 0.0f, relPos.y / (bgSize.y * amt));
            //Debug.Log("near bottom: "+ dir.y);
        }
        if (relPos.y > bgSize.y * (1 - amt))
        {

            dir.y = Mathf.Lerp(0.0f, -1.0f, (bgSize.y - relPos.y) / (bgSize.y * amt));
            //Debug.Log("near top: "+ dir.y);
        }
        return dir;

    }

    void OnFreeze(IMessage rMessage)
    {
        //Debug.Log((string)rMessage.Data);
        isFreezed = true;
        Timer timer = this.gameObject.GetComponent<Timer>();
        //fix this: 如果同时触发多个不同类型的timer可能会出错。。注意
        if (timer == null)
        {
            this.gameObject.AddComponent<Timer>().Setup(TIMERTYPE.FREEZE, OnUnFreeze);
            freezePs.SetActive(true);
        }
        else
            timer.Refresh(TIMERTYPE.FREEZE);
        //timer.timesUp.AddListener(OnUnFreeze);


    }

    void OnUnFreeze()
    {
        //Debug.Log((string)rMessage.Data);
        isFreezed = false;
        freezePs.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //rb.AddRelativeForce(kickbackPower, transform.position, 2.0f);
                rb.AddForce((rb.transform.position - transform.position).normalized * kickbackPower);
                coll.gameObject.SendMessage("ApplyLife", -1);
            }

        }
    }

    void OnDestroy()
    {
        MessageDispatcher.RemoveListener("FREEZE", OnFreeze);
    }



    #endregion
}
