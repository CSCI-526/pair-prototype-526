using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCollision : MonoBehaviour
{
    public Vector3 jointStartPosition;
    public Vector3 ballStartPosition;
    public Vector3 cubeStartPosition;

    public GameObject ball;
    public GameObject magnetCollider;
    public GameObject cube;

    public float stopTime = 1.0f; 

    public void StopMovement(GameObject otherObject)
    {
        Rigidbody rb = otherObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    IEnumerator OnCollisionEnter()  
    {
        cube.transform.SetParent(null);  

        transform.position = jointStartPosition;
        transform.rotation = Quaternion.identity;  
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = Quaternion.identity;  
        cube.transform.position = cubeStartPosition;
        cube.transform.rotation = Quaternion.identity;  

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;  // Freeze all movement and rotation

            rb.velocity = Vector3.zero;  // Stop all movement
            rb.angularVelocity = Vector3.zero;  // Stop rotation

            StopMovement(magnetCollider);

            yield return new WaitForSeconds(stopTime);

            rb.constraints = RigidbodyConstraints.None;  // Unfreeze all movement and rotation
        }
    }


    /*
    void OnCollisionEnter(Collision collision)
    {
        cube.transform.SetParent(null);  

        transform.position = jointStartPosition;
        ball.transform.position = ballStartPosition;
        ball.transform.rotation = Quaternion.identity;  
        cube.transform.position = cubeStartPosition;
        cube.transform.rotation = Quaternion.identity;  

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  
            rb.angularVelocity = Vector3.zero;  

            rb.constraints = RigidbodyConstraints.FreezeAll;  

            yield return new WaitForSeconds(stopTime);

            rb.constraints = RigidbodyConstraints.None;  
        }

        
        StopMovement(magnetCollider);  
    }
    */



    void Start()
    {
        jointStartPosition = transform.position;
        ballStartPosition = ball.transform.position;
        cubeStartPosition = cube.transform.position;
    }

    void Update()
    {

    }
}
