using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using com.ootii.Messages;
public class Freezer : MonoBehaviour {

	#region Variables
	public float rotSpeed;
	#endregion
	public GameObject ps;
	#region Methods
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(0,0,Time.deltaTime*rotSpeed);	
	}


    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
			LeanTween.scale( this.gameObject, new Vector3(0.0f, 0.0f, 0.0f), 1.0f).setEase(LeanTweenType.easeInBounce).destroyOnComplete = true;
			MessageDispatcher.SendMessage("FREEZE",0);
			Instantiate(ps,transform.position,transform.rotation);
			GetComponent<Collider2D>().enabled = false;
			Debug.Log("FREEZE!");
		}
	}

	#endregion

	
}
