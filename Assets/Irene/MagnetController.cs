using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public Vector3 startPosition;
    public float speed = 1.0f;

    void OnCollisionEnter(Collision collision)
    {
        transform.position = startPosition;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation
        }
    }


    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float moveDirection = Input.GetAxis("Horizontal"); // Returns -1 for left, 1 for right

        transform.Translate(Vector3.forward * moveDirection * speed * Time.deltaTime);
    }
}
