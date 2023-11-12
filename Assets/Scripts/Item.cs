using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject pickText;         //定义拾取文字
    public int id;                      //定义本道具id
    public GameObject openTrigger;      //定义是否能打开触发器
    public GameObject sound;            //定义声音

    // Start is called before the first frame update
    void Start()
    {
        pickText.SetActive(false);          //初始化时关闭文字
    }

    private void OnTriggerEnter(Collider other)     //当触发进入时
    {
        if (other.tag == "Player")                 //判断进入物体是不是玩家
        {
            pickText.SetActive(true);              //显示拾取文字
        }
    }

    private void OnTriggerExit(Collider other)      //当触发离开时
    {
        if (other.tag == "Player")                  //判断进入物体是不是玩家
        {
            pickText.SetActive(false);              //隐藏拾取文字
        }
    }

    private void Update()
    {
        if (pickText.activeSelf == true)           //如果字已经显示
        {
            if (Input.GetKeyDown(KeyCode.F))       //如果按下F键拾取
            {
                PackManager.instance.ChangeItem(id, true);  //包管理器中获得道具
                if (openTrigger != null)                    //如果有关联触发器
                {
                    openTrigger.SetActive(true);            //打开触发器
                    Instantiate(sound, transform.position, transform.rotation);      //将声音预设实例化到场景，播放声音
                }
                gameObject.SetActive(false);                //道具消失
            }
        }
    }
}
