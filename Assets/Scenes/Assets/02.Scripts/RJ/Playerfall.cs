using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerfall : MonoBehaviour
{
    // Play�� Enemy�� �浹�ϸ� GameOver UI�� Ȱ�� �ϰ�ʹ� 
    public static Playerfall instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject gameOverUI;

    void Start()
    {
        // GameOver UI�� �¾�� ��Ȱ��ȭ �ϰ�ʹ� 
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

    // �����ư�� ������ �����ϰ�ʹ�
    public void OnClickQuit()
    {
        print("OnClickQuit");
        Application.Quit();
    }

    // ����۹�ư�� ������ ����� �ϰ�ʹ� 
    public void OnClickRestart()
    {
        print("OnClickRestart");
        //���� Scene�� �ٽ� Load�ϰ�ʹ� 
        SceneManager.LoadScene(1);
    }

}
