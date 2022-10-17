using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class ItemCheck : MonoBehaviour
{
    public DialogueTreeController dialogue;
    public Inventory item;
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {

            if(item.itemList[0].itemHeld >= 1)
            {
                dialogue.StartDialogue();
            }
            

        }



    }



}
