using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{

    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FreezeRotation();
        StopWall();

    }

    private void StopWall()
    {
      //  Debug.DrawRay(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    private void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    private void Move()
    {
     
       
    }
}
