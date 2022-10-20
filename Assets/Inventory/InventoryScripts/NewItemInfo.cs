using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemInfo : MonoBehaviour
{
    public GameObject GO;
    

    private void OnMouseEnter()
    {
        
        GO.SetActive(true);
    }

    private void OnMouseExit()
    {
        GO.SetActive(false);
    }
}
