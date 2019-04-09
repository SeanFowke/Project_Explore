using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Fire : MonoBehaviour
{
    // disipation time on weak fuel IE grass
    [SerializeField] float disipationTime;
    // disipation time on a strong fuel IE logs
    [SerializeField] float fuelTime;
    private bool isFueled;



    void Update()
    {
        Disipate();
    }

    void Disipate()
    {
        if (isFueled == true)
        {
            Destroy(gameObject, fuelTime);
        }
        else if (isFueled == false)
        {
            Destroy(gameObject, disipationTime);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Log"))
        {
            isFueled = true;
        }
        else
        {
            isFueled = false;
        }
    }

    public float burnTime()
    {
        
            return disipationTime;
        
    }
}
