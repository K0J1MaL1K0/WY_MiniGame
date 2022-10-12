using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveByMouse : MonoBehaviour
{
    public bool leftOrNot;
    public float speed = 10;
    public float maxPosX = 10;  //最大的移动范围

    Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (leftOrNot)
        {
            playerInput = new Vector3(-1, 0);
            maxPosX = -maxPosX;
        }
        else
        {
            playerInput = new Vector3(1, 0);
        }
    }

    private void OnMouseOver()
    {
        if(leftOrNot && Camera.main.transform.position.x > maxPosX)
        {
            Camera.main.transform.position += playerInput * speed * Time.deltaTime;
        }
        else if(!leftOrNot && Camera.main.transform.position.x < maxPosX)
        {
            Camera.main.transform.position += playerInput * speed * Time.deltaTime;
        }
    }
}
