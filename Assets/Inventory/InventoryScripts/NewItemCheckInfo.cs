using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemCheckInfo : MonoBehaviour
{
    public Text text;
    bool Active = true;

    //private void Start()
    //{
    //    text.text = "";
    //}



    //private void OnMouseOver()
    //{
    //    if (Active)
    //    {
    //        Debug.Log(this.transform.GetChild(0).GetComponent<Text>().text);
    //        text.text = this.transform.GetChild(0).GetComponent<Text>().text;
    //        Active = false;
    //    }
    //}

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "xin")
        {
            Debug.Log(other.transform.GetChild(0).GetComponent<Text>().text);
            text.text = other.transform.GetChild(0).GetComponent<Text>().text;
        }
    }

}
