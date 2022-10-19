using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour
{
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
    }
}
