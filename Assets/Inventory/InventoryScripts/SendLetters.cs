using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SendLetters : MonoBehaviour
{
    public Inventory Inventory;
    public Item thisItem;
    public SendLetters SendLetter;
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
           SendLetter.ReduceItem();

         }
    }


    //{
    //if (Input.GetMouseButton(0))
    //{
    //   SendLetter.ReduceItem();

    // }
    //}

    public void ReduceItem()
    {
        if (thisItem.itemHeld >= 2)
        {
            thisItem.itemHeld -= 1;
        }
        else
        {
            Inventory.itemList.Remove(thisItem);
        }
        InventoryManage.RefreshItem();
    }
}
