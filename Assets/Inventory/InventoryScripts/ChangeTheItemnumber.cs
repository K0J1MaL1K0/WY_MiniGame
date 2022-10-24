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
    public GameObject Prefab;
    Vector3 mousePos;



    //private void OnTriggerEnter2D(Collider2D collision)
    //{


    //        if (collision.tag == "xin")
    //        {
    //            Debug.Log(BB.GetVariable("SpecialLetter").value);
    //            BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
    //            //BB.GetVariableValue<int>("SpecialLetter")=OperationTools.Operate() 


    //        }


    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "xin")
    //    {
    //        Debug.Log(BB.GetVariable("SpecialLetter").value);
    //        BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);
    //        //BB.GetVariableValue<int>("SpecialLetter")=OperationTools.Operate() 
    //    }
    //}


        //在鼠标处生成预制体
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && (int)BB.GetVariable("SpecialLetter").value >=1)
        {
            this.tag = "Untagged";
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);
            Instantiate(Prefab, Prefab.transform.position = new Vector3(mousePos.x, mousePos.y, 0.5f), transform.rotation );

            Debug.Log("ss");

        }
    }

    private void OnMouseExit()
    {
        this.tag = "Contain";
    }
}
