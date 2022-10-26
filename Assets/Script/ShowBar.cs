using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBar : MonoBehaviour
{
    public float speed = 10;  //显示速度
    public float showBarTime = 3;  //显示物品栏的计时
    public float maxY = -3.14f;  //最大上升的y值
    public float minY = -5;  //最小下降升的y值
    public float showBarAreaY = -2.5f;  //感应鼠标的最大范围

    float timer;
    float startTimer = 0.5f;  //开始运行时间计时

    // Start is called before the first frame update
    void Start()
    {
        timer = showBarTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer >= 0)  //几秒之后才开始运行,以防加载时出现bug
            startTimer -= Time.deltaTime;

        //判断鼠标位置
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < showBarAreaY && startTimer < 0)
        {
            if (transform.position.y < maxY)
                transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;  //上升

            timer = showBarTime;  //重置计时器
        }

        else
        {
            if (transform.position.y >= minY && timer < 0)
                transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;  //下降

            if (transform.position.y > minY)
                timer -= Time.deltaTime;  //计时
        }
    }
}
