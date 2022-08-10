using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instacne;

    private void Awake()
    {
        instacne = this;
    }

    public Camera cam;
    Vector3 cameraPos;

    [SerializeField] [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField] [Range(0.1f, 1f)] float duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shake()
    {
        cameraPos = cam.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", duration);
    
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY= Random.value * shakeRange * 2 - shakeRange;
        Vector3 camerPos = cam.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        cam.transform.position = cameraPos;

    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        cam.transform.position = cameraPos;
    
    }
}
