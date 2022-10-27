using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSendLetter : MonoBehaviour
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
        if(collision.tag == "Daishu")
        {
            Destroy(this.gameObject);
        }
    }

}
