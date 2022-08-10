using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx;
    float ry;
    public float rotSpeed = 200;

    void Start()
    {
    }

    void Update()
    {
        //사용자의 마우스 움직임을 누적해서 
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * Time.deltaTime;
        ry += mx * Time.deltaTime;
        //회전값으로 사용하고 싶다
        transform.eulerAngles = new Vector3(rx, ry, 0);

    }
}
