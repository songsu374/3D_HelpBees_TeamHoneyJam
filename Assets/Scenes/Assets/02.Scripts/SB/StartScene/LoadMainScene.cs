using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    float loadTime = 38.5f;
    float curTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > loadTime)
        {
            SceneManager.LoadScene(2);
            Debug.Log(curTime);
            Debug.Log("¾À³Ñ¾î°¨");
        }
    }
}
