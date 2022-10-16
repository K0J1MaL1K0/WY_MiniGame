using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            Debug.Log(null == instance);
            if (null == instance)
            {
                //FindObjectOfType����Awake�еĸ�ֵ�滻���ɲ���Ҫ��дAwake�������������Բ�
                //instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null) instance = new GameObject("Single of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null) instance = this as T;
    }


}