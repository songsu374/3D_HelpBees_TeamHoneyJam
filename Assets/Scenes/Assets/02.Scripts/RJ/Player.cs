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

    [Header("?Œë ˆ?´ì–´ ê³µê²© ?•ë³´")]
    public int PlayerAttack1 = 10;
    public int PlayerAttack2 = 15;

    [Header("?Œë ˆ?´ì–´ê°€ ì£½ì—ˆ????UI")]
    [SerializeField] GameObject GameOverPlayer;

    [Header("?¬ìš´??")]
    [Header("»ç¿îµå")]
    [Header("?¬ìš´??")]
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

    //?Œë ˆ?´ì–´ ì²´ë ¥ ?•ì¸
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

    // ?¤ë? ?„ë¥´ë©?
    void Move()
    {
        if (false == missionPenel.Panel.activeSelf && false == CollectionPenel.activeSelf)
        {
            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            dir.Normalize();

            transform.position += dir * speed * Time.deltaTime;

            if (Input.anyKey && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                //?„ì¬ ?„ì¹˜?ì„œ ?€ì§ì—¬?¼í•  ë°©í–¥?¼ë¡œ ?€ì§ì„
                transform.forward = dir;
            }

            if (j && !Jumping) // Jump êµ¬í˜„ ë¡œì§ 
            {
                rigid.AddForce(Vector3.up * 8, ForceMode.Impulse);
                Jumping = true;
            }
            anim.SetTrigger("Move");
        }
    }



    //attack?¤ë? ?„ë¥´ë©?ê·¼ì ‘?´ìˆ??ëª¬ìŠ¤??ë°©í–¥?¼ë¡œ ë¹ ë¥´ê²?ì¡°ê¸ˆë§??Œê²©?˜ê³ ?¶ë‹¤
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
        //start?ì„œ??false
        //attackë²„íŠ¼???„ë¥´ë©??œì„±??
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

    //Player ?ë™?Œì „ ë°©ì?
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
        //Item ë¨¹ê¸°
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

        //ë²Œì§‘??ë¨¹ìœ¼ë©?ë²Œì§‘?€ ?¬ë¼ì§„ë‹¤
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


    //1. ëª¬ìŠ¤?°ê? ?‰ë°±?íƒœ?ì„œ ?´íƒ???˜ì? ?Šë„ë¡?!
    //2. isDash OR ?‰ë°± ?íƒœ?ì„œ ëª¬ìŠ¤?°ì˜ HPê°€ ê¹ì´ê³? PlayerHP??ê¹ì´ì§€ ?Šë„ë¡?!
    public GameObject dashSuccessFactory;
    public GameObject dashSuccessPosition;
    void OnCollisionEnter(Collision collision)
    {
        // ?€?œì¤‘???ê³¼ ë¶€?ªí˜”?¤ë©´
        if (isDash && collision.gameObject.CompareTag("Monster"))
        {
            // ëª¬ìŠ¤??ì»´í¬?ŒíŠ¸ë¥?ê°€?¸ì???
            Monster mon = collision.gameObject.GetComponent<Monster>();
            // ?‰ë°± ?˜ë¼ê³??˜ê³ ?¶ë‹¤.
            mon.SetNuckback(-collision.contacts[0].normal * 50);
            dashEffect.SetActive(false);
            GameObject dashSuccess = Instantiate(dashSuccessFactory);
            dashSuccess.transform.position = dashSuccessPosition.transform.position;
            Destroy(dashSuccess, 3);
        }

        //??ë²?ë§?Jump
        // Floor?€ ?¿ìœ¼ë©?Jump ë¶ˆê??íƒœ 
        if (collision.gameObject.tag == "Floor")
        {
            Jumping = false;
        }

        //ê³µê²©ë°›ëŠ” ì¤?
        Monster mons = collision.gameObject.GetComponent<Monster>();
        //ëª¬ìŠ¤?°ì? ?‘ì´‰?˜ë©´??ëª¬ìŠ¤?°ê? ?‰ë°±?íƒœê°€ ?„ë‹ ??
        if (collision.gameObject.tag == "Monster" && mons.state != Monster.State.Nuckback)
        {
            anim.SetTrigger("Damage");
            //PlayerHPê°€ ê¹ì¸??
            PlayerHp -= collision.gameObject.GetComponent<Monster>().AttackHP;
            Debug.Log("PlayerHp : " + PlayerHp);
            StartCoroutine("OnDamage");
        }



        //?„ë°œ,ë¯¸ì‚¬?¼ê³µê²?êµ´ë¦¬ê¸?ê³µê²©ê³??¿ëŠ”?¤ë©´     
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
