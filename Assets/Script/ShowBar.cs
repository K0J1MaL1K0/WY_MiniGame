using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBar : MonoBehaviour
{
    public float speed = 10;  //��ʾ�ٶ�
    public float showBarTime = 3;  //��ʾ��Ʒ���ļ�ʱ
    public float maxY = -3.14f;  //���������yֵ
    public float minY = -5;  //��С�½�����yֵ
    public float showBarAreaY = -2.5f;  //��Ӧ�������Χ

    float timer;
    float startTimer = 0.5f;  //��ʼ����ʱ���ʱ

    // Start is called before the first frame update
    void Start()
    {
        timer = showBarTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer >= 0)  //����֮��ſ�ʼ����,�Է�����ʱ����bug
            startTimer -= Time.deltaTime;

        //�ж����λ��
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < showBarAreaY && startTimer < 0)
        {
            if (transform.position.y < maxY)
                transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;  //����

            timer = showBarTime;  //���ü�ʱ��
        }

        else
        {
            if (transform.position.y >= minY && timer < 0)
                transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;  //�½�

            if (transform.position.y > minY)
                timer -= Time.deltaTime;  //��ʱ
        }
    }
}
