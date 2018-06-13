using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// public enum HOMETYPE
// {
//     SUN,
//     MER
// }
public class Home : MonoBehaviour
{


    public string homeType;
    public Text resultText;
    Animator animator;
    bool charging = false;
    bool moving = false;
    public float moveSpeed = 1.0f;
    #region Methods
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.MODE == GameManager.MOVE)
            moving = true;

        if (moving && homeType == "LEFT")
        {
            float y = Mathf.Sin(moveSpeed * Time.time) * 5.35f;
            this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
        }
        if (moving && homeType == "RIGHT")
        {
            float y = Mathf.Cos(moveSpeed * Time.time) * 5.35f;
            this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("DDD");
        if (other.tag == "Player")
        {
            charging = true;
            animator.SetBool("charging", charging);

            string s = homeType + " WIN !";
            resultText.text = s;

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            charging = false;
            animator.SetBool("charging", charging);

            string s = homeType + " WIN !";
            resultText.text = s;
        }

    }
    #endregion
}
