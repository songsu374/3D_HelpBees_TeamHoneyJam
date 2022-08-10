using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // 리소스 폴더에서 로드한 이펙트 리소스 (메모리에만 상주 : 화면에 보여진 것이 아니다.)
    List<GameObject> effectlist = new List<GameObject>();

    public static EffectManager instance = null;

    // 유니티 상의 이펙트 풀 게임오브젝트
    // 이펙트를 생성하게 되면 effectPool의 자식으로 붙인다.
    // 생성된 이펙트를 effectPolllist에 보관

    Transform effectPool = null;
    List<Effect> effectPolllist = new List<Effect>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        effectPool = GameObject.Find("EffectPool").transform;

        // 리소스 폴더 안에 있는 이펙트를 모두 로드한다.
        // 리소스 폴더안의 Effect 폴더전체를 한번에 로드하여 보관한다.
        //GameObject[] objs = Resources.LoadAll<GameObject>("Effect");
        //for (int i = 0; i < objs.Length; i++)
        //{
        //    effectlist.Add(objs[i]);
        //}
    }

    // 이름으로 이펙트리스트에서 검사하여 검사한 결과를 리턴
    // 추가적으로 구현해야 하는 사항 
    // 이펙트 풀에서 사용가능한 이펙트가 있다면 재사용하고
    // 없다면 화면에 새로 이펙트를 생성한다.
    public Effect GetEffect(string strName)
    {
        // 10개까지는 무조건 생성하고 10개 이상부터는 아래의 조건을 비교하여 생성

        // 유니티상에 비어있는 이펙트 관리게임오브젝트가 생성되어 있어야 하고 
        // 그 아래에 이펙트를 붙여 준다.(이펙트 풀)
        // 이펙트풀에서 비활성화 되어 있는 이펙트 게임오브젝트를 검사
        GameObject activeEffect = null;
        Effect effectScript = null;
        if (effectPolllist.Count <= 10)
        {
            // 메모리에 로드된 이펙트 리소스를 이름으로 검색
            GameObject _obj = effectlist.Find(o => (o.name == strName));
            if (_obj != null)
            {
                activeEffect = Instantiate(_obj);   // 화면에 생성된 이펙트

                activeEffect.name = effectPolllist.Count.ToString();



                // 비어 있는 이펙트 게임오브젝트를 부모로 지정
                activeEffect.transform.SetParent(effectPool);
                // 생성된 이펙트 게임오브젝트에 Effect 스크립트를 추가
                effectScript = activeEffect.AddComponent<Effect>();
                // 아래의 이펙트 리스트에 활성화된 이펙트를 추가
                effectPolllist.Add(effectScript);
            }
        }
        else
        {
            effectScript = effectPolllist.Find(o => (o.gameObject.activeSelf == false));
        }
        return effectScript;
    }

    void Update()
    {

    }
}
