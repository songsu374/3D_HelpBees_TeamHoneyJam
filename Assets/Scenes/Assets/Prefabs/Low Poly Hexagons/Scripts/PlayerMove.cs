using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //������ ���� ���� �ʱ�ȭ
    public float gravity = -9.81f;
    float yVeloctiy;
    public float jumpPower = 10;
    public CharacterController Cc;

    public Rigidbody rb;
    void Start()
    {        
    }

    public float speed = 5;
    void Update()
    {
        //�߷��� �ݿ��ؾ��Ѵ�
        yVeloctiy += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        //1. ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2.�յ��¿�� ������ �����
        Vector3 dir = transform.right * h + transform.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        //dir �� ũ�⸦ 1�� �������Ѵ�
        dir.Normalize();

        //3. �� �������� �̵��ϰ�ʹ�
        rb.MovePosition(transform.position.normalized + dir * speed * Time.deltaTime);
    }
}
