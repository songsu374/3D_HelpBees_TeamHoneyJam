using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPatern : BossEnemy
{

    //미사일 관련 변수를 만들자. 
    public GameObject bossMissile;
    public Transform missilePortA;
    public Transform missilePortB;
    //돌을 저장할 변수는 따로 만들지 말자. 
    //Enemy 스크립트에 있는 bullet 오브젝트를 사용하자. 

    //플레이어 움직임 예측 벡터 변수 생성함.
    Vector3 lookVec;

    //점프해서 내려찍을 때 어느 곳에 taunt(도발)할지 정하는 포인트 필요하다.
    Vector3 tauntVec;

    //플레이어 바라보는 플래그 변수를 추가함. 
    public bool isLook; //플레이어를 바라보고 있는가

    BossHP bossHP;


    public float myTime = 2f;
    float curTime;

    Player player;

    [Header("보스패턴효과")]
    public GameObject taunteEffectFactory; //파티클시스템 공장
    public GameObject missileEffectFactory;// 미사일 파티클시스템 공장
    public ParticleSystem awakeParticle;
    [Header("보스가 죽었을 때 UI")]
    [SerializeField] GameObject GameOverBoss;

    public GameObject canvasHighScore;
    [SerializeField] ParticleSystem[] BossDieParticle;
    //public GameObject canvasNameInput;
 
    //각성상태 애니메이션을 했는지 알기 위한 불변수
    bool IsAwake;

    //맞는 상태, 죽는 상태
    public enum State
    {
        StartAction,
        TakeDamage,
        Die,
        Awake,//각성

    }
    public State state;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        meshs = GetComponents<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;

    }

    void Start()
    {
        isLook = true;
        bossHP = GetComponent<BossHP>();
        GameOverBoss.SetActive(false);
        state = State.StartAction;
        bossImage_2.SetActive(false);


    }


    void Update()
    {
        if (state == State.StartAction)
        {
            StartCoroutine(Think());
            state = State.TakeDamage;
        }
        if (isDead)
        {
            //하고 있는 모든 코루틴 멈추기
            StopAllCoroutines();
            return;
        }

        //플레이어를 계속 바라보게 함.
        if (isLook)
        {
            //플레이어의 움직임을 예측하기 위해서 플레이어의 입력값을 사용함.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //플레이어 입력값으로 예측 벡터값을 만듬 5를 더 가함.
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);

        }
        else
        {
            nav.SetDestination(tauntVec);
        }

        if (bossHP.HP <= 0)
        {
            state = State.Die;
            StartCoroutine("UpdateDie");
        }

        if (IsAwake == false)
        {
            if (bossHP.HP <= 50)
            {
                UpdateAwake();

                   IsAwake = true;
            }
        }

        switch (state)
        {

            case State.Die:
                StartCoroutine("UpdateDie");
                break;

            case State.Awake:
                StartCoroutine("UpdateAwake");
                break;
        }
    }

    IEnumerator Think()
    {
        //난이도 조절시 여기서! 
        //생각하는 시간
        yield return new WaitForSeconds(0.1f);

        int randAction = Random.Range(0, 5);
        switch (randAction)
        {
            //확률

            case 0:
            case 1:
                //미사일  발사 패턴
                StartCoroutine(MissileShot());

                break;
            case 2:
            case 3:
                //돌 굴러가는 패턴
                StartCoroutine(RockShot());

                break;

            case 4:
            case 5:
                //점프 공격 패턴
                StartCoroutine(Taunt());

                break;

        }

    }


    IEnumerator MissileShot()
    {
        anim.SetTrigger("Shot");
        GameObject missilePS = Instantiate(missileEffectFactory);
        missilePS.transform.position = transform.position;
        yield return new WaitForSeconds(0.2f);  //애니메이션이 걸리는 시간
        //서로 다른 미사일 2개 만들기
        //missile?->bossMissile이 맞는가
        GameObject instantMissileA = Instantiate(bossMissile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = target;


        yield return new WaitForSeconds(0.3f);  //애니메이션이 걸리는 시간
        GameObject instantMissileB = Instantiate(bossMissile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = target;


        yield return new WaitForSeconds(2f);  //애니메이션이 걸리는 시간

        curTime += Time.deltaTime;
        //일정시간이 지나면 삭제된다.
        if (myTime < curTime)
        {
            Destroy(instantMissileA);
            Destroy(instantMissileB);

        }

        StartCoroutine(Think());


    }
    IEnumerator RockShot()
    {
        //돌 굴릴때는 안 쳐다 봄
        isLook = false;

        anim.SetTrigger("BigShot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);  //애니메이션이 걸리는 시간

        isLook = true;

        StartCoroutine(Think());

    }

    IEnumerator Taunt()
    {
        //내려찍을 곳
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;
        //boxcollider.enabled = false;

        anim.SetTrigger("Taunt");

        yield return new WaitForSeconds(1f);  //애니메이션이 걸
        CameraShake.instacne.Shake();
        GameObject tauntPS = Instantiate(taunteEffectFactory);
        tauntPS.transform.position = target.position;
        //일정시간동안 점프하면서 내려찍고 내려찍을 동안 플레이어에게 닿으면 피해를 줌
        yield return new WaitForSeconds(0.5f);  //애니메이션이 걸리는 시간
        //meleeArea.enabled = true;

        //일정시간 끝 ->피해 안줄수있게 박스콜라이더 비활성화한다.
        yield return new WaitForSeconds(0.5f);
        //meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);

        isLook = true;
        nav.isStopped = true;
        //boxcollider.enabled = true;

        StartCoroutine(Think());

    }


    private void OnTriggerEnter(Collider other)
    {
        //캐릭터에게 총알을 맞았을 때 체력감소
        if (other.gameObject.tag == ("Bullet"))
        {
            if (bossHP.HP <= 0)
            {
                state = State.Die;

            }
            Destroy(other.gameObject);
            //보스 맞는 애니메이션
            StartCoroutine("UpdateTakeDamage");

        }

        //플레이어랑 충돌했는데
        //만약 대시상태=넉백상태라면
        //Attack2데미지 받음
        if (other.gameObject.tag == "Player")
        {
            if (Player.instance.isDash == true)
            {
                if (bossHP.HP <= 0)
                {
                    state = State.Die;

                }
                else if (bossHP.HP > 0)
                {
                    StartCoroutine("UpdateTakeDamage");
                    bossHP.HP -= Player.instance.PlayerAttack2;
                }


            }


        }
    }


    //보스가 총알로 맞았을 때
    IEnumerator UpdateTakeDamage()
    {
        if (bossHP.HP > 0)
        {
            yield return new WaitForSeconds(0.5f);

            //보스 맞는 애니메이션
            anim.SetTrigger("TakeDamage");
            bossHP.HP -= Player.instance.PlayerAttack1;

            Debug.Log("보스체력" + bossHP.HP);
        }
        else if (bossHP.HP <= 0)
        {
            state = State.Die;
        }

    }

    IEnumerator UpdateDie()
    {

        canvasHighScore.SetActive(true);
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(0.5f);


        //GameOverBoss.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < BossDieParticle.Length; i++)
        {
            BossDieParticle[i].Play();

        }
        Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);

        StopAllCoroutines();



        //보스 죽는 애니메이션

        //yield return new WaitForSeconds(1f);
        //엔딩->씬으로 가기        

        isDead = true;
    }

    public GameObject bossImage_1;
    public GameObject bossImage_2;
    private void UpdateAwake()
    {
        awakeParticle.Play();
        anim.SetTrigger("Awake");
        Debug.Log("각성");
        bossImage_1.SetActive(false);
        bossImage_2.SetActive(true);


    }
}

