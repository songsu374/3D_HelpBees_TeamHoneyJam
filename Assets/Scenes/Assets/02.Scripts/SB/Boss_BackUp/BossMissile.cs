using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : BossBullet
{
    public Transform target;
    NavMeshAgent nav;

    public float myTime=3f;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(target.position);

        curTime += Time.deltaTime;
        //일정시간이 지나면 삭제된다.
        if (myTime < curTime)
        {
            Destroy(gameObject);
        }
    }
}
