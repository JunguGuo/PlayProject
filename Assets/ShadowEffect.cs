using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
public class ShadowEffect : MonoBehaviour {

	#region Variables
	#endregion
	public float LoopTime = 1.0f;
	#region Methods
	void Start () 
	{
		LeanTween.scale( this.gameObject, new Vector3(1.7f, 1.7f, 1.7f), LoopTime).setLoopPingPong(-1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	#endregion
}
