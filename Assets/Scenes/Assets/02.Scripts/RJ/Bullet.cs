using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public float BulletLifeTime = 3f;
    float curBulletTime;

    [Header("보스패턴")]
    public int damage;
    public bool isMelee;
    public bool isRock;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
        curBulletTime += Time.deltaTime;

        if (curBulletTime > BulletLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRock && collision.gameObject.tag == "Floor")
        {

            Destroy(gameObject, 3);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);

        }
    }
}
