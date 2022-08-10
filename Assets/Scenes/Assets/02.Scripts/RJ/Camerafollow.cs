using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
    }

    public void Update()
    {
            transform.position = target.position + offset;
        
    }

}
