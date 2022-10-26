using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xinOnly : MonoBehaviour
{
    //信件的专属脚本,需要有下面这个布尔值才能放进物品栏里
    public bool xinOnBarSet;  //判断xin是否在物品栏里,防止一个xin占多个物品栏
    public Text text;
    public GameObject Info;

    public float num = 500;

    // Start is called before the first frame update
    void Start()
    {
        xinOnBarSet = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseOver()
    {
        if (xinOnBarSet == true)
        {
            
            CheckItemInfo();
            Info.SetActive(true);
            Info.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y+num, 0);//this.transform.position
            if (Input.GetMouseButton(0))
            {
                Info.SetActive(false);
            }
        }
    }

    private void OnMouseExit()
    {
        if (xinOnBarSet == true)
        {
            Info.SetActive(false);
        }
            
    }

    
     

    private void CheckItemInfo()
    {
        text.text = this.transform.GetChild(0).GetComponent<Text>().text;
    }
}
