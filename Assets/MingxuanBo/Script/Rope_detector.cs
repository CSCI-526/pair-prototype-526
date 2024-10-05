using System.Collections;
using UnityEngine;

public class Rope_detector : MonoBehaviour
{
    public Transform cylinder; 
    private GameObject firstDetectedItem = null; 
    private GameObject currentAttachedItem = null; 
    private bool isItemAttached = false; 
    private float cylinderRadius; 


    void Start()
    {

        CapsuleCollider capsuleCollider = cylinder.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            cylinderRadius = capsuleCollider.radius * cylinder.localScale.x; 
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && firstDetectedItem != null && !isItemAttached)
        {
            StartCoroutine(MoveItemToBottom(firstDetectedItem));
        }


        if (Input.GetKeyDown(KeyCode.O) && isItemAttached && currentAttachedItem != null)
        {
            ReleaseItem();
        }
    }


    private Vector3 GetCylinderBottomPosition()
    {

        float fullHeight = cylinder.localScale.y;
        return cylinder.position - new Vector3(0, fullHeight / 2, 0); 
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item") && firstDetectedItem == null && !isItemAttached)
        {
            firstDetectedItem = other.gameObject; 
            Debug.Log("检测到的物体：" + firstDetectedItem.name); 
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject == firstDetectedItem && !isItemAttached)
        {
            Debug.Log("物体离开 trigger：" + firstDetectedItem.name); 
            firstDetectedItem = null; 
        }
    }


    private IEnumerator MoveItemToBottom(GameObject item)
    {
        float speed = 5f; 
        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        if (itemRb != null)
        {
            itemRb.detectCollisions = false; 
            itemRb.useGravity = false;
            itemRb.isKinematic = true; 
        }


        Collider itemCollider = item.GetComponent<Collider>();
        float itemHeight = 0f;
        if (itemCollider != null)
        {
            itemHeight = itemCollider.bounds.size.y;
        }

        while (item != null)
        {

            Vector3 bottomPosition = GetCylinderBottomPosition();

            Vector3 targetPosition = bottomPosition - new Vector3(0, itemHeight  + 0.05f, 0); // 修正：确保物体底部对齐到圆柱体底部


            item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, speed * Time.deltaTime);


            if (Vector3.Distance(item.transform.position, targetPosition) <= 0.1f)
                break;

            yield return null;
        }

        item.transform.SetParent(cylinder);
        currentAttachedItem = item; 
        isItemAttached = true; 
        firstDetectedItem = null; 
    }

    private void ReleaseItem()
    {
        if (currentAttachedItem != null)
        {
            Rigidbody itemRb = currentAttachedItem.GetComponent<Rigidbody>();

            if (itemRb != null)
            {
                itemRb.detectCollisions = true; 
                itemRb.useGravity = true; 
                itemRb.isKinematic = false; 
            }

            currentAttachedItem.transform.SetParent(null);
            currentAttachedItem = null; 
            isItemAttached = false; 
            Debug.Log("物体已释放");
        }
    }
}
