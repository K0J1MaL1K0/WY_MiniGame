using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFront : MonoBehaviour
{
    public int setEventID = 0;
    public List<int> eventList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(setEventID != 0)
        {
            eventList.Add(setEventID);
            setEventID = 0;
        }
    }
}

public static class CheckEvent
{
    public static bool CheckList(this List<int> eventList, int eventID)
    {
        foreach(int i in eventList)
        {
            if (eventID == i)
                return true;
        }
        return false;
    }
}

