using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSet : MonoBehaviour
{
    public GameObject xin;  //��ȡ��ǰ��Ʒ��λ�����GO��Ϣ�ı���

    // Start is called before the first frame update
    void Start()
    {
        xin = null;  //��Ʒ����λ��Ϊ��
    }

    // Update is called once per frame
    void Update()
    {
        if (xin != null)  //��Ʒ����λ��������Ʒ
        {
            if (!(xin.GetComponent<Draggable>().mouseEnter && Input.GetMouseButton(0)))  //û�е��ʰȡʱ
                xin.transform.position = transform.position;  //��������Ʒ�����λ����                

            if (!xin.GetComponent<xinOnly>().xinOnBarSet)  //�ж�ԭ������Ʒ�Ƿ���Ҫ�ص�ԭλ
            {
                //�������Ҫ�ص�ԭλ֮��
                xin = null;  //�ӳ���Ʒ��λ�������Ʒ
                //�����������ײ��
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                gameObject.AddComponent<Rigidbody2D>();
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "xin" && !collision.GetComponent<xinOnly>().xinOnBarSet && !Input.GetMouseButton(0))  //������Ʒ
        {
            xin = collision.gameObject;  //��ȡ��ǰ��Ʒ��λ�����GO��Ϣ
            xin.GetComponent<xinOnly>().xinOnBarSet = true;  //��xin������Ʒ����־��Ϊtrue

            //����Ʒ����λ�õ���ײ�����رջ�ɾ��,��ֹʰȡ����ʱ����bug
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());

        }
    }
}
