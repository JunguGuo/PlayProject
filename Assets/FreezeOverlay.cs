using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using com.ootii.Messages;
public class FreezeOverlay : MonoBehaviour
{

    #region Variables
    #endregion
    // public float minScale;
    // public float animTime;
    SpriteRenderer sr;
    #region Methods
    bool isFreezed = false;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MessageDispatcher.AddListener("FREEZE", OnFreeze);

    }

    void updateValueExampleCallback(float val, float ratio)
    {
        // Debug.Log("tweened value:" + val + " percent complete:" + ratio / 100);
        Color c = sr.color;
        c.a = val;
        sr.color = c;

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


            sr.enabled = true;
            // freeze action
            LeanTween.value(gameObject, updateValueExampleCallback, 0.3f, 1.0f, 1.5f).setEase(LeanTweenType.linear).setLoopPingPong(-1);

        }
        else
            timer.Refresh(TIMERTYPE.FREEZE);
        //timer.timesUp.AddListener(OnUnFreeze);


    }

    void OnUnFreeze()
    {
        //Debug.Log((string)rMessage.Data);
        isFreezed = false;

        LeanTween.cancel(this.gameObject);
        LeanTween.value(gameObject, updateValueExampleCallback, sr.color.a, 0.0f, .5f).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            sr.enabled = false;
        });
    }

    void OnDestroy()
    {
        MessageDispatcher.RemoveListener("FREEZE", OnFreeze);
    }
    #endregion
}
