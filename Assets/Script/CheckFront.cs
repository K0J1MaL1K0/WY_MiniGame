using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFront : MonoBehaviour
{
    public int setEventNumber = 0;
    public List<int> eventList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(setEventNumber != 0)  //��setEventNumber��ֵʱ
        {
            if(!CheckEvent.CheckList(eventList, setEventNumber))  //��ֹ�ظ�
            {
                eventList.Add(setEventNumber);  //��setEventNumber����
            }
            setEventNumber = 0;  //����setEventNumber
        }
    }
}

public static class CheckEvent
{
    public static bool CheckList(this List<int> eventList, int eventNumber)
    {
        foreach(int i in eventList)
        {
            if (eventNumber == i)
                return true;
        }
        return false;
    }
}

