using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPatern : BossEnemy
{

    //�̻��� ���� ������ ������. 
    public GameObject bossMissile;
    public Transform missilePortA;
    public Transform missilePortB;
    //���� ������ ������ ���� ������ ����. 
    //Enemy ��ũ��Ʈ�� �ִ� bullet ������Ʈ�� �������. 

    //�÷��̾� ������ ���� ���� ���� ������.
    Vector3 lookVec;

    //�����ؼ� �������� �� ��� ���� taunt(����)���� ���ϴ� ����Ʈ �ʿ��ϴ�.
    Vector3 tauntVec;

    //�÷��̾� �ٶ󺸴� �÷��� ������ �߰���. 
    public bool isLook; //�÷��̾ �ٶ󺸰� �ִ°�

    BossHP bossHP;


    public float myTime = 2f;
    float curTime;

    Player player;

    [Header("��������ȿ��")]
    public GameObject taunteEffectFactory; //��ƼŬ�ý��� ����
    public GameObject missileEffectFactory;// �̻��� ��ƼŬ�ý��� ����
    public ParticleSystem awakeParticle;
    [Header("������ �׾��� �� UI")]
    [SerializeField] GameObject GameOverBoss;

    public GameObject canvasHighScore;
    [SerializeField] ParticleSystem[] BossDieParticle;
    //public GameObject canvasNameInput;
 
    //�������� �ִϸ��̼��� �ߴ��� �˱� ���� �Һ���
    bool IsAwake;

    //�´� ����, �״� ����
    public enum State
    {
        StartAction,
        TakeDamage,
        Die,
        Awake,//����

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
            //�ϰ� �ִ� ��� �ڷ�ƾ ���߱�
            StopAllCoroutines();
            return;
        }

        //�÷��̾ ��� �ٶ󺸰� ��.
        if (isLook)
        {
            //�÷��̾��� �������� �����ϱ� ���ؼ� �÷��̾��� �Է°��� �����.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //�÷��̾� �Է°����� ���� ���Ͱ��� ���� 5�� �� ����.
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
        //���̵� ������ ���⼭! 
        //�����ϴ� �ð�
        yield return new WaitForSeconds(0.1f);

        int randAction = Random.Range(0, 5);
        switch (randAction)
        {
            //Ȯ��

            case 0:
            case 1:
                //�̻���  �߻� ����
                StartCoroutine(MissileShot());

                break;
            case 2:
            case 3:
                //�� �������� ����
                StartCoroutine(RockShot());

                break;

            case 4:
            case 5:
                //���� ���� ����
                StartCoroutine(Taunt());

                break;

        }

    }


    IEnumerator MissileShot()
    {
        anim.SetTrigger("Shot");
        GameObject missilePS = Instantiate(missileEffectFactory);
        missilePS.transform.position = transform.position;
        yield return new WaitForSeconds(0.2f);  //�ִϸ��̼��� �ɸ��� �ð�
        //���� �ٸ� �̻��� 2�� �����
        //missile?->bossMissile�� �´°�
        GameObject instantMissileA = Instantiate(bossMissile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = target;


        yield return new WaitForSeconds(0.3f);  //�ִϸ��̼��� �ɸ��� �ð�
        GameObject instantMissileB = Instantiate(bossMissile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = target;


        yield return new WaitForSeconds(2f);  //�ִϸ��̼��� �ɸ��� �ð�

        curTime += Time.deltaTime;
        //�����ð��� ������ �����ȴ�.
        if (myTime < curTime)
        {
            Destroy(instantMissileA);
            Destroy(instantMissileB);

        }

        StartCoroutine(Think());


    }
    IEnumerator RockShot()
    {
        //�� �������� �� �Ĵ� ��
        isLook = false;

        anim.SetTrigger("BigShot");
        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);  //�ִϸ��̼��� �ɸ��� �ð�

        isLook = true;

        StartCoroutine(Think());

    }

    IEnumerator Taunt()
    {
        //�������� ��
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;
        //boxcollider.enabled = false;

        anim.SetTrigger("Taunt");

        yield return new WaitForSeconds(1f);  //�ִϸ��̼��� ��
        CameraShake.instacne.Shake();
        GameObject tauntPS = Instantiate(taunteEffectFactory);
        tauntPS.transform.position = target.position;
        //�����ð����� �����ϸ鼭 ������� �������� ���� �÷��̾�� ������ ���ظ� ��
        yield return new WaitForSeconds(0.5f);  //�ִϸ��̼��� �ɸ��� �ð�
        //meleeArea.enabled = true;

        //�����ð� �� ->���� ���ټ��ְ� �ڽ��ݶ��̴� ��Ȱ��ȭ�Ѵ�.
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
        //ĳ���Ϳ��� �Ѿ��� �¾��� �� ü�°���
        if (other.gameObject.tag == ("Bullet"))
        {
            if (bossHP.HP <= 0)
            {
                state = State.Die;

            }
            Destroy(other.gameObject);
            //���� �´� �ִϸ��̼�
            StartCoroutine("UpdateTakeDamage");

        }

        //�÷��̾�� �浹�ߴµ�
        //���� ��û���=�˹���¶��
        //Attack2������ ����
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


    //������ �Ѿ˷� �¾��� ��
    IEnumerator UpdateTakeDamage()
    {
        if (bossHP.HP > 0)
        {
            yield return new WaitForSeconds(0.5f);

            //���� �´� �ִϸ��̼�
            anim.SetTrigger("TakeDamage");
            bossHP.HP -= Player.instance.PlayerAttack1;

            Debug.Log("����ü��" + bossHP.HP);
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



        //���� �״� �ִϸ��̼�

        //yield return new WaitForSeconds(1f);
        //����->������ ����        

        isDead = true;
    }

    public GameObject bossImage_1;
    public GameObject bossImage_2;
    private void UpdateAwake()
    {
        awakeParticle.Play();
        anim.SetTrigger("Awake");
        Debug.Log("����");
        bossImage_1.SetActive(false);
        bossImage_2.SetActive(true);


    }
}

