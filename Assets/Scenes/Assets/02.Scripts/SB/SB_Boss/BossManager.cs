using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    //�̱���
    public static BossManager instance;
    //�¾��� �� ǥ�õǴ� UI
    public GameObject hitUI;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        hitUI.SetActive(false);
    }

    public void DoHit()
    {
        StartCoroutine("_DoHit");

    }

    IEnumerator _DoHit()
    {
        //�����Ÿ��� �ʹ�.
        hitUI.SetActive(true);

        //0.1����
        yield return new WaitForSeconds(0.1f);
        hitUI.SetActive(false);
    }
}
