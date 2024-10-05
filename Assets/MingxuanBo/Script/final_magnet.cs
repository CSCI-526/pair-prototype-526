using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_magnet : MonoBehaviour
{
    public LayerMask jointLayerPart;
    public int playerLayerPart;

    public string targetTag;
    //public GameObject joint;

    private Renderer objectRenderer;
    private Rigidbody objectRigidbody;

    private Rigidbody attachedRigidbody = null;
    private Transform attachedObject = null;
    private Collider attachedCollider = null;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(targetTag))
        {

            attachedRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = true; 
                attachedRigidbody.useGravity = false; 
            }

            attachedCollider = other.gameObject.GetComponent<Collider>();
            if (attachedCollider != null)
            {
                attachedCollider.enabled = false; 
            }


            attachedObject = other.transform;

            other.transform.SetParent(transform);
            //other.transform.SetParent(joint.transform);

            other.gameObject.layer = playerLayerPart;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position - other.transform.position.normalized, out hit, Mathf.Infinity, jointLayerPart))
            {
                other.transform.forward = hit.normal;

                other.transform.position = hit.point;

                // Offset the position slightly along the forward direction
                other.transform.position += other.transform.forward * other.transform.localScale.z * 0.5f;

            }

            Collider parentCollider = GetComponent<Collider>();
            if (parentCollider != null && attachedCollider != null)
            {
                Physics.IgnoreCollision(parentCollider, attachedCollider);
            }


            StartCoroutine(EnableColliderAfterDelay(1f));




        }
    }


    private IEnumerator EnableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (attachedCollider != null)
        {
            attachedCollider.enabled = true;
        }
    }



    private void Update()
    {
        if (attachedObject != null && Input.GetKey(KeyCode.Space))
        {
            ReleaseAttachedObject();
        }
    }

    private void ReleaseAttachedObject()
    {
        if (attachedObject != null)
        {
            Collider parentCollider = GetComponent<Collider>();
            if (parentCollider != null && attachedCollider != null)
            {
                Physics.IgnoreCollision(parentCollider, attachedCollider, false);  // »Ö¸´Åö×²
            }

            attachedObject.SetParent(null);

            if (attachedRigidbody != null)
            {
                attachedRigidbody.isKinematic = false;  
                attachedRigidbody.useGravity = true; 
            }

            if (attachedCollider != null)
            {
                attachedCollider.enabled = true;  // ÆôÓÃÅö×²
            }


            attachedRigidbody = null;
            attachedCollider = null;
            attachedObject = null;
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
