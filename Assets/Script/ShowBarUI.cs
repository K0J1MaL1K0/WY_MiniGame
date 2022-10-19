using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowBarUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float speed = 5;  //显示速度
    public GameObject MoveBar;  //物品栏父节点GO
    public float showBarTime = 3;  //显示物品栏的计时

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
        //利用物品栏的位置判断是否显示
        if (showBar && MoveBar.transform.position.y <= -2.5)
            MoveBar.transform.position -= new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;
        else if (!showBar && MoveBar.transform.position.y >= -5 && timer < 0)
            MoveBar.transform.position += new Vector3(0, -2.5f, 0) * speed * Time.deltaTime;

        if(!showBar && MoveBar.transform.position.y > -3)  //计时
            timer -= Time.deltaTime;
        else if(showBar)  //重置计时器
            timer = showBarTime;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        showBar = true;  //显示物品栏
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        showBar = false;  //关闭物品栏
    }
}
