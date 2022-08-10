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
        //������� ���콺 �������� �����ؼ� 
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * Time.deltaTime;
        ry += mx * Time.deltaTime;
        //ȸ�������� ����ϰ� �ʹ�
        transform.eulerAngles = new Vector3(rx, ry, 0);

    }
}
