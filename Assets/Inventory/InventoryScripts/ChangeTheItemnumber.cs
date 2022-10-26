using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine.EventSystems;

public class ChangeTheItemnumber : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Blackboard BB;
    public OperationMethod Operation = OperationMethod.Add;
    public OperationMethod Operationre = OperationMethod.Subtract;
    public GameObject Prefab;
    Vector3 mousePos;

    bool mouseEnter = false;
    bool dragging = false;
    GameObject xin;



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
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouseEnter)
            if (Input.GetMouseButtonDown(0) && (int)BB.GetVariable("SpecialLetter").value >= 1)
                dragging = true;


        if (Input.GetMouseButton(0) && xin != null)
            xin.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        else
            xin = null;
            //Destroy(xin);
    }

    //在鼠标处生成预制体
    //private void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(0) && (int)BB.GetVariable("SpecialLetter").value >=1)
    //    {
    //        this.tag = "Untagged";
    //        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);
    //        Instantiate(Prefab, Prefab.transform.position = transform.position, transform.rotation );

    //        Debug.Log("ss");

    //    }
    //}

    //private void OnMouseExit()
    //{
    //    this.tag = "Contain";
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEnter = false;
        if (dragging)
        {
            this.tag = "Untagged";
            BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);
            xin = Instantiate(Prefab, new Vector3(mousePos.x, mousePos.y, 1f), transform.rotation);
            

            Debug.Log("ss");
        }

        
    }
}
