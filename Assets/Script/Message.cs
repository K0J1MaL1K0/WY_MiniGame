using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour

{
    public float time = 2f; //µÈ´ýÊ±¼ä
    public GameObject Go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled == true)
        {
            StartCoroutine(OnWaitMethod());  
        }
        
    }

    IEnumerator OnWaitMethod()
    {
        yield return new WaitForSeconds(time);
        Set(Go);
    }

    private static void Set(GameObject go)
    {
        go.SetActive(false);
    }
   


}
