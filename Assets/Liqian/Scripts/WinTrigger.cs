using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Canvas winCanvas; 
    public string targetTag = "Item";

    void Start()
    {
        if (winCanvas == null)
        {
            Debug.LogError("Win Canvas is not assigned to the WinTrigger script!");
        }
        else
        {
            winCanvas.gameObject.SetActive(false);
        }

        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No Collider found on the destination plane!");
        }
        else
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        if (winCanvas != null)
        {
            winCanvas.gameObject.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }
}