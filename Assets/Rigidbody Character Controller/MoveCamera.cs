using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// moves the camera to the specified position each frame
/// </summary>
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
