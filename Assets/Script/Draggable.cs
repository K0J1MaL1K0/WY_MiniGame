using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

public class Draggable : MonoBehaviour
{
    public float Magnification = 30;  //�������������Űٷֱ�
    public bool mouseEnter = false;  //�ж�����Ƿ�����Object
    
    //blackboard���
    public OperationMethod Operation = OperationMethod.Add;
    public OperationMethod Operationre = OperationMethod.Subtract;
    public Blackboard BB;


    Vector3 originalScale;  //ԭ���Ĵ�С
    Vector3 mousePos;
    Vector3 offset;  //ƫ����,����������Objectλ��

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;  //��ȡԭ���Ĵ�С
    }

    void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))  //��괥������û�е�����
            mouseEnter = true;
    }

 

    private void OnMouseExit()
    {
        transform.localScale = originalScale;  //�ָ����з���
        if (!Input.GetMouseButton(0))  //��ȫ�˳�
            mouseEnter = false;
    }

    void Update()
    {
        if (mouseEnter)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //��ȡ���λ��
            transform.localScale = new Vector3(originalScale.x * (1 + (Magnification / 100)), originalScale.y * (1 + (Magnification / 100)), 1f);  //��������

            if (Input.GetMouseButton(0) && offset.magnitude < 10.06)  //����ƫ������ģ,�Ӷ�����ʰȡ��Χ
            {
                transform.position = mousePos - offset;  //����ƫ���������objectλ��
                transform.localScale = originalScale;  //ʰȡ����
            }
            else
                offset = mousePos - transform.position;  //����괥��objectʱ�趨ƫ����
        }
    }
    //ʶ�𱳰� +1 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Contain")
        {
            BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
            Destroy(this.gameObject);
           
        }
    }
}