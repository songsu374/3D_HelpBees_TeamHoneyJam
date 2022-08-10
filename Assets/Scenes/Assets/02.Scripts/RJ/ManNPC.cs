using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManNPC : MonoBehaviour
{

    public TextMeshProUGUI keyGtext;
    public GameObject bubbleImage;


    void Start()
    {
        keyGtext = GetComponentInChildren<TextMeshProUGUI>();
        keyGtext.text = "";
        bubbleImage.gameObject.SetActive(false);
    }

    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        //이 구역에 들어오면 G버튼을 누르라는 UI가 뜬다
        keyGtext.text = "key G를 누르세요";
        
        //만약 G버튼을 눌렀다면
        if (Input.GetKey(KeyCode.G))
        {
            //말풍선 UI 등장
            Debug.Log("키 누름");
            bubbleImage.gameObject.SetActive(true);
            keyGtext.enabled = false;

        }


    }

}
