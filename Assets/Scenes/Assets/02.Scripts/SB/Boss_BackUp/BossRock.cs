using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : BossBullet
{
    Rigidbody rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShot;

    public float myTime = 5f;
    float curTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (myTime < curTime)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShot = true;
    
    }

    IEnumerator GainPower()
    {
        while (!isShot)
        {
            //¿ø·¡ °ª
            //angularPower += 0.02f;
            //scaleValue += 0.005f;
            angularPower += 0.05f;
            scaleValue += 0.007f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
