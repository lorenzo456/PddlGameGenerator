using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    float lifeTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifeTime = .2f;
    }
    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        rb.AddForce(transform.forward * 2,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            collision.gameObject.GetComponent<IKillable>().Damaged(10);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
