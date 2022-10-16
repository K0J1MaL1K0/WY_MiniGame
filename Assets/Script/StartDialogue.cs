using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class StartDialogue : MonoBehaviour
{
    public DialogueTreeController dialogue;
    public Inventory item;
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {


            dialogue.StartDialogue();

        }
        


    }


   
}
