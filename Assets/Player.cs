using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	#region Variables
	Transform m_transform;
	Vector3 ms;
	
	[SerializeField]
	[Range(0.8f, 0.99f)]
	float speed = 0.99f;
	#endregion
	
	#region Methods
	void Start () 
	{
		m_transform = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		ms = Input.mousePosition;
		ms = Camera.main.ScreenToWorldPoint(ms);

		Vector3 newPos =  (1-speed)* ms + speed*m_transform.position; 
		newPos.z = 1;
		m_transform.position = newPos;

	}
	#endregion
}
