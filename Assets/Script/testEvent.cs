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
        //将xinOnBarSet设为false值,就可以将物品移出物品栏了
        collision.GetComponent<xinOnly>().xinOnBarSet = false;

        Camera.main.GetComponent<CheckFront>().setEventNumber = eventNumer;  //将事件编号加入链表里
        print(CheckEvent.CheckList(Camera.main.GetComponent<CheckFront>().eventList, 10111));  //检测编号
    }
}
