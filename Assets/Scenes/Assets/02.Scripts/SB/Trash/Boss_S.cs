using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_S : Enemy
{
    //�̻��� ���� ����
    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;



    Vector3 lookVec;
    //�÷��̾� ������ ���� ���� ����
    Vector3 tauntVec;
    //�÷��̾� �ٶ󺸴� �÷���
    public bool isLook;

    //�ڽ� ��ũ��Ʈ�� �ܵ� ����
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        meshs = GetComponents<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(Think());
    }
    // Start is called before the first frame update
    void Start()
    {
        //isLook = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //�÷��̾� �Է°����� ���� ���Ͱ� ����
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);

        }
        else
            nav.SetDestination(tauntVec);

    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        //0���� 5���� ���� �׼��ϱ�
        int randAction = Random.Range(0, 5);
        switch (randAction)
        {
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;
            //�̻��� �߻� ����
            case 2:

                break;
            case 3:
                //�� �������� ����
                StartCoroutine(RockShot());
                break;

            case 4:
                //���� ���� ����
                StartCoroutine(Taunt());
                break;

        }

    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("doShot");

        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile_S bossMissileA = instantMissileA.GetComponent<BossMissile_S>();
        bossMissileA.target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile_S bossMissileB = instantMissileB.GetComponent<BossMissile_S>();
        bossMissileB.target = target;

        yield return new WaitForSeconds(2f);

        StartCoroutine(Think());

    }
    IEnumerator RockShot()
    {
        isLook = false;
        anim.SetTrigger("doRock");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(Think());


    }
    //�پ ���� ����
    IEnumerator Taunt()
    {
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;
        boxcollider.enabled = false;

        anim.SetTrigger("doTaunt");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;


        yield return new WaitForSeconds(1f);
        isLook = true;
        nav.isStopped = true;
        boxcollider.enabled = true;
        StartCoroutine(Think());


    }


}
