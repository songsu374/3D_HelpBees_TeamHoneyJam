using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{

    // ????
    public enum Level
    {
        Lv1,
        Lv2,
        Lv3,
        LvBoss,
    }
    public Level level;

    // ????
    enum State
    {
        MakeEnemy,
        CheckEnemy,
    }
    State state;

    public List<Monster> list = new List<Monster>();

    //?????? ????
    public static MonsterManager instance = null;
    private void Awake()
    {
        instance = this;
    }
    public Region rgion = new Region();

    public GameObject terrain;

    public GameObject LV01Stairs;
    public GameObject LV02Stairs;
    public GameObject LV03Stairs;

    public GameObject monsterOrigin;
    // ???? ???????? ?????? ?????? ??????(????????????)
    // ???? ?????? ???? ???????? ????????.(????????????)

    public GameObject boss;

    public GameObject Lv1ClearUI;
    public GameObject Lv2ClearUI;
    public GameObject Lv3ClearUI;

    public GameObject Slider;
    public GameObject SliderBossHPUI;

    void Start()
    {
        LV01Stairs.gameObject.SetActive(false);
        LV02Stairs.gameObject.SetActive(false);
        LV03Stairs.gameObject.SetActive(false);

        rgion.Init(terrain.transform);

        boss.SetActive(false);
        //progressBar.maxValue = 15;
        Lv1ClearUI.SetActive(false);
        Lv2ClearUI.SetActive(false);
        Lv3ClearUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.MakeEnemy:
                UpdateMakeEnemy();
                break;
            case State.CheckEnemy:
                UpdateCheckEnemy();
                break;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            BossStart();
            transform.position = new Vector3(87.3f, 36.0f, 281.1f);
        }


    }

    private void UpdateMakeEnemy()
    {
        // ????(prefab)?? 
        // ??????
        if (level == Level.Lv1)
        {
            MakeLv1();
        }
        else if (level == Level.Lv2)
        {
            MakeLv2();
        }
        else if (level == Level.Lv3)
        {
            MakeLv3();
        }

        state = State.CheckEnemy;
    }

    public GameObject[] enemyFactoryLv1;
    public Transform[] spawnsLv1;
    private void MakeLv1()
    {
        list = new List<Monster>();
        for (int i = 0; i < spawnsLv1.Length; i++)
        {

            GameObject enemyFactory = enemyFactoryLv1[Random.Range(0, enemyFactoryLv1.Length)];
            GameObject monster = Instantiate(enemyFactory);

            monster.tag = "Monster";
            monster.name = "MonsterLv1_" + i.ToString();

            monster.transform.position = spawnsLv1[i].position;
            list.Add(monster.GetComponent<Monster>());
        }


        //list = new List<Monster>();
        //for (int i = 0; i < rgion.iMobCount; i++)
        //{
        //    GameObject enemyFactory = enemyFactoryLv1[Random.Range(0, enemyFactoryLv1.Length)];
        //    GameObject monster = Instantiate(enemyFactory);

        //    monster.tag = "Monster";
        //    monster.name = "MonsterLv1_" + i.ToString();

        //    int iArrIndex = rgion.byRandomSeed[i];

        //    monster.transform.position = rgion.CellList[iArrIndex].centerPos;
        //    list.Add(monster.GetComponent<Monster>());
        //}
    }



    public GameObject[] enemyFactoryLv2;
    public Transform[] spawnsLv2;
    private void MakeLv2()
    {
        list = new List<Monster>();
        for (int i = 0; i < spawnsLv2.Length; i++)
        {

            GameObject enemyFactory = enemyFactoryLv2[Random.Range(0, enemyFactoryLv2.Length)];
            GameObject monster = Instantiate(enemyFactory);

            monster.tag = "Monster";
            monster.name = "MonsterLv2_" + i.ToString();

            monster.transform.position = spawnsLv2[i].position;
            list.Add(monster.GetComponent<Monster>());
        }
    }

    public GameObject[] enemyFactoryLv3;
    public Transform[] spawnsLv3;
    private void MakeLv3()
    {
        list = new List<Monster>();
        for (int i = 0; i < spawnsLv3.Length; i++)
        {
            GameObject enemyFactory = enemyFactoryLv3[Random.Range(0, enemyFactoryLv3.Length)];
            GameObject monster = Instantiate(enemyFactory);

            monster.tag = "Monster";
            monster.name = "MonsterLv3_" + i.ToString();

            monster.transform.position = spawnsLv3[i].position;
            list.Add(monster.GetComponent<Monster>());
        }

    }

    public int GetEnemyTotal(Level level)
    {
        if (level == Level.Lv1)
        {
            return spawnsLv1.Length;
        }
        else if (level == Level.Lv2)
        {
            return spawnsLv2.Length;
        }
        else if (level == Level.Lv3)
        {
            return spawnsLv3.Length;
        }

        return 0;
    }

    //public Slider progressBar;
    public int GetEnemyCurrent(Level level)
    {
        if (level == Level.Lv1)
        {
            //Debug.Log("level 1 : " + list.Count);
            return list.Count;

        }
        else if (level == Level.Lv2)
        {
            //Debug.Log("level 2 : " + list.Count);
            return list.Count;
        }
        else if (level == Level.Lv3)
        {
            //Debug.Log("level 3 : " + list.Count);
            return list.Count;

        }

        return 0;

    }




    private void UpdateCheckEnemy()
    {

        if (level == Level.Lv1)
        {
            CheckEnemyLv1();
        }
        else if (level == Level.Lv2)
        {
            CheckEnemyLv2();
        }
        else if (level == Level.Lv3)
        {
            CheckEnemyLv3();
        }

    }
    private void CheckEnemyLv1()
    {
        //spawnsLv1[0] = enemyFactoryLv1[0];
        curMonCount1 = GetEnemyCurrent(Level.Lv1) / GetEnemyTotal(Level.Lv1);
        //Debug.Log("curmoncount1 : " + curMonCount1);

    }

    public int curMonCount1, curMonCount2, curMonCount3;
    public int Lv2spawnCount;
    public void CheckEnemyLv2()
    {

        curMonCount2 = GetEnemyCurrent(Level.Lv2) / GetEnemyTotal(Level.Lv2);
        //Debug.Log("curmoncount : " + curMonCount2);
    }

    private void CheckEnemyLv3()
    {
        curMonCount3 = GetEnemyCurrent(Level.Lv3) / GetEnemyTotal(Level.Lv3);
        //Debug.Log("curmoncount 3 : " + curMonCount3);

    }

    public void BossStart()
    {

        Slider.SetActive(false);
        SliderBossHPUI.SetActive(true);
        boss.SetActive(true);
        CameraManager.instance.BossStartCamera();

    }



    public void ICanDie(Monster monster)
    {
        if (list.Contains(monster))
        {
            ProgressBar.instance.GetComponent<Slider>().value += 1;

            list.Remove(monster);

            // ???? ?????? ???????? ???????? ?? ????????
            if (list.Count == 0)
            {
                if (level == Level.Lv1)
                {
                    //ProgressBar.instance.GetComponent<Slider>().value += 1;
                    Invoke("Lv1Clear", 0.5f);
                    LV01Stairs.gameObject.SetActive(true);
                    level = Level.Lv2;
                    state = State.MakeEnemy;
                }
                else if (level == Level.Lv2)
                {
                    //ProgressBar.instance.GetComponent<Slider>().value += 1;
                    StartCoroutine("Lv2Clear", 0.5f);
                    LV02Stairs.gameObject.SetActive(true);
                    level = Level.Lv3;
                    state = State.MakeEnemy;
                }
                else if (level == Level.Lv3)
                {
                    //ProgressBar.instance.GetComponent<Slider>().value += 1;
                    StartCoroutine("Lv3Clear", 0.5f);

                    LV03Stairs.gameObject.SetActive(true);
                    level = Level.LvBoss;
                    //Invoke("BossStart", 7f);
                }
            }
        }
    }

    private void Lv1Clear()
    {

        Lv1ClearUI.SetActive(true);
        Invoke("ClearOffUI", 3f);

    }

    private void Lv2Clear()
    {
        Lv2ClearUI.SetActive(true);
        Invoke("ClearOffUI", 3f);

    }
    private void Lv3Clear()
    {
        Lv3ClearUI.SetActive(true);
        Invoke("ClearOffUI", 3f);

    }

    private void ClearOffUI()
    {
        Lv1ClearUI.SetActive(false);
        Lv2ClearUI.SetActive(false);
        Lv3ClearUI.SetActive(false);

    }
}
