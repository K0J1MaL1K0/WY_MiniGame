using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public float Magnification = 30;  //�������������Űٷֱ�

    public bool mouseEnter = false;  //�ж�����Ƿ�����Object

    Vector3 mousePos;
    Vector3 offset;  //ƫ����,����������Objectλ��

    void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))  //��괥������û�е�����
        {
            mouseEnter = true;
        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))  //��괥������û�е�����
        {
            mouseEnter = false;
        }
    }

    void Update()
    {
        if (mouseEnter)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //��ȡ���λ��

            transform.localScale = new Vector3(1 + (Magnification/100), 1 + (Magnification / 100), 1f);  //��������

            if (Input.GetMouseButton(0) && offset.magnitude < 10.06)  //����ƫ������ģ,�Ӷ�����ʰȡ��Χ
            {
                transform.position = mousePos - offset;  //����ƫ���������objectλ��
                transform.localScale = new Vector3(1, 1, 1);  //ʰȡ����
            }
            else
            {
                offset = mousePos - transform.position;  //����괥��objectʱ�趨ƫ����
            }
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}