using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionBackup : MonoBehaviour
{

    public GameObject currentObject; 
    public GameObject parent; 
    private SpringJoint springJoint; 
    public GameObject connectedGameObject; 

    private Rigidbody originalConnectedBody; 

    void Start()
    {
        springJoint = GetComponent<SpringJoint>();

        if (springJoint != null && connectedGameObject != null)
        {
            originalConnectedBody = connectedGameObject.GetComponent<Rigidbody>(); 
            springJoint.connectedBody = originalConnectedBody; 
        }
    }

    // 检测与 "tube" 的碰撞事件
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tube")) 
        {
            Debug.Log("Tube Collision Detected, Returning to checkpoint");
            TeleportToCheckpoint();
        }
    }

    // 检测 savePoint 的触发器事件
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("savePoint")) 
        {
            Debug.Log("Save Point Reached, updating checkpoint.");
            currentObject = other.gameObject; 
        }
    }

    // 将 version2（parent）传送到 currentObject（存档点）并保持圆柱体直立
    private void TeleportToCheckpoint()
    {
        if (springJoint != null)
        {
            springJoint.connectedBody = null;
        }

        Rigidbody[] rigidbodies = parent.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; 
        }

        parent.transform.position = currentObject.transform.position;
        parent.transform.rotation = currentObject.transform.rotation;


        //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); 
        transform.rotation = Quaternion.Euler(0, 0, 0); 

        if (originalConnectedBody != null)
        {
            originalConnectedBody.transform.rotation = Quaternion.identity; 
            Debug.Log("connectedRigiBody's rotation has been reset to (0, 0, 0)");
        }


        Physics.SyncTransforms();

        Debug.Log("Version2 and all attached objects have returned to the checkpoint and stopped.");


        StartCoroutine(StopSwingingAndReconnectSpringJoint());
    }

    private IEnumerator StopSwingingAndReconnectSpringJoint()
    {

        yield return new WaitForSeconds(1.0f);


        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; 
            //rb.angularVelocity = Vector3.zero; 
        }


        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = false;


            //springJoint.spring = 100f;  // spring 参数
            //springJoint.damper = 19f;  // 减小阻尼

            springJoint.connectedBody = originalConnectedBody; 
            Debug.Log("Spring Joint reconnected");
        }


        yield return new WaitForSeconds(0.5f);
        if (rb != null)
        {
            rb.AddTorque(-rb.angularVelocity * rb.mass, ForceMode.VelocityChange); 
        }


        yield return new WaitForSeconds(0.5f);
        if (springJoint != null && originalConnectedBody != null)
        {
            originalConnectedBody.isKinematic = true; 
        }
    }
}
