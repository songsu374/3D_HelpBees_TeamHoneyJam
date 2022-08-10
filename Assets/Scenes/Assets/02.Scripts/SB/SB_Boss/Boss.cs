using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    private void Awake()
    {
        instance = this;
    }

    GameObject target;
    NavMeshAgent agent = null;

    public enum State
    {
        IDLE,
        MOVE,

        JumpATTACK,
        RushATTACK,
        RollingATTACK,


        DIE,
    }
    public State state;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        state = State.IDLE;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        switch (state)
        {
            case State.IDLE:
                UpdateIdle();
                break;
            case State.MOVE:
                UpdateMove();
                break;
            case State.JumpATTACK:
                UpdateJumpAttack();
                break;
            case State.RushATTACK:
                UpdateRushAttack();
                break;
            case State.RollingATTACK:
                UpdateRollingAttack();
                break;

            case State.DIE:
                UpdateDie();
                break;
        }
    }

    //공격시리즈

    //점프 공격
    private void UpdateJumpAttack()
    {
        //플레이어와의 거리 계속측정
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }
    //돌진 공격
    private void UpdateRushAttack()
    {
        //플레이어와의 거리 계속측정
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }

    //굴리기 공격 ? 미사일 공격
    private void UpdateRollingAttack()
    {
        //플레이어와의 거리 계속측정
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }



    private void UpdateMove()
    {
        //타겟을 찾은 후 목적지로 움직여야한다.
        agent.destination = target.transform.position;
        //agent.SetDestination(target.transform.position);
        //이동:
        //1. 나와 Player와의 거리를 구하고
        //2. 그 거리가 공격가능거리이하라면(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            int randAttack = Random.Range(0, 3);
            Debug.Log(randAttack);

            //렌덤으로 공격패턴을 실행해주고싶다.
            switch (randAttack)
            {
                //점프공격
                case 0:
                    //상태전이
                    state = State.JumpATTACK;
                    break;

                //돌진공격
                case 1:
                    state = State.RushATTACK;
                    break;

                //굴리기 공격 , 미사일 공격
                case 2:
                    state = State.RollingATTACK;
                    break;



            }


            anim.Play("Move", 0, 0);
            //0.1초동안 지금 애니메이션과 다음애니메이션을 변환해라?
            anim.CrossFade("Move", 0.1f, 0);
        }


    }

    private void UpdateIdle()
    {
        //대기:
        //1. Player를 찾고 싶다.
        target = GameObject.Find("Player");
        //2. 만약 null이 아니라면, 이동상태로 전이하고 싶다
        if (target != null)
        {
            state = State.MOVE;
            //애니메이션의 상태를 Move상태로 전이하고 싶다.
            anim.SetTrigger("Move");

        }
    }

    //보스가 죽었을때 함수
    private void UpdateDie()
    {
        anim.SetTrigger("Die");

    }


    //이벤트 함수, 콜백함수
    public void OnAttackHit()
    {
        print("OnAttackHit");

        //agent.SetDestination(target.transform.position);
        //이동:
        //1. 나와 Player와의 거리를 구하고
        //2. 그 거리가 공격가능거리이하라면(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            BossManager.instance.DoHit();
        }

    }

    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // 만약 공격가능 거리가 아니라면(도망갔다면) 

        //이동:
        //1. 나와 Player와의 거리를 구하고
        //2. 그 거리가 공격가능거리이하라면(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance > agent.stoppingDistance)
        {
            //이동상태로 전이한다.
            state = State.MOVE;
            //애니메이션도
            anim.SetTrigger("Move");
        }

    }
}
