using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByMouse : MonoBehaviour
{
    public GameObject MoveObject;  //需要移动的GO
    public bool leftOrNot;  //左边的感应区为true,右边为false
    public float speed = 10;
    public float rightMaxPosX = -10;  //最大的移动范围

    Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (leftOrNot)  //设置左右区域
        {
            playerInput = new Vector3(-1, 0);
            rightMaxPosX = -rightMaxPosX;  //取相反数,得到左边的最大x轴坐标值
        }
        else
        {
            playerInput = new Vector3(1, 0);
        }
    }

    private void OnMouseOver()
    {
        //MoveObject往左移
        if (!Input.GetMouseButton(0) && leftOrNot && MoveObject.transform.localPosition.x > rightMaxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
        //MoveObject往右移
        else if (!Input.GetMouseButton(0) && !leftOrNot && MoveObject.transform.localPosition.x < rightMaxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
    }
}
