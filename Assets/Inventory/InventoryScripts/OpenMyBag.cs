using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMyBag : MonoBehaviour
{
    public GameObject myBag;
    bool isOpen = true;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenBag();
    }

    void OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            myBag.SetActive(isOpen);
        }
    }

    
}
