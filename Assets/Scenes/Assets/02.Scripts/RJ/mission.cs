using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mission : MonoBehaviour
{
    public GameObject Panel;
    public GameObject mPanel_1;
    public GameObject mPanel_2;
    public GameObject mPanel_3;
    Player enterPlayer; //플레이어 접근 
    public Animator anim;
    bool isEnter;

    public enum State { Spawn, Idle, Castspell };
    State state;

    public AudioSource missionNPC;

    private void Start()
    {
        Panel.SetActive(false);
        mPanel_1.SetActive(false);
        mPanel_2.SetActive(false);
        mPanel_3.SetActive(false);
        anim.SetTrigger("Idle");

    }

    public void Update()
    {
        switch (state)
        {
            case State.Spawn:
                MissionSpawn(enterPlayer);
                break;
            case State.Idle:
                MissionIdle();
                break;
            case State.Castspell:
                MissionCastspell();
                break;
        }

    }

    public void MissionSpawn(Player player)
    {
        //입장 시, 플레이어 정보를 저장하면서 UI 위치로 이동
        if (enterPlayer = player)
        {
            if (isEnter == false)
            {
                CameraManager.instance.MissionCamera();
                anim.SetTrigger("Spawn");
                Panel.SetActive(true);
                mPanel_1.SetActive(true);
                isEnter = true;
                state = State.Idle;

                missionNPC.Play();
            }
        }
    }

    public void MissionIdle() 
    {
        {
            if (mPanel_1.activeSelf == true || mPanel_2.activeSelf == true || mPanel_3.activeSelf == true)
            {
            anim.SetTrigger("Idle");
            }
            else 
            { 
            state = State.Castspell;
            }
        }
    }

    public void MissionCastspell() 
    {
        Invoke("SetHide", 3);
        anim.SetTrigger("Castspell");
    }

    void SetHide()
    {
        CameraManager.instance.FirstCamera();
        gameObject.SetActive(false);
    }
}
