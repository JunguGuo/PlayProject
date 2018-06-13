using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using com.ootii.Messages;
public class Cross : MonoBehaviour
{

    #region Variables
    public float rotSpeed;
    #endregion

    #region Methods
    bool isFreezed = false;
    void Start()
    {
        MessageDispatcher.AddListener("FREEZE", OnFreeze);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFreezed)
            transform.Rotate(0, 0, Time.deltaTime * rotSpeed);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            LeanTween.scale(this.gameObject, new Vector3(0.0f, 0.0f, 0.0f), 1.0f).setEase(LeanTweenType.easeOutSine);
    }

    void OnFreeze(IMessage rMessage)
    {
        isFreezed = true;
        Timer timer = this.gameObject.GetComponent<Timer>();
        //fix this: 如果同时触发多个不同类型的timer可能会出错。。注意
        if (timer == null)
            this.gameObject.AddComponent<Timer>().Setup(TIMERTYPE.FREEZE, OnUnFreeze);
        else
            timer.Refresh(TIMERTYPE.FREEZE);


    }
    void OnUnFreeze()
    {
        //Debug.Log((string)rMessage.Data);
        isFreezed = false;

    }

    void OnDestroy()
    {
        MessageDispatcher.RemoveListener("FREEZE", OnFreeze);
    }

    #endregion


}
