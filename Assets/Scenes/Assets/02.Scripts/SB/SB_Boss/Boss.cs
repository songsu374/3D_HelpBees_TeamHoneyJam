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

    //���ݽø���

    //���� ����
    private void UpdateJumpAttack()
    {
        //�÷��̾���� �Ÿ� �������
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }
    //���� ����
    private void UpdateRushAttack()
    {
        //�÷��̾���� �Ÿ� �������
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }

    //������ ���� ? �̻��� ����
    private void UpdateRollingAttack()
    {
        //�÷��̾���� �Ÿ� �������
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance >= agent.stoppingDistance)
        {

        }
    }



    private void UpdateMove()
    {
        //Ÿ���� ã�� �� �������� ���������Ѵ�.
        agent.destination = target.transform.position;
        //agent.SetDestination(target.transform.position);
        //�̵�:
        //1. ���� Player���� �Ÿ��� ���ϰ�
        //2. �� �Ÿ��� ���ݰ��ɰŸ����϶��(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            int randAttack = Random.Range(0, 3);
            Debug.Log(randAttack);

            //�������� ���������� �������ְ�ʹ�.
            switch (randAttack)
            {
                //��������
                case 0:
                    //��������
                    state = State.JumpATTACK;
                    break;

                //��������
                case 1:
                    state = State.RushATTACK;
                    break;

                //������ ���� , �̻��� ����
                case 2:
                    state = State.RollingATTACK;
                    break;



            }


            anim.Play("Move", 0, 0);
            //0.1�ʵ��� ���� �ִϸ��̼ǰ� �����ִϸ��̼��� ��ȯ�ض�?
            anim.CrossFade("Move", 0.1f, 0);
        }


    }

    private void UpdateIdle()
    {
        //���:
        //1. Player�� ã�� �ʹ�.
        target = GameObject.Find("Player");
        //2. ���� null�� �ƴ϶��, �̵����·� �����ϰ� �ʹ�
        if (target != null)
        {
            state = State.MOVE;
            //�ִϸ��̼��� ���¸� Move���·� �����ϰ� �ʹ�.
            anim.SetTrigger("Move");

        }
    }

    //������ �׾����� �Լ�
    private void UpdateDie()
    {
        anim.SetTrigger("Die");

    }


    //�̺�Ʈ �Լ�, �ݹ��Լ�
    public void OnAttackHit()
    {
        print("OnAttackHit");

        //agent.SetDestination(target.transform.position);
        //�̵�:
        //1. ���� Player���� �Ÿ��� ���ϰ�
        //2. �� �Ÿ��� ���ݰ��ɰŸ����϶��(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            BossManager.instance.DoHit();
        }

    }

    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // ���� ���ݰ��� �Ÿ��� �ƴ϶��(�������ٸ�) 

        //�̵�:
        //1. ���� Player���� �Ÿ��� ���ϰ�
        //2. �� �Ÿ��� ���ݰ��ɰŸ����϶��(agent.stoppingDistance)
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        if (distance > agent.stoppingDistance)
        {
            //�̵����·� �����Ѵ�.
            state = State.MOVE;
            //�ִϸ��̼ǵ�
            anim.SetTrigger("Move");
        }

    }
}
