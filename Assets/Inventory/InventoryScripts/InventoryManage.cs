using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManage : MonoBehaviour
{
    // Start is called before the first frame update
    static InventoryManage instance;

    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text itemInformation;
    public Text bigItemInformation;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    public static void BigUpdateItemInfo(string itemDescription)
    {
        instance.bigItemInformation.text = itemDescription;
    }

    //挂载prefab到物品
    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();

    }
    //销毁重新创建
    public static void RefreshItem()
    {
        for (int i = 0; i< instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }
}
