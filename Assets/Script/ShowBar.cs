using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBar : MonoBehaviour
{
    public float speed = 5;  //��ʾ�ٶ�
    public float showBarTime = 3;  //��ʾ��Ʒ���ļ�ʱ

    float timer;
    bool showBar = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = showBarTime;
    }

    // Update is called once per frame
    void Update()
    {
        //�ж����λ��
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < -2)
        {
            if (transform.position.y < -2.5)
                transform.position -= new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

            timer = showBarTime;  //���ü�ʱ��
        }

        else
        {
            if (transform.position.y >= -5 && timer < 0)
                transform.position += new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

            if (transform.position.y > -3)
                timer -= Time.deltaTime;  //��ʱ
        }
    }
}
