using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_S : Enemy
{
    //미사일 관련 변수
    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;



    Vector3 lookVec;
    //플레이어 움직임 예측 벡터 변수
    Vector3 tauntVec;
    //플레이어 바라보는 플래그
    public bool isLook;

    //자식 스크립트만 단독 실행
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
            //플레이어 입력값으로 예측 벡터값 생성
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);

        }
        else
            nav.SetDestination(tauntVec);

    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        //0부터 5까지 랜덤 액션하기
        int randAction = Random.Range(0, 5);
        switch (randAction)
        {
            case 0:
            case 1:
                StartCoroutine(MissileShot());
                break;
            //미사일 발사 패턴
            case 2:

                break;
            case 3:
                //돌 굴러가는 패턴
                StartCoroutine(RockShot());
                break;

            case 4:
                //점프 공격 패턴
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
    //뛰어서 점프 공격
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
