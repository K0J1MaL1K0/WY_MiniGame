using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine.EventSystems;

public class AddXin : MonoBehaviour
{
    public Blackboard BB;
    public OperationMethod Operation = OperationMethod.Add;
    public OperationMethod Operationre = OperationMethod.Subtract;

    public GameObject instantiateXin;

    public GameObject addXin;
    public GameObject subXin;

    public bool mouseOver;
    public bool xinOver;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mouseOver)
        {
            if (addXin == null)
            {
                addXin = Instantiate(instantiateXin, transform.position, transform.rotation);
            }

        }
        else
        {
            print("m out");
            if (xinOver)
            {
                print("x in");
                if (!Input.GetMouseButton(0) && subXin != null && addXin == null)
                {
                    Destroy(subXin);
                    BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
                }
            }
            else
            {
                if (!Input.GetMouseButton(0) && addXin != null)
                {
                    Destroy(addXin);
                    BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
                    addXin = null;
                }
            }

        }
    }

    private void OnMouseEnter()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }

    //以下是放碰撞器重叠出bug
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BarSet")
            if (collision.gameObject.GetComponent<BarSet>().xin != null)
                collision.gameObject.GetComponent<BarSet>().xin.GetComponent<BoxCollider2D>().enabled = false;

        if (collision.tag == "xin")
        {
            xinOver = true;
            subXin = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BarSet")
            if (collision.gameObject.GetComponent<BarSet>().xin != null)
                collision.gameObject.GetComponent<BarSet>().xin.GetComponent<BoxCollider2D>().enabled = true;

        if (collision.tag == "xin")
        {
            xinOver = false;
            subXin = null;
        }
    }
}
