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
            transform.localScale += new Vector3(Magnification / 100, Magnification / 100, 1f);  //��������
            mouseEnter = true;
        }
    }

    private void OnMouseDown()
    {
        transform.localScale -= new Vector3(Magnification / 100, Magnification / 100, 1f);  //ʰȡ����
    }

    private void OnMouseUpAsButton()
    {
        transform.localScale += new Vector3(Magnification / 100, Magnification / 100, 1f);  //�ɿ�ʰȡ����
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);  //��ԭ���з���
        if (!Input.GetMouseButton(0))  //��ȫ�˳�
        {
            mouseEnter = false;
        }
    }

    void Update()
    {
        if (mouseEnter)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //��ȡ���λ��

            if (Input.GetMouseButton(0) && offset.magnitude < 10.06)  //����ƫ������ģ,�Ӷ�����ʰȡ��Χ
            {
                transform.position = mousePos - offset;  //����ƫ���������objectλ��
            }
            else
            {
                offset = mousePos - transform.position;  //����괥��objectʱ�趨ƫ����
            }
        }
    }
}