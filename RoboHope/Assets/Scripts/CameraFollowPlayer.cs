using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

	public GameObject target;
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 position = transform.position;
		position.y = target.transform.position.y;
		transform.position = position;

	}
}
