using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NameInput : MonoBehaviour //IPointerClickHandler
{
   
    [SerializeField]
    GameObject nameInputWin = null;
    [SerializeField]
    InputField NewUserName = null;

    public Text[] scores;

    //public void OnPointerClick(PointerEventData eventData)
    //{

    //    //LogicValue.ScoreInput(NewUserName.text);
    //    //if (true)
    //    //{
    //    //nameInputWin.SetActive(true);

    //    //}
    //}

    public void BtnClick()
    {
        // 데이터 설정
        LogicValue.ScoreInput(NewUserName.text, Player.instance.HoneycbCount);
        Player.instance.HoneycbCount = 0;


        List<LogicValue.ScoreData> scoreDatas = LogicValue.ScoreArr;
        // UI를 채워야함.
        for (int i = 0; i < scoreDatas.Count; i++)
        {
            scores[i].text = scoreDatas[i].Name + "  " + scoreDatas[i].Score + "Point";
        }

        nameInputWin.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
