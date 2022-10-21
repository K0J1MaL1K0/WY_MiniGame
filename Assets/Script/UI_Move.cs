using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Move : MonoBehaviour {
    public Camera camera;
    public GameObject target;
    public bool alwaysFollow = true;

    bool hasFollowed = false;
    public Canvas canvas;

    public void Init()
    {
        FollowObject();
    }

    public void Update()
    {
        FollowObject();
    }

    void FollowObject()
    {
        if (!alwaysFollow && hasFollowed)
            return;

        if (camera != null && target != null)
        {
            Vector2 pos = camera.WorldToScreenPoint(target.transform.position);
            Vector2 point;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, pos, canvas.worldCamera, out point))
            {
                transform.localPosition = new Vector3(point.x, point.y, 0);
                hasFollowed = true;
            }
        }
    }
}

