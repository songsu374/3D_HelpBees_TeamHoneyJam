using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Start()
    {

    }

    void OnEnable()
    {
        Invoke("OffEffect", particle.main.duration);
    }

    public void OffEffect()
    {
        gameObject.SetActive(false);
    }

    public void OnEffect()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
