using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
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
        float moveHorizontal = Input.GetAxis("Horizontal");  // A/D or Left/Right arrows for X-axis movement

        float moveVertical = Input.GetAxis("Vertical");  // W/S or Up/Down arrows for Z-axis movement

        float moveY = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f; // Move down
        }

        Vector3 movement = new Vector3(moveHorizontal, moveY, -moveVertical);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

    }
}
