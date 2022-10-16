using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLetter : MonoBehaviour
{
    public Inventory letter;



    private void Update()
    {
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            letter.itemList[0].itemHeld += 1;
        }
    }





}
