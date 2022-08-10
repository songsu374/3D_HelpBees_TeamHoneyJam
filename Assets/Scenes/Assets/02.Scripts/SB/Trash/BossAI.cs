using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Animator bossAni;
    public Transform target; //�÷��̾�
    public float bossSpeed;
    bool enableAct; //�׼� ����ġ
   

    //미사일관련 변수
    public GameObject missile;
    public Transform missilePortA;
    public Transform missilePortB;

    Vector3 lookVec;
    //플레이어 움직임 예측 벡터 변수
    Vector3 tauntVec;
    //플레이어 바라보는 플래그
    public bool isLook;

    Rigidbody rigid;
    BoxCollider boxcollider;
    MeshRenderer meshs;
    NavMeshAgent nav;
    Animator anim;
    public BoxCollider meleeArea;

    float misilleLifeTime=3;
    float curTime;
    enum bossAI
    {
        Idle,
        Jump, //점프 공격
        Shot, //미사일 공격
        Rush, //돌진 공격
        Awake, //각성 -> 애니메이션
        Die


    }

    bossAI _BossAI;


    private void Awake()
    {
       Rigidbody rigid = GetComponent<Rigidbody>();
       BoxCollider boxcollider = GetComponent<BoxCollider>();
       MeshRenderer meshs = GetComponent<MeshRenderer>();
       NavMeshAgent nav = GetComponent<NavMeshAgent>();
       Animator bossAni = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(BossAtk());
    }

    // Start is called before the first frame update
    void Start()
    {
        bossAni = GetComponentInChildren<Animator>();
        enableAct = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableAct)
        {
            RotateBoss();
            MoveBoss();
            StartCoroutine(BossAtk());
        }

      
    }

    void RotateBoss()
    {
        Vector3 dir = target.position - transform.position;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    void MoveBoss()
    {
        if ((target.position - transform.position).magnitude >= 10)
        {
            bossAni.SetTrigger("Idle");       
            transform.Translate(Vector3.forward * bossSpeed * Time.deltaTime);
        }
        if ((target.position - transform.position).magnitude < 10)
        {
            bossAni.SetTrigger("Idle");
        }
    }

    //������ ���ݱ��
    //����, ����, ������
    //Walk , Taunt , Rush, Anger
    IEnumerator BossAtk()
    {     
        yield return new WaitForSeconds(0.1f);

        //0���� 5���� ���� �׼��ϱ�
        int randAction = Random.Range(0, 3);
        Debug.Log(randAction);

        if ((target.position - transform.position).magnitude < 10)
        {
            switch (randAction)
            {
                case 0:   //점프공격
                    UpdateJump();
                    break;
                case 1:   //미사일공격, 부하공격
                    UpdateShot();
                    break; 
                case 2:  //돌진공격
                    UpdateRush();
                    break;
                

            }
        }

    }

    void FreezeBoss()
    {
        enableAct = false;
    }
    void UnFreezeBoss()
    {
        enableAct = true;
    }

    private void UpdateJump()
    {      
        bossAni.Play("Jump");
        StartCoroutine(Jump());
    }

    private void UpdateShot()
    {
        bossAni.Play("Shot");

        StartCoroutine(MissileShot());
    }


    private void UpdateRush()
    {
        bossAni.Play("Rush");
        StartCoroutine(Rush());
    }

    IEnumerator MissileShot()
    {
        bossAni.SetTrigger("Shot");

        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile_S bossMissileA = instantMissileA.GetComponent<BossMissile_S>();
        bossMissileA.target = target;

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile_S bossMissileB = instantMissileB.GetComponent<BossMissile_S>();
        bossMissileB.target = target;

       curTime+= Time.deltaTime;
        if (misilleLifeTime > curTime)
        {
            Destroy(instantMissileA);
            Destroy(instantMissileB);
            curTime = 0;
        }



        yield return new WaitForSeconds(2f);

        StartCoroutine(BossAtk());

    }

    //돌진하는 코드
    IEnumerator Rush()
    {
        isLook = false;
        bossAni.SetTrigger("Shot");
        GetComponent<Monster>().speed = 3;  //보스 돌진시 스피드 조정
        
        yield return new WaitForSeconds(3f);

        isLook = true;
        StartCoroutine(BossAtk());


    }
    //점프 공격
    IEnumerator Jump()
    {
        tauntVec = target.position + lookVec;

        isLook = false;
        //nav.isStopped = false;
        boxcollider.enabled = false;

        bossAni.SetTrigger("Jump");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;


        yield return new WaitForSeconds(1f);
        isLook = true;
        nav.isStopped = true;
        boxcollider.enabled = true;
        StartCoroutine(BossAtk());


    }
}
