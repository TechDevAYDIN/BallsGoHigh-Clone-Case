
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollbg : MonoBehaviour
{
	public float ParallaxFactor = 0f;

	Transform theCamera;
	public Vector3 theDimension;
	Vector3 theStartPosition;

	void Start()
	{
		
		theCamera = Camera.main.transform;
		theStartPosition = transform.position;

		theDimension = GetComponent<Renderer>().bounds.size;
	}

	void Update()
	{
		Vector3 newPos = theCamera.position * ParallaxFactor;                   // Calculate the position of the object
		newPos.z += theStartPosition.z;                       // Force Z-axis to zero, since we're in 2D
		newPos.x = -75;
		newPos.y = -20;
		transform.position = newPos;

		EndlessRepeater();
	}

	void EndlessRepeater()
	{
		if (theCamera.position.z > (transform.position.z + theDimension.z/2))
		{
			theStartPosition.z += theDimension.z * 2;
		}
	}
}