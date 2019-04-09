using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    private Rigidbody2D rb;
	[SerializeField] GameObject fireBall;
	private bool isRight;
	private bool isUp;
	[SerializeField] float attackTimerInitial;
	private float attackTimer;
	private bool isAttack;
	private SpriteRenderer sr;
	private Animator anim;
	private bool canFire = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		isAttack = false;
		attackTimer = attackTimerInitial;
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
		ShootFireBall();
	}

    void MovePlayer()
    {

        if (Input.GetAxisRaw("Horizontal") != 0 )
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, 0);
        }
        else if (Input.GetAxisRaw("Vertical") != 0)
        {
            rb.velocity = new Vector2(0, Input.GetAxis("Vertical") * playerSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            rb.velocity = new Vector2(0.0f, 0.0f);
        }

		if (rb.velocity.x > 0)
		{
			isRight = true;
		}
		if (rb.velocity.x < 0)
		{
			isRight = false;
		}
		if (rb.velocity.y > 0)
		{
			isUp = true;
		}
		if (rb.velocity.y < 0)
		{
			isUp = false;
		}
		if (rb.velocity.y == 0 && rb.velocity.x == 0)
		{
			isUp = false;
			isRight = true;
		}
    }


	void ShootFireBall()
	{
		if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.dir = "Right";
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Horizontal") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.dir = "Left";
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") > 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.dir = "Up";
			canFire = false;
		}
		else if (Input.GetButtonDown("Fire1") && Input.GetAxisRaw("Vertical") < 0 && canFire == true)
		{
			GameObject thisFire = (GameObject)Instantiate(fireBall, transform.position, Quaternion.identity);
			Fireball fireRef = thisFire.GetComponent<Fireball>();
			fireRef.dir = "Down";
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
	

   private void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.CompareTag("River"))
		{
			col.isTrigger = false;
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
}
