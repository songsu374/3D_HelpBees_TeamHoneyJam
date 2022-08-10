using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // ���ҽ� �������� �ε��� ����Ʈ ���ҽ� (�޸𸮿��� ���� : ȭ�鿡 ������ ���� �ƴϴ�.)
    List<GameObject> effectlist = new List<GameObject>();

    public static EffectManager instance = null;

    // ����Ƽ ���� ����Ʈ Ǯ ���ӿ�����Ʈ
    // ����Ʈ�� �����ϰ� �Ǹ� effectPool�� �ڽ����� ���δ�.
    // ������ ����Ʈ�� effectPolllist�� ����

    Transform effectPool = null;
    List<Effect> effectPolllist = new List<Effect>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        effectPool = GameObject.Find("EffectPool").transform;

        // ���ҽ� ���� �ȿ� �ִ� ����Ʈ�� ��� �ε��Ѵ�.
        // ���ҽ� �������� Effect ������ü�� �ѹ��� �ε��Ͽ� �����Ѵ�.
        //GameObject[] objs = Resources.LoadAll<GameObject>("Effect");
        //for (int i = 0; i < objs.Length; i++)
        //{
        //    effectlist.Add(objs[i]);
        //}
    }

    // �̸����� ����Ʈ����Ʈ���� �˻��Ͽ� �˻��� ����� ����
    // �߰������� �����ؾ� �ϴ� ���� 
    // ����Ʈ Ǯ���� ��밡���� ����Ʈ�� �ִٸ� �����ϰ�
    // ���ٸ� ȭ�鿡 ���� ����Ʈ�� �����Ѵ�.
    public Effect GetEffect(string strName)
    {
        // 10�������� ������ �����ϰ� 10�� �̻���ʹ� �Ʒ��� ������ ���Ͽ� ����

        // ����Ƽ�� ����ִ� ����Ʈ �������ӿ�����Ʈ�� �����Ǿ� �־�� �ϰ� 
        // �� �Ʒ��� ����Ʈ�� �ٿ� �ش�.(����Ʈ Ǯ)
        // ����ƮǮ���� ��Ȱ��ȭ �Ǿ� �ִ� ����Ʈ ���ӿ�����Ʈ�� �˻�
        GameObject activeEffect = null;
        Effect effectScript = null;
        if (effectPolllist.Count <= 10)
        {
            // �޸𸮿� �ε�� ����Ʈ ���ҽ��� �̸����� �˻�
            GameObject _obj = effectlist.Find(o => (o.name == strName));
            if (_obj != null)
            {
                activeEffect = Instantiate(_obj);   // ȭ�鿡 ������ ����Ʈ

                activeEffect.name = effectPolllist.Count.ToString();



                // ��� �ִ� ����Ʈ ���ӿ�����Ʈ�� �θ�� ����
                activeEffect.transform.SetParent(effectPool);
                // ������ ����Ʈ ���ӿ�����Ʈ�� Effect ��ũ��Ʈ�� �߰�
                effectScript = activeEffect.AddComponent<Effect>();
                // �Ʒ��� ����Ʈ ����Ʈ�� Ȱ��ȭ�� ����Ʈ�� �߰�
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
