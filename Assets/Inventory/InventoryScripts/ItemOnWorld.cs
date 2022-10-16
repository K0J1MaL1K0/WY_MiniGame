using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }



    //{
    //   AddNewItem();
    //   Destroy(gameObject);
    // }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            //InventoryManage.CreateNewItem(thisItem);


        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManage.RefreshItem();
    }


    // Start is called before the first frame update
  
}
