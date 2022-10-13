using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSet : MonoBehaviour
{
    public Collider2D xin;  //获取当前物品栏位置里的GO信息的变量

    // Start is called before the first frame update
    void Start()
    {
        xin = null;  //物品栏该位置为空
    }

    // Update is called once per frame
    void Update()
    {
        if (xin != null)
        {
            xin.transform.position = transform.position;  //让物品放入该位置里

            if (!xin.GetComponent<xinOnly>().xinOnBarSet)  //判断原来的物品是否还需要回到原位
            {
                xin = null;  //扔出物品栏位置里的物品
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<xinOnly>().xinOnBarSet)
        {
            xin = collision;  //获取当前物品栏位置里的GO信息
            collision.GetComponent<xinOnly>().xinOnBarSet = true;  //将xin放入物品栏标志改为true
        }
    }
}
