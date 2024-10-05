using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controller : MonoBehaviour
{
    public float moveSpeed;  
    public float verticalSpeed;  
    private bool canMoveUp = true;  

    public Canvas hitCanvas;  

    private void Start()
    {
        hitCanvas.gameObject.SetActive(false);

    }


    void Update()
    {

        float horizontal = 0f;
        float forward = 0f;


        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }


        if (Input.GetKey(KeyCode.W))
        {
            forward = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forward = -1;
        }

        float rise = 0f;
        if (canMoveUp)  // 只有在允许的情况下才能上升
        {
            if (Input.GetKey(KeyCode.UpArrow))  
            {
                rise = verticalSpeed;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))  
        {
            rise = -verticalSpeed;
        }


        Vector3 movement = new Vector3(horizontal, rise, forward) * moveSpeed * Time.deltaTime;


        transform.Translate(movement);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            hitCanvas.gameObject.SetActive(true);

            canMoveUp = false;  
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TestCube"))
        {
            hitCanvas.gameObject.SetActive(false);

            canMoveUp = true;  
        }
    }
}
