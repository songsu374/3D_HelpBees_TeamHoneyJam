using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnim : MonoBehaviour
{
    public static BounceAnim instance;
    private void Awake()
    {
        instance = this;
    }
    float time = 0;
    public float size = 3;
    public float upSizeTime = 0.2f;


    void Start()
    {

    }

    void Update()
    {
        if (time <= upSizeTime)
        {
            
            transform.localScale = Vector3.one * (1 + size * time);
        }
        else if (time <= upSizeTime * 2)
        {
            transform.localScale = Vector3.one * (2 * size * upSizeTime + 1 - time * size);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        time += Time.deltaTime;
    }

    public void resetAnim()
    {
        time = 0;
    }

}
