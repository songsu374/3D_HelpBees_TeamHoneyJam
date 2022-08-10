using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { plus, minus };
    public Type itemType;
    public int hp;
    public float rotateSpeed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
