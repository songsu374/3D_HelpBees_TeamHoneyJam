using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkybox : MonoBehaviour
{
    public Material aa;

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void ontriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("����");
            GameObject cam = GameObject.Find("Main Camera");
            cam.AddComponent<Skybox>().material = aa;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("��ī�̹ڽ� ����");
            GameObject cam = GameObject.Find("Main Camera");
            cam.AddComponent<Skybox>().material = aa;
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    
}
