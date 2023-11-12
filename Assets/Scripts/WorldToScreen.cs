using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldToScreen : MonoBehaviour
{
    public GameObject pos;          //定义剧情触发的物体作为位置
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(pos.transform.position);       //将剧情触发的位置转换成屏幕坐标位置给UI
    }

    
}
