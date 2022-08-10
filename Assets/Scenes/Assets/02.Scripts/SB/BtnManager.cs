using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject start;
    [SerializeField] GameObject expain;

    void Start()
    {
        expain.SetActive(false);
    }

    //스프래쉬 스크린의 Start버튼을 눌렀을 때
    public void BtnStart()
    {
        start.SetActive(false);
        //빌드세팅에 00.Start 씬 0번, 01.Player_New 씬 1번으로 두기
        SceneManager.LoadScene(1);

    }

    public void GameExplain()
    {
        //게임 설명 UI 뜨기
        expain.SetActive(true);
        start.SetActive(false);
    }

    //닫기버튼을 눌렀을때
    public void BtnClose()
    {
        
        expain.SetActive(false);
        start.SetActive(true);

   }
}
