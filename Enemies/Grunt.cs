using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
	private float switchDir;
	private float dir;
	[SerializeField] float shotTimerInitial;
	private float shotTimer;
	[SerializeField] float shotSpeed;
	private bool hasFired = false;
	private bool isActive = false;
	[SerializeField] float gruntSpeed;
	[SerializeField] GameObject projectile;
	[SerializeField] float patrolZoneX;
	[SerializeField] float patrolZoneY;
	[SerializeField] float gruntHealth;
	[SerializeField] float gruntDamage;
	private Vector2 spawn;
	private Transform pl;
    void Start()
    {
		Initialise();
		dir = Random.Range(1, 4);
		speed = gruntSpeed;
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		shotTimer = shotTimerInitial;
		spawn = transform.position;
		health = gruntHealth;
		damage = gruntDamage;
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    protected override void Update()
    {
		base.Update();
		Patrol();

	}

	void Patrol()
	{
		if (isActive)
		{
			switchDir = Random.Range(1, 50);
			
			if (switchDir == 1)
			{
				dir = Random.Range(1, 4);
			}
			if (gameObject.transform.position.x > spawn.x + patrolZoneX)
			{
				dir = 2;
			}
			if (gameObject.transform.position.x < spawn.x - patrolZoneX)
			{
				dir = 1;
			}
			if (gameObject.transform.position.y > spawn.y + patrolZoneY)
			{
				dir = 4;
			}
			if (gameObject.transform.position.y < spawn.y - patrolZoneY)
			{
				dir = 3;
			}
			if (gameObject.transform.position.y + 0.2 >= pl.transform.position.y && gameObject.transform.position.y - 0.2 <= pl.transform.position.y)
			{
				float check = gameObject.transform.position.x - pl.transform.position.x;
				
				if (check <= 0 && hasFired == false)
				{
					GameObject thisShot = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
					Projectile shotRef = thisShot.GetComponent<Projectile>();
					shotRef.shotSpeed = shotSpeed;
					shotRef.dir = "Right";
					hasFired = true;
				}
				else if (check > 0 && hasFired == false)
				{
					GameObject thisShot = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
					Projectile shotRef = thisShot.GetComponent<Projectile>();
					shotRef.shotSpeed = shotSpeed;
					shotRef.dir = "Left";
					hasFired = true;
				}
			}
			if (hasFired == true)
			{
				shotTimer -= Time.deltaTime;
			}
			if (shotTimer < 0)
			{
				shotTimer = shotTimerInitial;
				hasFired = false;
			}

			// right
			if (dir == 1)
			{
				rb.velocity = new Vector2(speed, 0);
			}
			// left 
			else if (dir == 2)
			{
				rb.velocity = new Vector2(-speed, 0);
			}
			// up
			else if (dir == 3)
			{
				rb.velocity = new Vector2(0, speed);
			}
			// down
			else if (dir == 4)
			{
				rb.velocity = new Vector2(0, -speed);
			}
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
