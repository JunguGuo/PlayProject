using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporManager : MonoBehaviour
{

    #region Variables
    #endregion

    #region Methods

    public GameObject port;
    public float minInterval;
    public float maxInterval;
    float timer = 10.0f;
    float counter = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timer)
        {
            //reset timer
            counter = 0.0f;
            timer = Random.Range(minInterval, maxInterval);
            LaunchEvent();
        }

    }

    void LaunchEvent()
    {
        port.SetActive(true);
    }
    #endregion
}
