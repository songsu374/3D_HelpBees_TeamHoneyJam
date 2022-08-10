using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestoryTime = 1f;
    public Vector3 Offset = new Vector3(0, 1, 0);
    public Vector3 RandomIntensity = new Vector3(0.5f, 0, 0);

    void Start()
    {
        Destroy(gameObject, DestoryTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomIntensity.x, RandomIntensity.x),
            Random.Range(0.7f,1.3f), Random.Range(-RandomIntensity.z, RandomIntensity.z));
    }

    void Update()
    {
        
    }
}
