using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField] float playerSpeed;
	private Rigidbody2D rb;
	[SerializeField] GameObject fireBall;
	private bool isRight;
	private bool isLeft;
	private bool isUp;
	private bool isDown;
	private bool isStop;
	[SerializeField] float attackTimerInitial;
	private float attackTimer;
	private SpriteRenderer sr;
	private Animator anim;
	private bool canFire = true;
	[SerializeField] float totalHealth;
	private float currentHealth;
	private Slider healthBar;
	private Slider coolDownBar;
	private int keyCount = 0;
	private Transform spawn;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		attackTimer = attackTimerInitial;
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		currentHealth = totalHealth;
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
		coolDownBar = GameObject.Find("CoolDown").GetComponent<Slider>();
		spawn.position = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		MovePlayer();
		ShootFireBall();
		HandleAnimation();
		HandleUI();
		HandleHealth();
	}
	#region Input
	void MovePlayer()
	{
		if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
		{
			rb.velocity = new Vector2(0.0f, 0.0f);

		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			rb.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, rb.velocity.y);
		}
		if (Input.GetAxis("Vertical") != 0)
		{
			rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * playerSpeed);
		}



	}


	void ShootFireBall()
	{
		Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;

			canFire = false;
		}

		if (canFire == false)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer < 0)
			{
				attackTimer = attackTimerInitial;
				canFire = true;
			}
		}
	}
	#endregion

	#region Animation
	void HandleAnimation()
	{
		if (rb.velocity.x > 0)
		{
			isRight = true;
			isLeft = false;
			isUp = false;
			isDown = false;
			isStop = false;
		}
		else if (rb.velocity.x < 0)
		{
			isRight = false;
			isLeft = true;
			isUp = false;
			isDown = false;
			isStop = false;
		}
		else if (rb.velocity.y > 0)
		{
			isRight = false;
			isLeft = false;
			isUp = true;
			isDown = false;
			isStop = false;
		}
		else if (rb.velocity.y < 0)
		{
			isRight = false;
			isLeft = false;
			isUp = false;
			isDown = true;
			isStop = false;
		}
		if (rb.velocity.y == 0 && rb.velocity.x == 0)
		{
			isRight = false;
			isLeft = false;
			isUp = false;
			isDown = false;
			isStop = true;
		}

		if (isUp)
		{
			anim.SetBool("WalkUp", true);
			anim.SetBool("WalkDown", false);
			anim.SetBool("WalkLeft", false);
			anim.SetBool("WalkRight", false);
			anim.SetBool("IdleUp", false);
			anim.SetBool("IdleDown", false);
			anim.SetBool("IdleLeft", false);
			anim.SetBool("IdleRight", false);
		}
		else if (isDown)
		{
			anim.SetBool("WalkUp", false);
			anim.SetBool("WalkDown", true);
			anim.SetBool("WalkLeft", false);
			anim.SetBool("WalkRight", false);
			anim.SetBool("IdleUp", false);
			anim.SetBool("IdleDown", false);
			anim.SetBool("IdleLeft", false);
			anim.SetBool("IdleRight", false);
		}
		else if (isLeft)
		{
			anim.SetBool("WalkUp", false);
			anim.SetBool("WalkDown", false);
			anim.SetBool("WalkLeft", true);
			anim.SetBool("WalkRight", false);
			anim.SetBool("IdleUp", false);
			anim.SetBool("IdleDown", false);
			anim.SetBool("IdleLeft", false);
			anim.SetBool("IdleRight", false);
		}
		else if (isRight)
		{
			anim.SetBool("WalkUp", false);
			anim.SetBool("WalkDown", false);
			anim.SetBool("WalkLeft", false);
			anim.SetBool("WalkRight", true);
			anim.SetBool("IdleUp", false);
			anim.SetBool("IdleDown", false);
			anim.SetBool("IdleLeft", false);
			anim.SetBool("IdleRight", false);
		}
		else if (isStop)
		{
			anim.SetBool("WalkUp", false);
			anim.SetBool("WalkDown", false);
			anim.SetBool("WalkLeft", false);
			anim.SetBool("WalkRight", false);
			anim.SetBool("IdleUp", true);
			anim.SetBool("IdleDown", true);
			anim.SetBool("IdleLeft", true);
			anim.SetBool("IdleRight", true);
		}
	}
	#endregion
	#region UI/Health
	void HandleUI()
	{
		healthBar.value = currentHealth / totalHealth;
		coolDownBar.value = attackTimer / attackTimerInitial;
	}

	void TakeDamage(float damage)
	{
		currentHealth -= damage;

	}

	void HandleHealth()
	{
		if (currentHealth <= 0)
		{
			transform.position = spawn.position;
			currentHealth = totalHealth;
		}


	}
	#endregion

	#region Collisions
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("River"))
		{
			col.isTrigger = false;
		}
		if (col.gameObject.CompareTag("Projectile"))
		{
			TakeDamage(1.0f);
		}
		if (col.CompareTag("Heart") && currentHealth != totalHealth)
		{
			currentHealth = totalHealth;
			Destroy(col.gameObject);
		}
		if (col.CompareTag("Key"))
		{
			keyCount++;
			Destroy(col.gameObject);
		}

	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy"))
		{
			var enemy = col.gameObject.GetComponent<Enemy>();
			TakeDamage(enemy.GetDamage());

		}
		if (col.gameObject.CompareTag("Door") && keyCount >= 3)
		{
			SceneManager.LoadScene("Win");
		}
	}

	private void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("River"))
		{
			var coli = col.gameObject.GetComponent<BoxCollider2D>();
			coli.isTrigger = true;
		}
	}
	#endregion
}
