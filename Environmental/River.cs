using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum direction
{
	right,
	up,
	down,
	left
}

public class River : MonoBehaviour
{
	[SerializeField] private direction dir;
	[SerializeField] float speed;



	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public direction getDirection()
	{

		return dir;

	}
	public float getSpeed()
	{
		return speed;
	}
}
