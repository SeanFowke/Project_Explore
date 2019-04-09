using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed;
	protected Rigidbody2D rb;
    void Start()
    {
		
    }

    
    void Update()
    {
        
    }

	protected void Initialise()
	{
		rb = GetComponent<Rigidbody2D>();
	}

}
