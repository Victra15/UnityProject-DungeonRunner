using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{



	public float speed;
	private float x;
	public float PontoDeDestino;
	public float PontoOriginal;
	private float t;
	public float speed_obj;
	private float startTime;
	private bool ismove = false;


	// Use this for initialization
	public void MoveAll()
	{
		//PontoOriginal = transform.position.x;
		Debug.Log("start");
		ismove = true;
		startTime = Time.time;
		t = startTime;
		speed_obj = speed;
		
	}

	// Update is called once per frame
	void Update()
	{
		if (ismove)
		{
			Debug.Log(t);
			x = transform.position.x;
			x += speed_obj * Time.deltaTime;
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			t = Time.time - startTime;

			if (t >= 3) //이동시간 3초
			{
				speed_obj = 0;
				ismove = false;
			}

			if (x <= PontoDeDestino)
			{

				Debug.Log("hhhh");
				x = PontoOriginal;
				transform.position = new Vector3(x, transform.position.y, transform.position.z);

			}


		}
	}
}
