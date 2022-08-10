using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMoving : MonoBehaviour
{
    [SerializeField] [Header("�̵��Ÿ�")] [Range(1f, 100f)] float dist = 7f;
    [SerializeField] [Header("�̵��ӵ�")] [Range(1f, 50f)] float speed = 5f;
    [SerializeField] [Header("�ĵ���")] [Range(1f, 40f)] float frequency = 20f;
    [SerializeField] [Header("�ĵ�����")] [Range(0.2f, 10f)] float waveHeight = 0.5f;

    Vector3 pos, localScale;
    bool dirRight = true;

    void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (transform.position.x > dist)
        {
            dirRight = false;
        }
        else if (transform.position.x < -dist)
        {
            dirRight = true;
        }
        if (dirRight)
        {
            GoRight();
        }
        else
        {
            GoLeft();
        }

    }

    void GoRight()
    {
        localScale.x = 1;
        transform.transform.localScale = localScale;
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;
    }

    void GoLeft()
    {
        localScale.x = 1;
        transform.transform.localScale = localScale;
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
