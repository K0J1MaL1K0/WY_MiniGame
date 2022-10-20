using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBar : MonoBehaviour
{
    public float speed = 5;  //显示速度
    public float showBarTime = 3;  //显示物品栏的计时

    float timer;
    bool showBar = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = showBarTime;
    }

    // Update is called once per frame
    void Update()
    {
        //判断鼠标位置
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < -2)
        {
            if (transform.position.y < -2.5)
                transform.position -= new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

            timer = showBarTime;  //重置计时器
        }

        else
        {
            if (transform.position.y >= -5 && timer < 0)
                transform.position += new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

            if (transform.position.y > -3)
                timer -= Time.deltaTime;  //计时
        }
    }
}
