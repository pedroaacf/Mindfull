using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhoneRaycast : MonoBehaviour
{
     
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private String excludeLayerName = null;
    [FormerlySerializedAs("openDoorKey")] [SerializeField] private KeyCode phoneKey = KeyCode.Mouse0;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive, doOnce;
    private const string interactableTag = "InteractiveObject";
    private PhoneController rayCastedObj;

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        int mask = 1<<LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;
        if (Physics.Raycast(transform.position, forward, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    rayCastedObj = hit.collider.gameObject.GetComponent<PhoneController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(phoneKey))
                {
                    rayCastedObj.RingStop();
                }
            }
        }
        else if (isCrosshairActive)
        {
            CrosshairChange(false);
            doOnce = false;
        }

    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}
