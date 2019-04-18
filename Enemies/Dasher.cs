using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : Enemy
{
	private Transform pl;
	private bool isActive;
	[SerializeField] float dashDistance;
	[SerializeField] float dashTimeInitial;
	private float dashTime;
	[SerializeField] float dashCoolDownInitial;
	private float dashCoolDown;
	private bool canDash = true;
	private bool dashCool = false;
	[SerializeField] float dasherHealth;
	[SerializeField] float dasherDamage;
	void Start()
    {
		Initialise();
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		dashTime = dashTimeInitial;
		dashCoolDown = dashCoolDownInitial;
		health = dasherHealth;
		damage = dasherDamage;
	}

    // Update is called once per frame
    protected override void Update()
    {
		base.Update();
		DashToPlayer();

	}

	void DashToPlayer()
	{
		if (isActive)
		{
			if (canDash)
			{
				Vector2 dir = new Vector2(pl.position.x - transform.position.x, pl.position.y - transform.position.y);
				dir.Normalize();
				rb.velocity = dir * dashDistance;
				dashTime -= Time.deltaTime;
				dashCool = false;

			}
			if (dashTime <= 0)
			{
				canDash = false;
				
				dashCoolDown -= Time.deltaTime;
				dashCool = true;
				rb.velocity = new Vector2(0.0f, 0.0f);
			}
			if (dashCoolDown <= 0 && dashCool)
			{
				dashTime = dashTimeInitial;
				dashCoolDown = dashCoolDownInitial;
				canDash = true;
				rb.velocity = new Vector2(0.0f, 0.0f);
			}
		}
		if (rb.velocity.x > 0)
		{
			sr.flipX = false;
		}
		else if (rb.velocity.x < 0)
		{
			sr.flipX = true;
		}
	}

	private void OnBecameVisible()
	{
		isActive = true;
	}
	private void OnBecameInvisible()
	{
		isActive = false;
	}
}
