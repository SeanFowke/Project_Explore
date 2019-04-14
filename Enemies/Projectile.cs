using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Rigidbody2D rb;
	public float shotSpeed;
	public string dir;
	public float inputX;
	public float inputY;

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
			rb.velocity = new Vector2(shotSpeed, 0);
		}
		else if (dir == "Left")
		{
			rb.velocity = new Vector2(-shotSpeed, 0);
		}
		else if (dir == "Up")
		{
			rb.velocity = new Vector2(0, shotSpeed);
		}
		else if (dir == "Down")
		{
			rb.velocity = new Vector2(0, -shotSpeed);
		}
		else if (dir != "Right" && dir != "Right" && dir != "Up" && dir != "Down")
		{
			rb.velocity = new Vector2(inputX, inputY);
		}

	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
