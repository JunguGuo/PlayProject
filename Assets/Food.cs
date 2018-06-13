using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using com.ootii.Messages;
public enum FOODTYPE
{
    SPEEDUP,
    LIFE
}
public class Food : MonoBehaviour
{
    public FOODTYPE foodType;
    #region Variables
    public float rotSpeed;
    #endregion
    Transform target;
    public float chasingSpeed = 0.7f;
    #region Methods
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotSpeed);
        if (target != null)
            transform.position = chasingSpeed * target.position + transform.position * (1 - chasingSpeed);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<Collider2D>().enabled = false;
            LeanTween.scale(this.gameObject, new Vector3(0.0f, 0.0f, 0.0f), 1.0f).setEase(LeanTweenType.easeOutSine).destroyOnComplete = true;
            target = other.transform;

            if (foodType == FOODTYPE.SPEEDUP)
                MessageDispatcher.SendMessage("SPEEDUP", 0);
            else if (foodType == FOODTYPE.LIFE)
                other.gameObject.SendMessage("ApplyLife", 1);
        }
    }

    #endregion


}
