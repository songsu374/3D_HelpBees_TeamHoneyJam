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

    //�������� ��ũ���� Start��ư�� ������ ��
    public void BtnStart()
    {
        start.SetActive(false);
        //���弼�ÿ� 00.Start �� 0��, 01.Player_New �� 1������ �α�
        SceneManager.LoadScene(1);

    }

    public void GameExplain()
    {
        //���� ���� UI �߱�
        expain.SetActive(true);
        start.SetActive(false);
    }

    //�ݱ��ư�� ��������
    public void BtnClose()
    {
        
        expain.SetActive(false);
        start.SetActive(true);

   }
}
