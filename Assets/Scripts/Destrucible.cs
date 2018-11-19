using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucible : MonoBehaviour
{
    public GameObject DestroyedVariant;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
        {
            Instantiate(DestroyedVariant, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
