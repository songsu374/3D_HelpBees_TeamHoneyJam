using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //점프를 위한 변수 초기화
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
        //중력을 반영해야한다
        yVeloctiy += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        //1. 사용자의 입력에 따라서
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //2.앞뒤좌우로 방향을 만들고
        Vector3 dir = transform.right * h + transform.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        //dir 의 크기를 1로 만들어야한다
        dir.Normalize();

        //3. 그 방향으로 이동하고싶다
        rb.MovePosition(transform.position.normalized + dir * speed * Time.deltaTime);
    }
}
