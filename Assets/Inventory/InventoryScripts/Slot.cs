using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update

    public Item slotItem;
    public Image slotImage;
    public Text slotNum;

    public void ItemOnClicked()
    {
        InventoryManage.UpdateItemInfo(slotItem.itemInformation);
        InventoryManage.BigUpdateItemInfo(slotItem.itemInformation);
    }







    

}
