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
            Debug.Log("색상");
            GameObject cam = GameObject.Find("Main Camera");
            cam.AddComponent<Skybox>().material = aa;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("스카이박스 변경");
            GameObject cam = GameObject.Find("Main Camera");
            cam.AddComponent<Skybox>().material = aa;
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    
}
