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
        // 저장된것이 있다면
        if (PlayerPrefs.HasKey("Name0") == true)
        {
            // 스코어 리스트를 만들고
            m_ScoreArr = new List<ScoreData>();

            // 가져온다.
            for (int i = 0; i < 5; i++)
            {
                //자동으로 계속 남아 있다.            
                ScoreData NewScore = new ScoreData(PlayerPrefs.GetString("Name" + i, ""), PlayerPrefs.GetInt("Score" + i, 0));
                m_ScoreArr.Add(NewScore);

            }
            //키가 존재하면 기존 데이터가 있다는 이야기
            return;
        }

        //딕셔너리와 비슷하게
        //키와 값을 하나로 묶어서 파일 저장

        //시작하면 기존 데이터를 로드 해야한다.

        m_ScoreArr = new List<ScoreData>();

        for (int i = 0; i < 5; i++)
        {
            //자동으로 계속 남아 있다.
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
                //새로운 체크 데이터가 차지해버린것
                ScoreArr[i] = CheckData;
                //새로운 떠돌이 데이터
                CheckData = TempScore;

            }
        }
        //끝나면 파일저장
        for (int i = 0; i < 5; i++)
        {
            //자동으로 계속 남아 있다.
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
        //초기화
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
        //기록을 검사해서 만약
        //내 기록이 의미가 있다면 true를 리턴한다.
        //만약 비어있는 곳이 있다면
        for (int i = 0; i < ScoreArr.Count; i++)
        {
            if (Player.instance.HoneycbCount > ScoreArr[i].Score)
            {

                Debug.Log("새로운 기록 true");


                return true;
            }
        }

        return false;
    }
}
