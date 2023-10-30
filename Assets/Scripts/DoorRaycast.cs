using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private String excludeLayerName = null;
    [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
    [SerializeField] private Image crosshair = null;
    private bool isCrosshairActive, doOnce;
    private const string interactableTag = "InteractiveObject";
    private DoorController rayCastedObj;

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
                    rayCastedObj = hit.collider.gameObject.GetComponent<DoorController>();
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(openDoorKey))
                {
                    rayCastedObj.PlayAnimation();
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
