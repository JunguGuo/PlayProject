using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
// using com.ootii.Messages
public class Teleporter : MonoBehaviour
{

    #region Variables
    #endregion
    public GameObject destination;
    public ParticleSystem portPs;
    public Transform areaTL;
    public Transform areaBR;
    public bool fixedPos = false;
    #region Methods
    void Start()
    {

    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    void OnEnable()
    {
        if (!fixedPos)
            transform.position = GetRandomPos();
    }
    Vector3 GetRandomPos()
    {
        float x = Random.Range(areaTL.position.x, areaBR.position.x);
        float y = Random.Range(areaTL.position.y, areaBR.position.y);
        return new Vector3(x, y, 0.0f);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("0");
        if (coll.tag == "Player")
        {
            //Debug.Log("1");
            //coll.gameObject.SendMessage("ApplyDamage", 10);
            coll.gameObject.GetComponent<PhysicPlayer>().enabled = false;
            coll.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //LeanTween.move(coll.gameObject, )
            LeanTween.move(coll.gameObject, transform.position, 1.0f).setEase(LeanTweenType.linear);
            LeanTween.value(gameObject, ((float val) =>
            {
                coll.gameObject.GetComponentInChildren<TrailRenderer>().startWidth = val;
            }), 0.6f, 0f, 1f).setEase(LeanTweenType.linear);


            LeanTween.scale(coll.gameObject, new Vector3(0.0f, 0.0f, 0.0f), 1.0f).setEase(LeanTweenType.linear).setOnComplete(() =>
            {
                // teleport
                destination.transform.position = GetRandomPos();
                destination.SetActive(true);
                portPs.Stop();
                coll.gameObject.transform.position = destination.transform.position;
                LeanTween.scale(coll.gameObject, new Vector3(1.0f, 1.0f, 1.0f), 1.0f).setEase(LeanTweenType.linear).setOnComplete(() =>
                {
                    coll.gameObject.GetComponent<PhysicPlayer>().enabled = true;
                    coll.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                    portPs.Play();
                    this.gameObject.SetActive(false);
                }).setDelay(1.5f);

                LeanTween.value(gameObject, ((float val) =>
                {
                    coll.gameObject.GetComponentInChildren<TrailRenderer>().startWidth = val;
                }), 0.0f, 0.6f, 1f).setEase(LeanTweenType.linear).setDelay(1.5f);
            });

        }


    }

    #endregion
}
