using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xinOnly : MonoBehaviour
{
    //信件的专属脚本,需要有下面这个布尔值才能放进物品栏里
    public bool xinOnBarSet;  //判断xin是否在物品栏里,防止一个xin占多个物品栏

    // Start is called before the first frame update
    void Start()
    {
        xinOnBarSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
