using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveByKey : MonoBehaviour
{
    public float speed = 10;  //����
    public float maxPosX = 10;  //�����ƶ���Χ
    Vector3 playerInput;

    // Update is called once per frame
    void Update()
    {
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0);  //��ȡ�������

        if ((transform.position.x < -maxPosX && Input.GetAxisRaw("Horizontal") < 0) || (transform.position.x > maxPosX && Input.GetAxisRaw("Horizontal") > 0))
        {
            playerInput = Vector3.zero;  //�ﵽ���߽�ֵʱֹͣ�ƶ�
        }

        transform.position += playerInput * speed * Time.deltaTime;
    }
}
