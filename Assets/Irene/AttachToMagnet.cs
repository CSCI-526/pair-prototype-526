using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToMagnet : MonoBehaviour
{
    public LayerMask jointLayer;
    public int playerLayer;

    public string tagToAdd;
    //public GameObject joint;

    private Renderer objectRenderer;
    private Rigidbody objectRigidbody;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(tagToAdd))
        {
            other.transform.SetParent(transform);
            //other.transform.SetParent(joint.transform);
            
            other.gameObject.layer = playerLayer;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - other.transform.position.normalized, out hit, Mathf.Infinity, jointLayer))
            {
                other.transform.forward = hit.normal;

                other.transform.position = hit.point;
                
                other.transform.position += other.transform.forward * other.transform.localScale.z * 0.5f;

            }
            
        }
    }


    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        
        objectRigidbody = GetComponent<Rigidbody>();

        if (objectRenderer != null)
        {
            //objectRenderer.enabled = false;
        }

        if (objectRigidbody != null)
        {

        }
    }
}
