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

    GameObject existXin;

    bool mouseEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        mouseEnter = true;
        Instantiate(instantiateXin, transform.position, transform.rotation);

        //BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operationre);

        Destroy(gameObject.GetComponent<Rigidbody2D>(), 1f);//将物品栏该位置的刚体删除,防止拾取物体时出现bug
    }
    private void OnMouseExit()
    {
        mouseEnter = false;


        //重新添加上刚体
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "xin" && !Input.GetMouseButton(0))
        {
            Destroy(collision.gameObject);
            //BB.GetVariable("SpecialLetter").value = OperationTools.Operate(BB.GetVariableValue<int>("SpecialLetter"), 1, Operation);
        }
    }

    //以下是放碰撞器重叠出bug
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BarSet")
            if (collision.gameObject.GetComponent<BarSet>().xin != null)
                collision.gameObject.GetComponent<BarSet>().xin.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "BarSet")
            if (collision.gameObject.GetComponent<BarSet>().xin != null)
                collision.gameObject.GetComponent<BarSet>().xin.GetComponent<BoxCollider2D>().enabled = true;
    }
}
