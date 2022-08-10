using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera;
    public Camera UICamera;
    public Camera BossCamera;
   
    public static CameraManager instance;
    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        FirstCamera();
    }

    public void FirstCamera()
    {
        MainCamera.enabled = true;
        UICamera.enabled = false;
        BossCamera.enabled = false;
    }

    public void MissionCamera()
    {
        MainCamera.enabled = false;
        UICamera.enabled = true;
        BossCamera.enabled = false;

    }

    public void ChangePointCamera()
    {
        MainCamera.enabled = false;
        UICamera.enabled = false;
       
        BossCamera.enabled = false;

    }

    public void BossStartCamera()
    {
        MainCamera.enabled = false;
        UICamera.enabled = false;
        
        BossCamera.enabled = true;

    }
}
