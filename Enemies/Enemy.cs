using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed;
	protected Rigidbody2D rb;
	protected SpriteRenderer sr;
	protected AudioSource au;
	protected AudioManager ado;
	protected float health;
	private bool damaged = false;
	private float timer = 0;
	private float totalTime = 0;
	protected float damage;
	void Start()
    {
		
    }


	protected virtual void Update()
    {
		DamageIndicator();

	}

	protected void Initialise()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		au = GetComponent<AudioSource>();
		ado = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	protected void DamageIndicator()
	{
		if (damaged)
		{
			timer += Time.deltaTime;
			totalTime += Time.deltaTime;
			sr.enabled = true;
			
			if (timer > 0.01)
			{
				timer -= 0.01f;
				sr.enabled = false;
			}
			if (totalTime > 0.1)
			{
				totalTime = 0;
				timer = 0;
				sr.enabled = true;
				damaged = false;

			}
		}

		
	}

	public float GetDamage()
	{
		return damage;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Fireball"))
		{
			if (health > 0)
			{
				damaged = true;
				health--;
				Destroy(col.gameObject);
				au.PlayOneShot(ado.hitMob);
			}
			else if(health <= 0)
			{
				au.PlayOneShot(ado.deathMob);
				Destroy(col.gameObject);
				Destroy(gameObject);

			}
		}
	}


}
