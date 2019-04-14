using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGrave : MonoBehaviour
{
	[SerializeField] GameObject key;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Fireball"))
		{
			Instantiate(key, transform.position, Quaternion.identity);
			Destroy(col.gameObject);
			Destroy(gameObject);
		}
	}
}
