using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile_S : Bullet
{
    public Transform target;
    NavMeshAgent nav;
    //�̻����� ����ִ� �ð�
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
        //���� �̻����� �����ð� < �������ð�
        //�̻��� �ı�
        //���� �÷��̾ �浹�Ǿ��ٸ� �÷��̾� HP ���δ�.
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
                //�浹�� ����
                Player.instance.PlayerHp -= damage;
                Destroy(this);
                Debug.Log("vv");

            }
        }
    }
}
