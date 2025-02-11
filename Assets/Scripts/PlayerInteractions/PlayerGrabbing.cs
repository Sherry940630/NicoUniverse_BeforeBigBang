using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{
    //註：它不會運作，但E鍵感應正常。加raycast的debug，可能是腳色面對方向的問題。

    [SerializeField] private Transform hand;
    [SerializeField] private float grabRange = 2f;
    [SerializeField] private LayerMask grabbableLayer;

    private Transform grabbedObject = null;

    private void Update()
    {
        Debug.Log("E key pressed!");
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject == null)
                TryGrabObject();
            else
                ReleaseObject();
        }
    }

    private void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, grabbableLayer))
        {
            grabbedObject = hit.transform;
            grabbedObject.SetParent(hand);
            grabbedObject.localPosition = Vector3.zero; 
            grabbedObject.localRotation = Quaternion.identity;
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = true; /* Disable physics. */
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false; /* Re-enable physics. */
            grabbedObject.SetParent(null);
            grabbedObject = null;
        }
    }
}
