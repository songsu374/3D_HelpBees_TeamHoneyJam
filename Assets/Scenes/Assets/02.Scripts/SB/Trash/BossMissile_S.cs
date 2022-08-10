using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile_S : Bullet
{
    public Transform target;
    NavMeshAgent nav;
    //미사일이 살아있는 시간
    public float bossMissileLifeTime = 3f;
    float curTime;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        //만약 미사일이 생존시간 < 지나간시간
        //미사일 파괴
        //만약 플레이어가 충돌되었다면 플레이어 HP 깎인다.
        if (bossMissileLifeTime > curTime)
        {
            nav.SetDestination(target.position);         
        }
        else 
        {     
            Destroy(this);
        }
        curTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (bossMissileLifeTime > Time.deltaTime)
            {
                //충돌시 삭제
                Player.instance.PlayerHp -= damage;
                Destroy(this);
                Debug.Log("vv");

            }
        }
    }
}
