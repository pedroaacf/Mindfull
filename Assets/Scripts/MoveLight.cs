using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour
{
    public Transform lightPosition;
    
    void Update()
    {
        transform.position = lightPosition.position;
    }
}
