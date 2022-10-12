using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveByKey : MonoBehaviour
{
    public float speed = 10;  //移速
    public float maxPosX = 10;  //最大的移动范围
    Vector3 playerInput;

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0);  //获取玩家输入

        if ((transform.position.x < -maxPosX && Input.GetAxisRaw("Horizontal") < 0) || (transform.position.x > maxPosX && Input.GetAxisRaw("Horizontal") > 0))
        {
            playerInput = Vector3.zero;  //达到最大边界值时停止移动
        }

        transform.position += playerInput * speed * Time.deltaTime;
    }
}
