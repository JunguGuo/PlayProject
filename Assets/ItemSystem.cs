using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    #region Variables
    public Transform areaTL;
    public Transform areaBR;
    public GameObject food;
    public bool haveFood = false;
    public GameObject freezer;
    public bool haveFreezer = false;
    public GameObject life;
    public bool haveLife = false;
    public float foodInteral;
    float foodCounter;
    public float freezerInteral;
    float freezerCounter;
    public float lifeInteral;
    float lifeCounter;
    #endregion

    #region Methods
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foodCounter += Time.deltaTime;
        if (foodCounter > foodInteral)
        {
            //reset timer
            foodCounter = 0.0f;
            if (haveFood)
                Instantiate(food, GetRandomPos(), Quaternion.identity);
        }

        freezerCounter += Time.deltaTime;
        if (freezerCounter > freezerInteral)
        {
            //reset timer
            freezerCounter = 0.0f;
            if (haveFreezer)
                Instantiate(freezer, GetRandomPos(), Quaternion.identity);
        }

        lifeCounter += Time.deltaTime;
        if (lifeCounter > lifeInteral)
        {
            //reset timer
            lifeCounter = 0.0f;
            if (haveLife)
                Instantiate(life, GetRandomPos(), Quaternion.identity);
        }

    }


    Vector3 GetRandomPos()
    {
        float x = Random.Range(areaTL.position.x, areaBR.position.x);
        float y = Random.Range(areaTL.position.y, areaBR.position.y);
        return new Vector3(x, y, 0.0f);
    }
    #endregion
}
