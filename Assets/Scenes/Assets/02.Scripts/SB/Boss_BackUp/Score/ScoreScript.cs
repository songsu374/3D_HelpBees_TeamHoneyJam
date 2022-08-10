using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update

    Text textCom = null;

    void Start()
    {
       textCom= GetComponent<Text>();
        if (null == textCom)
        {
            Debug.Log("Error");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (null == textCom)
        {
            Debug.Log("Error");
            return;
        }

        textCom.text = LogicValue.Score.ToString();

    }
}
