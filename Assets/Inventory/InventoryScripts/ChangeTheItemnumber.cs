using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

public class ChangeTheItemnumber : MonoBehaviour
{
    public Blackboard BB;
    public OperationMethod Operation = OperationMethod.Add;
    public OperationMethod Operationre = OperationMethod.Subtract;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
          if (collision.tag == "xin")
          {
              Debug.Log(BB.GetVariable("SpecialLetter").value);
              BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
            //BB.GetVariableValue<int>("SpecialLetter")=OperationTools.Operate() 
            
          }

        
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "xin")
        {
            Debug.Log(BB.GetVariable("SpecialLetter").value);
            BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);
            //BB.GetVariableValue<int>("SpecialLetter")=OperationTools.Operate() 
        }
    }
}
