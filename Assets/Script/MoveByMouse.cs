using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByMouse : MonoBehaviour
{
    public GameObject MoveObject;  //��Ҫ�ƶ���GO
    public bool leftOrNot;  //��ߵĸ�Ӧ��Ϊtrue,�ұ�Ϊfalse
    public float speed = 10;
    public float rightMaxPosX = -10;  //�����ƶ���Χ

    Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (leftOrNot)  //������������
        {
            playerInput = new Vector3(-1, 0);
            rightMaxPosX = -rightMaxPosX;  //ȡ�෴��,�õ���ߵ����x������ֵ
        }
        else
        {
            playerInput = new Vector3(1, 0);
        }
    }

    private void OnMouseOver()
    {
        //MoveObject������
        if (!Input.GetMouseButton(0) && leftOrNot && MoveObject.transform.localPosition.x > rightMaxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
        //MoveObject������
        else if (!Input.GetMouseButton(0) && !leftOrNot && MoveObject.transform.localPosition.x < rightMaxPosX)
        {
            MoveObject.transform.position += playerInput * speed * Time.deltaTime;
        }
    }
}
