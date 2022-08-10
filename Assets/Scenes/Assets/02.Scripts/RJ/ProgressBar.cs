using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar instance;
    private void Awake()
    {
        instance = this;
    }
    public Slider progressBar;

    private void Start()
    {
    }

}

