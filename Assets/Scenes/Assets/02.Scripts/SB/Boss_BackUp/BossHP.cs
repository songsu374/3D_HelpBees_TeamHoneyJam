using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public static BossHP instance;
    private void Awake()
    {
        instance = this;
    }

    public int maxHP = 100;
    public Slider sliderHP;
    int hp;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderHP.value = hp;
        }


    
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
