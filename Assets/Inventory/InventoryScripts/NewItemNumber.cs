using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.UI;
using NodeCanvas;
using NodeCanvas.DialogueTrees;

public class NewItemNumber : MonoBehaviour
{
    public Blackboard BB;
    public Text Number;


    private void Update()
    {
        ConnectBlackboard();
    }




    private void ConnectBlackboard()
    {
        Number.text = BB.GetVariableValue<int>("SpecialLetter").ToString();
        
    }

    
}
