﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	public string dir;
	[SerializeField] float xSpeed;
	[SerializeField] float ySpeed;
	[SerializeField] float disperseTimeInitial;
	private Rigidbody2D rb;
	private float disperseTime;
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		Move();
	}

	public void Move()
	{
		if (dir == "Right")
		{
			rb.velocity = new Vector2(xSpeed, 0);
		}
		else if (dir == "Left")
		{
			rb.velocity = new Vector2(-xSpeed, 0);
		}
		else if (dir == "Up")
		{
			rb.velocity = new Vector2(0, ySpeed);
		}
		else if (dir == "Down")
		{
			rb.velocity = new Vector2(0, -ySpeed);
		}
		else if (dir != "Right" && dir != "Right" && dir != "Up" && dir != "Down")
		{
			throw new UnityException("No Direction Given to Fireball");
		}
			
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}



}
