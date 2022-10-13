using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public float Magnification = 30;  //触碰反馈的缩放百分比

    public bool mouseEnter = false;  //判断鼠标是否触碰到Object

    Vector3 mousePos;
    Vector3 offset;  //偏移量,用来绑定鼠标和Object位置

    void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))  //鼠标触碰并且没有点击左键
        {
            mouseEnter = true;
        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))  //鼠标触碰并且没有点击左键
        {
            mouseEnter = false;
        }
    }

    void Update()
    {
        if (mouseEnter)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //获取鼠标位置

            transform.localScale = new Vector3(1 + (Magnification/100), 1 + (Magnification / 100), 1f);  //触碰反馈

            if (Input.GetMouseButton(0) && offset.magnitude < 10.06)  //控制偏移量的模,从而控制拾取范围
            {
                transform.position = mousePos - offset;  //利用偏移量计算出object位置
                transform.localScale = new Vector3(1, 1, 1);  //拾取反馈
            }
            else
            {
                offset = mousePos - transform.position;  //当鼠标触碰object时设定偏移量
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}