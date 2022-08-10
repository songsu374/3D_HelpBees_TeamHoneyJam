using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCheckUI : MonoBehaviour
{
    TextMeshProUGUI LevelCheck;

    void Start()
    {
        LevelCheck = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (MonsterManager.instance.LV01Stairs.activeSelf == false)
        {
            print("Level01");
            LevelCheck.text = "´ÞÄÞÇÑ ½£";
        }
        else if(MonsterManager.instance.LV02Stairs.activeSelf == false)
        {
            print("Level02");
            LevelCheck.text = "µÚ¼¯ÀÎ ¹úÁý";
        }
        else if (MonsterManager.instance.LV03Stairs.activeSelf == false)
        {
            print("Level03");
            LevelCheck.text = "µÕµÕ ¼¶";
        }
        else
        {
            print("LevelBoss");
            LevelCheck.text = "ºÐ³ëÇÑ ¸»¹úÀÇ Áý";
        }
    }
}
