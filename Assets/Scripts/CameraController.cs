using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float trackSpeed = 0.124f;
    public Vector3 offset;


    // Set target
    
    // Track target
    void LateUpdate()
    {
        Vector3 posiciondeseada = target.position + offset;
        Vector3 posicionsuavidad = Vector3.Lerp(transform.position, posiciondeseada, trackSpeed);
        transform.position = posicionsuavidad;
        
    }
}