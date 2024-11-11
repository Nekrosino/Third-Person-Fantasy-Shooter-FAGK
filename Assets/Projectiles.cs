using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject,lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        Destroy(gameObject);
    }

}


