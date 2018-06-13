using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum TIMERTYPE
{

    FREEZE,
    SPEEDUP,
    ThirdSample,

}
// [System.Serializable]
//  public struct TimerData {
//      //TYPE type;
//      TYPE type;
// 	 float time;

//  }
public class Timer : MonoBehaviour
{

    #region Variables
    public TIMERTYPE type;
    float time;
    #endregion
    [SerializeField]
    float counter = 0.0f;

    public UnityEvent timesUp;
    #region Methods


    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= time)
        {
            timesUp.Invoke();
            Destroy(this);
        }

    }

    void Awake()
    {
        if (timesUp == null)
            timesUp = new UnityEvent();
    }

    public void Setup(TIMERTYPE type, UnityAction action)
    {
        switch (type)
        {
            case TIMERTYPE.FREEZE:
                this.type = type;
                time = 10.0f;
                timesUp.AddListener(action);
                break;
            case TIMERTYPE.SPEEDUP:
                this.type = type;
                time = 7.0f;
                timesUp.AddListener(action);
                break;
        }

    }
    public void Refresh(TIMERTYPE type)
    {
        switch (type)
        {
            case TIMERTYPE.FREEZE:
                counter = 0.0f;
                break;
            case TIMERTYPE.SPEEDUP:
                //speedup可以累积
                counter -= 7.0f;
                break;
        }

    }
    #endregion
}
