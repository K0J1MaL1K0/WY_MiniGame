using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine.EventSystems;
using NodeCanvas.DialogueTrees;



public class SendLetters : MonoBehaviour
{
    
    
    
    public DialogueTreeController dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            dialogue.StartDialogue();

        }
    }




   
}
