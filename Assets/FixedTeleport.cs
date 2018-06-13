using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTeleport : MonoBehaviour
{

    #region Variables

    #endregion

    #region Methods
    void Start()
    {
        if (GameManager.MODE == GameManager.HOLE)
        {
            for (int i = 0; i < transform.GetChildCount(); i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
