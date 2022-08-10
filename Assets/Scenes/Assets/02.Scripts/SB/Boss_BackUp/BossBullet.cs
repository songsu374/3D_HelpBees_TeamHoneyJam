using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public static BossBullet instance;
    private void Awake()
    {
        instance = this;
    }

    public int damage;
    public bool isMislee;
    public bool isRock; //ÅºÇÇÃ³¸®

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3.0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isMislee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
