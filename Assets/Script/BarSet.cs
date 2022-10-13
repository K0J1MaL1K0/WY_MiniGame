using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSet : MonoBehaviour
{
    public Collider2D xin;  //��ȡ��ǰ��Ʒ��λ�����GO��Ϣ�ı���

    // Start is called before the first frame update
    void Start()
    {
        xin = null;  //��Ʒ����λ��Ϊ��
    }

    // Update is called once per frame
    void Update()
    {
        if (xin != null)
        {
            xin.transform.position = transform.position;  //����Ʒ�����λ����

            if (!xin.GetComponent<xinOnly>().xinOnBarSet)  //�ж�ԭ������Ʒ�Ƿ���Ҫ�ص�ԭλ
            {
                xin = null;  //�ӳ���Ʒ��λ�������Ʒ
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<xinOnly>().xinOnBarSet)
        {
            xin = collision;  //��ȡ��ǰ��Ʒ��λ�����GO��Ϣ
            collision.GetComponent<xinOnly>().xinOnBarSet = true;  //��xin������Ʒ����־��Ϊtrue
        }
    }
}
