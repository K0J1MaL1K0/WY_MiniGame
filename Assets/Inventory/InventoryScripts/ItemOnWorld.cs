using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;
    public DialogueTreeController dialogue;
    public Blackboard BB;
    public int NewItemNumber;

    private void Update()
    {
        //NewItemNumber = BB.GetVariableValue<int>("SpecialLetter");
    }

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
            //thisItem.itemHeld += 1;
            NewItemNumber+= 1;



        }

        InventoryManage.RefreshItem();
    }


    // Start is called before the first frame update
  
}
