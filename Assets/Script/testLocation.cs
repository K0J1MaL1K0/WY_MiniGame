using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLocation : MonoBehaviour
{
    public GameObject objectLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            transform.position = objectLocation.GetComponent<Transform>().position;
        }
    }
}
