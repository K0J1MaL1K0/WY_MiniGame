using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarSet : MonoBehaviour
{
    public GameObject xin;  //获取当前物品栏位置里的GO信息的变量

    // Start is called before the first frame update
    void Start()
    {
        xin = null;  //物品栏该位置为空
    }

    // Update is called once per frame
    void Update()
    {
        if (xin != null)  //物品栏该位置里有物品
        {
            if (!(xin.GetComponent<Draggable>().mouseEnter && Input.GetMouseButton(0)))  //没有点击拾取时
                xin.transform.position = transform.position;  //继续让物品放入该位置里                

            if (!xin.GetComponent<xinOnly>().xinOnBarSet)  //判断原来的物品是否还需要回到原位
            {
                //如果不需要回到原位之后
                xin = null;  //扔出物品栏位置里的物品
                //重新添加上碰撞器
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                gameObject.AddComponent<Rigidbody2D>();
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "xin" && !collision.GetComponent<xinOnly>().xinOnBarSet && !Input.GetMouseButton(0))  //放入物品
        {
            xin = collision.gameObject;  //获取当前物品栏位置里的GO信息
            xin.GetComponent<xinOnly>().xinOnBarSet = true;  //将xin放入物品栏标志改为true

            //将物品栏该位置的碰撞器都关闭或删除,防止拾取物体时出现bug
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());

        }
    }
}
