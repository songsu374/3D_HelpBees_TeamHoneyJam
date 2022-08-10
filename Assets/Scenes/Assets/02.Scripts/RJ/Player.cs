using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public float speed;
    public int PlayerHp = 100;
    float h;
    float v;
    bool j;
    public int MaxPlayerHp = 100;
    Rigidbody rigid;
    bool Jumping;
    MeshRenderer[] meshs;
    bool Damaging;
    //public Monster monster;
    public bool isDash;
    float currentTime;
    public float dashTime = 1;
    public mission missionPenel;
    public GameObject CollectionPenel;
    public int HoneycbCount = 0;
    public Animator anim;

    [Header("?�레?�어 공격 ?�보")]
    public int PlayerAttack1 = 10;
    public int PlayerAttack2 = 15;

    [Header("?�레?�어가 죽었????UI")]
    [SerializeField] GameObject GameOverPlayer;

    [Header("?�운??")]
    [Header("����")]
    [Header("?�운??")]
    public AudioSource HoneycbSound;
    [SerializeField] AudioSource playerDashSound;
    [SerializeField] AudioSource minusItem;

    public enum State
    {
        Idle,
        Die,
    }
    public State state;

    void Awake()
    {
        instance = this;

        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    void Start()
    {
        GameOverPlayer.SetActive(false);
        dashEffect.SetActive(false);
        LogicValue.ScoreReset();

    }

    void Update()
    {
        CheckHP();

        GetKey();
        if (true == isDash)
        {
            currentTime += Time.deltaTime;
            if (currentTime > dashTime)
            {
                isDash = false;
            }
        }

        //Move();
        //Attack();
        switch (state)
        {
            case State.Idle:
                Thik();
                break;
            case State.Die:
                Die();
                break;
            
               
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(87.3f, 36.0f, 281.1f);
        }
    }

    //?�레?�어 체력 ?�인
    void CheckHP()
    {

        if (PlayerHp <= 0)
        {
            state = State.Die;
        }
    
    }

    void Die()
    {
        anim.SetTrigger("Die");
        GameOverPlayer.SetActive(true);

    }

    void Thik()
    {

        Move();
        Attack();
        //TPSCamera.instance.Move();
    }

    void GetKey()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        j = Input.GetButtonDown("Jump");

    }

    // ?��? ?�르�?
    void Move()
    {
        if (false == missionPenel.Panel.activeSelf && false == CollectionPenel.activeSelf)
        {
            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            dir.Normalize();

            transform.position += dir * speed * Time.deltaTime;

            if (Input.anyKey && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                //?�재 ?�치?�서 ?�직여?�할 방향?�로 ?�직임
                transform.forward = dir;
            }

            if (j && !Jumping) // Jump 구현 로직 
            {
                rigid.AddForce(Vector3.up * 8, ForceMode.Impulse);
                Jumping = true;
            }
            anim.SetTrigger("Move");
        }
    }



    //attack?��? ?�르�?근접?�있??몬스??방향?�로 빠르�?조금�??�격?�고?�다
    public float attackSpeed = 20;
    void Attack()
    {
        if (Input.GetMouseButtonDown(1) && false == missionPenel.Panel.activeSelf && false == CollectionPenel.activeSelf)
        {
            anim.SetTrigger("Attack2");
            rigid.AddForce(transform.forward * attackSpeed, ForceMode.Impulse);
            isDash = true;
            currentTime = 0;
            playerDashSound.Play();
            StartCoroutine("DashEffect");
        }
    }

    public GameObject dashEffect;
    IEnumerator DashEffect()
    {
        //start?�서??false
        //attack버튼???�르�??�성??
        dashEffect.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        dashEffect.SetActive(false);

    }


    IEnumerator OnDamage()
    {
        if (isDash == false)
        {
            //anim.SetTrigger("Damage");
            Damaging = true;
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.yellow;
            }

            yield return new WaitForSeconds(1f);

            Damaging = false;
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }
        }
    }

    //Player ?�동?�전 방�?
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }

    public void OnTriggerEnter(Collider other)
    {
        //Item 먹기
        if (other.gameObject.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.itemType)
            {
                case Item.Type.plus:
                    {
                        HoneycbSound.Play();
                        PlayerHp += item.hp;
                        if (MaxPlayerHp <= PlayerHp)
                        {
                            PlayerHp = 100;
                        }
                    }
                    break;

                case Item.Type.minus:
                    {
                        minusItem.Play();

                        PlayerHp += item.hp;
                        if (MaxPlayerHp <= PlayerHp)
                        {
                            PlayerHp = 100;
                        }
                    }
                    break;
            }
            Destroy(other.gameObject);
            
            print(PlayerHp);
        }
        if (other.tag == "Monster")
        {
            if (isDash == false)
            {
                Monster mons = GetComponent<Monster>();
                PlayerHp -= mons.AttackHP;
                anim.SetTrigger("Damage");
                Debug.Log(PlayerHp);
            }
        }

        //벌집??먹으�?벌집?� ?�라진다
        if (other.gameObject.tag == "Honeycomb")
        {
            print("Player's OnTriggerEnter : " + other.gameObject.name);
            HoneycbCount++;
            HoneycbSound.Play();
            Destroy(other.gameObject);
            BounceAnim.instance.resetAnim();
        }

        //if (other.gameObject.name == "CameraChangePoint")
        //{
        //    CameraManager.instance.ChangePointCamera();
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "missionNPC")
        {
            mission Mission = other.GetComponent<mission>();
            Mission.MissionSpawn(this);
        }


    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.name == "missionNPC")
    //    {
    //        mission Mission = other.GetComponent<mission>();
    //        Mission.MissionCastspell();
    //    }

    //}


    //1. 몬스?��? ?�백?�태?�서 ?�택???��? ?�도�?!
    //2. isDash OR ?�백 ?�태?�서 몬스?�의 HP가 깎이�? PlayerHP??깎이지 ?�도�?!
    public GameObject dashSuccessFactory;
    public GameObject dashSuccessPosition;
    void OnCollisionEnter(Collision collision)
    {
        // ?�?�중???�과 부?�혔?�면
        if (isDash && collision.gameObject.CompareTag("Monster"))
        {
            // 몬스??컴포?�트�?가?��???
            Monster mon = collision.gameObject.GetComponent<Monster>();
            // ?�백 ?�라�??�고?�다.
            mon.SetNuckback(-collision.contacts[0].normal * 50);
            dashEffect.SetActive(false);
            GameObject dashSuccess = Instantiate(dashSuccessFactory);
            dashSuccess.transform.position = dashSuccessPosition.transform.position;
            Destroy(dashSuccess, 3);
        }

        //??�?�?Jump
        // Floor?� ?�으�?Jump 불�??�태 
        if (collision.gameObject.tag == "Floor")
        {
            Jumping = false;
        }

        //공격받는 �?
        Monster mons = collision.gameObject.GetComponent<Monster>();
        //몬스?��? ?�촉?�면??몬스?��? ?�백?�태가 ?�닐 ??
        if (collision.gameObject.tag == "Monster" && mons.state != Monster.State.Nuckback)
        {
            anim.SetTrigger("Damage");
            //PlayerHP가 깎인??
            PlayerHp -= collision.gameObject.GetComponent<Monster>().AttackHP;
            Debug.Log("PlayerHp : " + PlayerHp);
            StartCoroutine("OnDamage");
        }



        //?�발,미사?�공�?굴리�?공격�??�는?�면     
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (MaxPlayerHp >= PlayerHp)
            {
                PlayerHp -= BossBullet.instance.damage;
                anim.SetTrigger("Damage");
                Debug.Log("PlayerHp : " + PlayerHp);
                Destroy(collision.gameObject);
            }
        }

    }
}
