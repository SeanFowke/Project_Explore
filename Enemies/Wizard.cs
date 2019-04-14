using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
	[SerializeField] float wizHealth;
	private bool isActive = false;
	private Transform pl;
	[SerializeField] float rangeX;
	[SerializeField] float rangeY;
	[SerializeField] float teleportTimerInitial;
	private float teleportTimer;
	[SerializeField] float shotTimerInitial;
	private float shotTimer;
	[SerializeField] float coolDownTimerInitial;
	private float coolDownTimer;
	private bool canTeleport = true;
	[SerializeField] GameObject projectile;
	[SerializeField] float shotSpeed;
	private bool hasShot = false;
	private Transform spawn;
	private bool hasTeleported = false;
	private bool isCoolDown = false;


	void Start()
    {
		Initialise();
		pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		health = wizHealth;
		spawn = gameObject.transform;
		teleportTimer = teleportTimerInitial;
		shotTimer = shotTimerInitial;
		coolDownTimer = coolDownTimerInitial;
	}

    protected override void Update()
    {
		base.Update();
		Teleport();
	}

	void Teleport()
	{
		if (isActive)
		{
			if (canTeleport)
			{
				sr.enabled = false;
				Vector2 dir = new Vector2(Random.Range(-rangeX + spawn.position.x, rangeX + spawn.position.x), Random.Range(-rangeY + spawn.position.y, rangeY + spawn.position.y));
				gameObject.transform.position = dir;
				Debug.Log("Teleported");
				hasTeleported = true;
			}
			if (hasTeleported)
			{
				canTeleport = false;
				sr.enabled = true;
				shotTimer -= Time.deltaTime;
				Debug.Log("Waiting");
				hasShot = false;
				
			}
			if (shotTimer <= 0 && hasShot == false)
			{
				hasTeleported = false;
				sr.enabled = true;
				GameObject thisShot = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
				Projectile shotRef = thisShot.GetComponent<Projectile>();
				Vector2 shotAt = new Vector2(pl.position.x - transform.position.x, pl.position.y - transform.position.y);
				shotAt.Normalize();
				shotRef.inputX = shotAt.x * shotSpeed;
				shotRef.inputY = shotAt.y * shotSpeed;
				hasShot = true;
				isCoolDown = true;
				Debug.Log("Shot");
			}
			if (isCoolDown)
			{
				coolDownTimer -= Time.deltaTime;
			}
			if (coolDownTimer <= 0)
			{
				sr.enabled = true;
				isCoolDown = false;
				teleportTimer = teleportTimerInitial;
				shotTimer = shotTimerInitial;
				coolDownTimer = coolDownTimerInitial;
				canTeleport = true;
				Debug.Log("Cooling Down");
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
