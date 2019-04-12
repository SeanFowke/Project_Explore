using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Wood
{

	[SerializeField] float spreadTimeInitial;
	private float spreadTime;
	private bool hasSpawnedFire;
	private float destroyTime;
	private bool isSpread;
	private bool isInRadius;
	private int spawnChance;
	private Rigidbody2D rb;
	[SerializeField] GameObject sticks;
	[SerializeField] GameObject fire;
	void Start()
	{
		hasSpawnedFire = false;
		isSpread = false;
		isInRadius = false;
		rb = gameObject.GetComponent<Rigidbody2D>();
		spreadTime = spreadTimeInitial;


	}

	// Update is called once per frame
	void Update()
	{
		Spread();
		if (hasSpawnedFire == true)
		{
			Destroy(this.gameObject, destroyTime);
		}
	}

	void Spread()
	{
		if (isInRadius == true)
		{
			spawnChance = Random.Range(0, 500);
		}
		if (isInRadius == true && spawnChance == 1)
		{

			isSpread = true;

		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Fireball"))
		{
			
			var fireComponent = fire.GetComponent<Fire>();
			Instantiate(fire, gameObject.transform.position, Quaternion.identity, gameObject.transform);
			hasSpawnedFire = true;
			destroyTime = fireComponent.burnTime();
		}
		if (col.CompareTag("Fire") && isInRadius == false)
		{
			isInRadius = true;
		}
		if (col.CompareTag("Sword"))
		{
			Instantiate(sticks, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("River"))
		{
			var river = col.gameObject.GetComponent<River>();

			if (river.getDirection() == direction.right)
			{
				rb.AddForce(new Vector2(river.getSpeed(), 0));
				if (gameObject.transform.position.y >= col.gameObject.transform.position.y - 0.3f || gameObject.transform.position.y <= col.gameObject.transform.position.y + 0.3f)
				{
					Vector2 pos = new Vector2(gameObject.transform.position.x, col.transform.position.y);
					gameObject.transform.position = pos;
				}
			}
		}
		if (col.CompareTag("Fire") && hasSpawnedFire == false && isSpread == true)
		{
			var fire = col.gameObject;
			var fireComponent = col.gameObject.GetComponent<Fire>();
			Instantiate(fire, gameObject.transform.position, Quaternion.identity, gameObject.transform);
			hasSpawnedFire = true;
			destroyTime = fireComponent.burnTime();
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Fire") && isInRadius == false)
		{
			isInRadius = false;
			spreadTime = spreadTimeInitial;
		}
	}

}
