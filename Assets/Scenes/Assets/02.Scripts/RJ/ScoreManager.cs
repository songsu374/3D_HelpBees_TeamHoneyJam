using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//레벨 1 몬스터의 개수를 UI에 표시하고 싶다

public class ScoreManager : MonoBehaviour
{
    //싱글톤
    public static ScoreManager instance;
    private void Awake()
    {
        instance = this;
    }

    //Honeycomb 개수 카운트
    //PlayerHP 표시
    public TextMeshProUGUI hcCountText;
    public TextMeshProUGUI playerHpText;
    public TextMeshProUGUI MonsterLv1Count;
    
    //public int COUNT
    //{
    //    get { return count; }
    //    set
    //    {
    //        count = value;
    //        hcCountText.text = "HC : " + count;
    //    }
    //}


    private void Start()
    {
        Player.instance.HoneycbCount = 0;
        //MonsterLv1Count.text = ;
        playerHpText.text = "" + Player.instance.MaxPlayerHp;

    }


    // Update is called once per frame
    void Update()
    {
        playerHpText.text = "" + Player.instance.PlayerHp;
        hcCountText.text = "" + Player.instance.HoneycbCount;
        MonsterLv1Count.text = "Mon :" + MonsterManager.instance.GetEnemyCurrent(MonsterManager.Level.Lv1);
    }
}
