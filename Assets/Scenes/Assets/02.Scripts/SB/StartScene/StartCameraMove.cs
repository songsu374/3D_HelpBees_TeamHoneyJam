using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartCameraMove : MonoBehaviour
{
    public GameObject lookTarget;
    //public GameObject OpenUI;

    Vector3[] path_1 = new Vector3[32];
    Vector3[] path_2 = new Vector3[5];


    // Start is called before the first frame update
    void Start()
    {
        path_1[0].Set(-6.98f, 4.13f, -29.25f);
        path_1[1].Set(-6.98f, 0.92f, -19.12f);
        path_1[2].Set(2.23f, 8.71f, 11.48f);
        path_1[3].Set(-15.94f, 4.76f, 7.32f);
        path_1[4].Set(-15.94f, 12.65f, 50.70f);

        path_1[5].Set(1.24f, 11.32f, 88.87f);
        path_1[6].Set(1.24f, 18.73f, 78.52f);
        path_1[7].Set(6.11f, 17.93f, 81.37f);
        path_1[8].Set(48.04f, 22.40f, 91.76f);
        path_1[9].Set(49.02f, 22.95f, 82.44f);

        path_1[10].Set(51.04f, 31.45f, 83.62f);
        path_1[11].Set(77.40f, 19.70f, 99f);
        path_1[12].Set(71.30f, 27.39f, 120.80f);
        path_1[13].Set(127.40f, 2.20f, 153.60f);
        path_1[14].Set(127.40f, 10.10f, 153.60f);
        path_1[15].Set(127.40f, 38.90f, 153.60f);
        path_1[16].Set(122.16f, 31.54f, 169.80f);
        path_1[17].Set(133.80f, 24.60f, 205.10f);
        path_1[18].Set(131.77f, 31.97f, 178.86f);
        path_1[19].Set(164.5f, 45.40f, 185.30f);
        path_1[20].Set(146.61f, 45.40f, 227.55f);
        path_1[21].Set(161.19f, 45.40f, 241.5f);
        path_1[22].Set(171.69f, 47.59f, 259.70f);
        path_1[23].Set(188.80f, 67f, 261f);
        path_1[24].Set(223.5f, 50.09f, 310.5f);
        path_1[25].Set(235.69f, 58.20f, 327.79f);
        path_1[26].Set(240.78f, 84.63f, 349.01f);
        path_1[27].Set(231.39f, 93.19f, 356.54f);
        path_1[28].Set(218.77f, 89.23f, 346.69f);
        path_1[29].Set(-15.94f, 12.65f, 50.70f);
        path_1[30].Set(93.19f, 14.60f, 197f);
        path_1[31].Set(55.79f, 47.09f, 152.30f);

        //path_2[0].Set(50.58514f, 1.357653f, 181.1022f);
        //path_2[1].Set(126.5239f, 11.39495f, 161.3851f);
        //path_2[2].Set(174.075f, 21.3119f, 87.9463f);
        //path_2[3].Set(228.5955f, 12.17532f, 251.5007f);
        //path_2[4].Set(233.5114f, 13.98508f, 138.9019f);

        iTween.MoveTo(gameObject,
            iTween.Hash("path", path_1,
            "easyType", "linear",
            "looktarget", lookTarget.transform.position,
            "oncomplete", "MoveEnd_2",
            "time", 55f)
            );

        /*
        iTween.MoveTo(gameObject,
            iTween.Hash("path", iTweenPath.GetPath("New Path 2"),
            "easyType", "easeOutCirc",
            "looktarget", lookTarget.transform.position,
            "oncomplete", "MoveEnd",
            "time", 55f)
            );
      */

    }
    //움직이면서 어떤 방향으로 봐라

    //끝나고 나서 어떤함수를 해라


    void MoveEnd_1()
    {

        //OpenUI.SetActive(true);

        iTween.MoveTo(gameObject,
             iTween.Hash("path", path_2,
             "easyType", "linear",
             "looktarget", lookTarget.transform.position,
             "oncomplete", "MoveEnd_2",
             "time", 35f)
             );

    }

    void MoveEnd_2()
    {
        SceneManager.LoadScene("00.Start");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
