using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.1f, 1.0f, -10f);
    public float dampTime = 0.3f;
    public Vector3 velocity = Vector3.zero;
    private float targetPosition;

    
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

   
    void Update()
    {
        Vector3 temp = target.position;
        targetPosition = temp.x += offset.x;
        Vector3 destination = new Vector3(targetPosition, offset.y, offset.z);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
    }
}
