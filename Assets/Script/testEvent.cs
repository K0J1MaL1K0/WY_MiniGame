using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour
{
    public int eventNumer = 10001;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��xinOnBarSet��Ϊfalseֵ,�Ϳ��Խ���Ʒ�Ƴ���Ʒ����
        collision.GetComponent<xinOnly>().xinOnBarSet = false;

        Camera.main.GetComponent<CheckFront>().setEventNumber = eventNumer;  //���¼���ż���������
        print(CheckEvent.CheckList(Camera.main.GetComponent<CheckFront>().eventList, 10111));  //�����
    }
}
