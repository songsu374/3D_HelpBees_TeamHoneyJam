using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    //싱글톤
    public static BossManager instance;
    //맞았을 때 표시되는 UI
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
        //깜빡거리고 싶다.
        hitUI.SetActive(true);

        //0.1초후
        yield return new WaitForSeconds(0.1f);
        hitUI.SetActive(false);
    }
}
