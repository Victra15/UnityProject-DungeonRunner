using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
	public float speed;
	public float Destination;
	public float OriginalPosition;
	public float curr_speed;
	private float x;

    // Use this for initialization
    // Update is called once per frame
	public void move()
	{ 
		curr_speed = speed;
	}

	public void stop()
    {
		curr_speed = 0;
    }

    void Start()
    {
		curr_speed = 0;  
    }

    void Update()
	{
		x = transform.position.x;
		x += curr_speed * Time.deltaTime;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);

		if (x <= Destination)
		{
			x = OriginalPosition;
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}
	}
}
