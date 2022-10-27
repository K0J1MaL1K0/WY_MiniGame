using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class StartDialogue : MonoBehaviour
{
    public DialogueTreeController dialogue;
    public Inventory item;
    public Collider2D coll;

 
    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }
    private void Update()
    {
        //sendMail();
    }

    //private void sendMail()
    //{
    //    if(coll.tag == "xin")
    //    {
    //        //dialogue.StartDialogue();
    //        Debug.Log("´¥·¢¶Ô»°£¡");
    //    }
    //}
    
    private void OnMouseOver()
    {
        
        if (Input.GetMouseButton(0))
        {

            dialogue.StartDialogue();

        }
    }
}
