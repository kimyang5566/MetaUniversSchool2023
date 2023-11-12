using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;  //定义脚本实例
    public GameObject packUI;          //定义背包UI
    public GameObject[] allItemsUI;      //定义所有的物品
    public bool[] itemGet;             //定义所有物品获得状态

    private void Awake()
    {
        instance = this;               //将自己作为实例
    }

    // Start is called before the first frame update
    void Start()
    {
        packUI.SetActive(false);        //游戏开始时关闭背包
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))    //当按下Tab键
        {
            if (packUI.activeSelf == true)      //如果背包UI是打开的
            {
                packUI.transform.DOScaleY(0, 0.2f).OnComplete(CloseFinish);  //缩放动画
                
            }
            else                                //否则
            {
                packUI.SetActive(true);         //打开背包UI
                packUI.transform.DOScaleY(1, 0.2f);      //缩放动画
            }
        }
    }

    void CloseFinish()                  //缩放完成时
    {
        packUI.SetActive(false);        //关闭背包UI
    }

    public void ChangeItem(int id, bool v)      //背包中增加或减少道具
    {
        itemGet[id] = v;                        //改变物品获得状态
        allItemsUI[id].SetActive(v);            //改变背包中物品UI的状态
    }

    public void ShowPicture(GameObject obj)     //显示图片
    {
        obj.SetActive(true);                    //把对应图片显示
    }
}
