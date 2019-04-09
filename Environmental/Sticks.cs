using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticks : MonoBehaviour
{
    [SerializeField] GameObject campFire;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fire"))
        {
            Instantiate(campFire, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
