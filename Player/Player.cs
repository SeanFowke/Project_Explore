using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	// speed
	[SerializeField] float playerSpeed;
	// rigidbody ref
	private Rigidbody2D rb;
	// fireball gameobject
	[SerializeField] GameObject fireBall;
	// variables that will control animation
	private bool isRight;
	private bool isLeft;
	private bool isUp;
	private bool isDown;
	private bool isStop;
	// reference to sprite renderer and anim controller
	private SpriteRenderer sr;
	private Animator anim;
	// bool that controls if you can fire 
	private bool canFire = true;
	// health
	[SerializeField] float totalHealth;
	private float currentHealth;
	// Ui bars
	private Slider healthBar;
	private Slider coolDownBar;
	// how many keys does the player have
	private int keyCount = 0;
	// where did the player spawns
	private Vector2 spawn;
	// timers
	[SerializeField] float attackTimerInitial;
	private float attackTimer;
	[SerializeField] float shotNumInitial;
	private float shotNumCurrent;
	private float indicatorTime;
	private bool iFrame;
	[SerializeField] float iFrameTimerInitial;
	private float iFrameTimer;
	private float flickerTimer = 0;


	void Start()
	{
		// get references and set default values
		rb = GetComponent<Rigidbody2D>();
		attackTimer = attackTimerInitial;
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		currentHealth = totalHealth;
		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
		coolDownBar = GameObject.Find("CoolDown").GetComponent<Slider>();
		spawn.x = gameObject.transform.position.x;
		spawn.y = gameObject.transform.position.y;
		shotNumCurrent = shotNumInitial;
		iFrameTimer = iFrameTimerInitial;
	}

	// Update is called once per frame
	void Update()
	{
		MovePlayer();
		ShootFireBall();
		HandleAnimation();
		HandleUI();
		HandleHealth();
		HandleIFrame();
	}
	#region Input
	void MovePlayer()
	{
		if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
		{
			rb.velocity = new Vector2(0.0f, 0.0f);

		}
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			// get a direction vector normalize it and multiply it by our player speed
			// this gives instanatneous speed
			Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			dir.Normalize();
			rb.velocity = dir * playerSpeed;
		}

	}


	void ShootFireBall()
	{
		// normalized direction vector
		Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			shotNumCurrent--;
			// get a reference to the fireball pass in the direction and multiply it by it's x and y speed then count down the shot counter
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			shotNumCurrent--;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			shotNumCurrent--;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.inputX = dir.x * fireRef.xSpeed;
			fireRef.inputY = dir.y * fireRef.ySpeed;
			shotNumCurrent--;
			
		}
		if (shotNumCurrent <= 0)
		{
			// if youve run out of shots start counting down
			canFire = false;
		}
		if (canFire == false)
		{
			// indicator time is going to decrement untill zero to trigger the reset. indicator time will be what's visible on the UI
			indicatorTime += Time.deltaTime;
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0)
			{
				attackTimer = attackTimerInitial;
				shotNumCurrent = shotNumInitial;
				indicatorTime = 0;
				canFire = true;
			}
		}
	}
	#endregion

	#region Animation
	void HandleAnimation()
	{
		// get inputs set the booleans and then set the animators booleans to correspond
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
		// set our UI Slider values
		healthBar.value = currentHealth / totalHealth;
		if (canFire)
		{
			// shows  your current number of shots
			coolDownBar.value = shotNumCurrent / shotNumInitial;
		}
		else if (!canFire)
		{
			// shows the cool down bar filling up
			coolDownBar.value = indicatorTime / attackTimerInitial;
		}
		
	}

	void TakeDamage(float damage)
	{
		currentHealth -= damage;
	}

	void HandleHealth()
	{
		if (currentHealth <= 0)
		{
			// reset the player's position
			transform.position = spawn;
			currentHealth = totalHealth;
		}


	}
	#endregion
	#region IFrame
	void HandleIFrame()
	{
		if (iFrame)
		{
			flickerTimer += Time.deltaTime;
			iFrameTimer -= Time.deltaTime;
			sr.enabled = true;
			if (flickerTimer > 0.01)
			{
				sr.enabled = false;
				flickerTimer = 0;
			}
			if (iFrameTimer <= 0)
			{
				sr.enabled = true;
				iFrameTimer = iFrameTimerInitial;
				flickerTimer = 0;
				iFrame = false;
			}
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
		if (col.gameObject.CompareTag("Projectile") && !iFrame)
		{
			TakeDamage(1.0f);
			iFrame = true;
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
		if (col.CompareTag("Potion"))
		{
			shotNumInitial++;
			coolDownBar.gameObject.transform.localScale += new Vector3(0.2f, 0.0f, 0.0f);
			coolDownBar.gameObject.transform.position += new Vector3(30f, 0.0f, 0.0f);
			shotNumCurrent = shotNumInitial;
			Destroy(col.gameObject);
		}

	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Enemy") && !iFrame)
		{
			var enemy = col.gameObject.GetComponent<Enemy>();
			TakeDamage(enemy.GetDamage());
			iFrame = true;
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
