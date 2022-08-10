using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerfire : MonoBehaviour
{
    public Transform bulletPosition; //총알 위치 
    public GameObject bulletFactory; //총알 공장 = 총알 
    public mission missionPenel;
    public GameObject CollectionPenel;
    //bool ischeck;

    void Start()
    {
        
    }

    //mission에서 UI가 비활성화 false일 때 공격 가능
    void Update()
    {
            if (Input.GetButtonDown("Fire1") && false == missionPenel.Panel.activeSelf && false == CollectionPenel.activeSelf) 
                //jump, move, attack에서도 해당 조건 추가하고 싶다... 근데 자꾸 NullReferenceException 떠요 ㅜㅜ 
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = bulletPosition.transform.position;
                Rigidbody bulletRigid = bullet.GetComponent<Rigidbody>(); //인스턴스화 된 총알에 속도 적용
                bulletRigid.velocity = bulletPosition.forward * 30;

            Player.instance.anim.SetTrigger("Attack1");
            }
    }

}
