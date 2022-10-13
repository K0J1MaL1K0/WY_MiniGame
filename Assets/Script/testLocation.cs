using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLocation : MonoBehaviour
{
    public GameObject xin;

    bool xinIsDragging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xinIsDragging = xin.GetComponent<Draggable>().mouseEnter && xin.transform.localScale==Vector3.positiveInfinity;
        if (!xinIsDragging)
        {
            xin.transform.position = transform.position;
        }
    }
}
