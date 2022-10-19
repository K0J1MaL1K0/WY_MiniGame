using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBarUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float speed = 5;  //��ʾ�ٶ�
    public GameObject MoveBar;  //��Ʒ�����ڵ�GO
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
        //������Ʒ����λ���ж��Ƿ���ʾ
        if (showBar && MoveBar.transform.position.y <= -2.5)
            MoveBar.transform.position -= new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;
        else if (!showBar && MoveBar.transform.position.y >= -5 && timer < 0)
            MoveBar.transform.position += new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

        if(!showBar && MoveBar.transform.position.y > -3)  //��ʱ
            timer -= Time.deltaTime;
        else if(showBar)  //���ü�ʱ��
            timer = showBarTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showBar = true;  //��ʾ��Ʒ��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showBar = false;  //�ر���Ʒ��
    }
}
