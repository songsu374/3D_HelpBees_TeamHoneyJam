using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerfall : MonoBehaviour
{
    // Play와 Enemy가 충돌하면 GameOver UI를 활성 하고싶다 
    public static Playerfall instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject gameOverUI;

    void Start()
    {
        // GameOver UI를 태어날때 비활성화 하고싶다 
        Playerfall.instance.gameOverUI.SetActive(false);
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = GetComponent<Player>();

        if(collision.gameObject.name == "Player")
        {
            Playerfall.instance.gameOverUI.SetActive(true);
        }
    }

    // 종료버튼을 누르면 종료하고싶다
    public void OnClickQuit()
    {
        print("OnClickQuit");
        Application.Quit();
    }

    // 재시작버튼을 누르면 재시작 하고싶다 
    public void OnClickRestart()
    {
        print("OnClickRestart");
        //현재 Scene을 다시 Load하고싶다 
        SceneManager.LoadScene(1);
    }

}
