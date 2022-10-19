using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBarUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float speed = 5;
    public GameObject MoveBar;
    public float showBarTime = 3;

    float timer;
    bool showBar = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = showBarTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (showBar && MoveBar.transform.position.y <= -2.5)
            MoveBar.transform.position -= new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;
        else if (!showBar && MoveBar.transform.position.y >= -5 && timer < 0)
            MoveBar.transform.position += new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

        print(timer);
        if(!showBar && MoveBar.transform.position.y > -3)
            timer -= Time.deltaTime;
        else if(showBar)
            timer = showBarTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showBar = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showBar = false;
    }
}
