using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicValue : MonoBehaviour
{
    public static LogicValue instance;

    [SerializeField]
    //private static int m_Score;
    public static int Score { get { return Player.instance.HoneycbCount; } }

    public Player m_HoneycbCount;
    //public static int Honeycb { get { return m_HoneycbCount; } }

    [SerializeField]
    private static List<ScoreData> m_ScoreArr;

    public static List<ScoreData> ScoreArr { get { return m_ScoreArr; } }


    public class ScoreData
    {
        public string Name;
        public int Score;


        public ScoreData(string _Name, int _Score)
        {
            Name = _Name;
            Score = _Score;
        }
    }


    public static void ScoreLoad()
    {
        // ����Ȱ��� �ִٸ�
        if (PlayerPrefs.HasKey("Name0") == true)
        {
            // ���ھ� ����Ʈ�� �����
            m_ScoreArr = new List<ScoreData>();

            // �����´�.
            for (int i = 0; i < 5; i++)
            {
                //�ڵ����� ��� ���� �ִ�.            
                ScoreData NewScore = new ScoreData(PlayerPrefs.GetString("Name" + i, ""), PlayerPrefs.GetInt("Score" + i, 0));
                m_ScoreArr.Add(NewScore);

            }
            //Ű�� �����ϸ� ���� �����Ͱ� �ִٴ� �̾߱�
            return;
        }

        //��ųʸ��� ����ϰ�
        //Ű�� ���� �ϳ��� ��� ���� ����

        //�����ϸ� ���� �����͸� �ε� �ؾ��Ѵ�.

        m_ScoreArr = new List<ScoreData>();

        for (int i = 0; i < 5; i++)
        {
            //�ڵ����� ��� ���� �ִ�.
            PlayerPrefs.SetString("Name" + i, "");
            PlayerPrefs.SetInt("Score" + i, 0);

            ScoreData NewScore = new ScoreData("", 0);
            m_ScoreArr.Add(NewScore);

        }
    }

    public static void ScoreInput(string _Name, int _Score)
    {
        ScoreData CheckData = new ScoreData(_Name, _Score);
        ScoreLoad();
        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (_Score > ScoreArr[i].Score)
            {
                ScoreData TempScore = ScoreArr[i];
                //���ο� üũ �����Ͱ� �����ع�����
                ScoreArr[i] = CheckData;
                //���ο� ������ ������
                CheckData = TempScore;

            }
        }
        //������ ��������
        for (int i = 0; i < 5; i++)
        {
            //�ڵ����� ��� ���� �ִ�.
            PlayerPrefs.SetString("Name" + i, m_ScoreArr[i].Name);
            PlayerPrefs.SetInt("Score" + i, m_ScoreArr[i].Score);
        }
    }

    private void Awake()
    {
        instance = this;
        ScoreReset();
    }


    public static void ScoreReset()
    {
        //�ʱ�ȭ
        Player.instance.HoneycbCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_Score = Player.instance.HoneycbCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool ScoreCheck()
    {
        //����� �˻��ؼ� ����
        //�� ����� �ǹ̰� �ִٸ� true�� �����Ѵ�.
        //���� ����ִ� ���� �ִٸ�
        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (Player.instance.HoneycbCount > ScoreArr[i].Score)
            {

                Debug.Log("���ο� ��� true");


                return true;
            }
        }

        return false;
    }
}
