using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;

public class clickStartDialogue : MonoBehaviour
{
    public DialogueTreeController dialogue;
 
    private void Start()
    {
        
    }
    private void Update()
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
