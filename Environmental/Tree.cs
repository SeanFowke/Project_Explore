using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Wood
{
    private bool isDead;
    [SerializeField] GameObject log;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FallDown();
    }

    void FallDown()
    {

        if (isDead == true)
        {

            Instantiate(log, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setDead(bool dead_)
    {
        isDead = dead_;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fireball"))
        {
			Destroy(col.gameObject);
            isDead = true;
        }
    }
}
