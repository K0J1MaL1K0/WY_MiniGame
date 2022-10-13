using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByMouse : MonoBehaviour
{
    public GameObject MoveObject;  //��Ҫ�ƶ���GO
    public bool leftOrNot;  //��ߵĸ�Ӧ��Ϊtrue,�ұ�Ϊfalse
    public float speed = 10;
    public float maxPosX = 10;  //�����ƶ���Χ

    Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (leftOrNot)  //������������
        {
            playerInput = new Vector3(-1, 0);
            maxPosX = -maxPosX;
        }
        else
        {
            playerInput = new Vector3(1, 0);
        }
    }

    private void OnMouseOver()
    {
        if(!Input.GetMouseButton(0) && leftOrNot && MoveObject.transform.position.x > maxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
        else if(!Input.GetMouseButton(0) && !leftOrNot && MoveObject.transform.position.x < maxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
    }
}
