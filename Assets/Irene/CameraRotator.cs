using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), speed * Time.deltaTime);
        }
        else if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), speed * Time.deltaTime);

        }

        // DEBUG: Rotate camera around object automatically
        //transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
