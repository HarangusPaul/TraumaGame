using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    [SerializeField] private float rotationSpeed = 50.0f; // Adjusted rotation speed

    // Look timer variables
    private bool isLookingAtBibelou = false;
    private float lookTimer = 0f;
    private float requiredLookTime = 2f; // Adjust as needed

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
        }

        // Rotate the held object up and down smoothly while 'R' key is held down.
        if (Input.GetKey(KeyCode.R))
        {
            RotateObjectUp();
        }
        if (Input.GetKey(KeyCode.T))
        {
            RotateObjectDown();
        }

        // Look at Bibelou functionality
        if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                if (hit.transform.CompareTag("Bibelou"))
                {
                    LookAtBibelou(hit.transform.gameObject);
                }
                else
                {
                    StopLookingAtBibelou();
                }
            }
            else
            {
                StopLookingAtBibelou();
            }
        }
        else
        {
            StopLookingAtBibelou();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void RotateObjectUp()
    {
        // Rotate the held object around its local X-axis with smooth rotation.
        float rotationAmount = rotationSpeed * Time.deltaTime;
        heldObj.transform.Rotate(Vector3.right, rotationAmount);
    }

    void RotateObjectDown()
    {
        // Rotate the held object around its local X-axis with smooth rotation.
        float rotationAmount = rotationSpeed * Time.deltaTime;
        heldObj.transform.Rotate(Vector3.left, rotationAmount);
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    // Look at Bibelou functionality
    void LookAtBibelou(GameObject bibelouObject)
    {
        if (GameManager.instance != null)
        {
            if (!isLookingAtBibelou)
            {
                lookTimer += Time.deltaTime;
                if (lookTimer >= requiredLookTime)
                {
                    GameManager.instance.AddScore(10); // Adjust points as needed
                    lookTimer = 0f; // Reset the timer
                }
            }
            else
            {
                isLookingAtBibelou = true;
                lookTimer = 0f; // Reset the timer
            }
        }
        else
        {
            Debug.LogError("GameManager.instance is null. Make sure GameManager object is present in the scene.");
        }
    }


    void StopLookingAtBibelou()
    {
        isLookingAtBibelou = false;
        lookTimer = 0f; // Reset the timer
    }
}
