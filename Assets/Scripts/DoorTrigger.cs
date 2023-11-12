using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;              //使用dotween插件库

public enum DoorType                   //定义门的类型
{
    norm,                              //普通门
    key,                               //需要钥匙打开的门
    password                           //密码门
}

public class DoorTrigger : MonoBehaviour
{
    public GameObject thisDoor;          //定义门物体
    public float moveDistance;           //开门移动距离
    public float moveTime;               //开门时间
    float orgPos;                        //定义起始位置
    public DoorType doorType;            //定义这个门的类型
    public string password;              //定义这个门的密码
    public int keyID;                    //定义该门对应钥匙
    public GameObject tips;              //定义提示文字
    public bool isOpen;                  //定义门的开关状态

    // Start is called before the first frame update
    void Start()
    {
        orgPos = thisDoor.transform.localPosition.x;    //把原来的位置给起始位置变量
    }

    private void OnTriggerEnter(Collider other)       //当触发器有物体进入时
    {
        if (other.tag == "Player")                    //如果进入的是玩家
        {
            if (doorType == DoorType.norm)
            {
                thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //开门
                GetComponent<AudioSource>().Play();                              //播放门音效
                isOpen = true;                                                   //开门状态为true
            }
            else if (doorType == DoorType.password)
            {
                //GameManager.instance.control.gameObject.SetActive(false);        //关闭角色
                Password.instance.door = gameObject;          //将门给到密码脚本
                Debug.Log(Password.instance);
                Password.instance.OpenUI();                   //打开密码UI
            }
            else                          //如果是钥匙门
            {
                if ( PackManager.instance.itemGet[keyID] )      //如果这个门对应的钥匙已经得到
                {
                    thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //开门
                    GetComponent<AudioSource>().Play();                              //播放门音效
                    isOpen = true;                                                   //开门状态为true
                }
                else            //否则
                {
                    tips.SetActive(true);             //显示提示
                    StartCoroutine(TipsClose());         //启动关闭携程
                }
            }
        }
    }

    IEnumerator TipsClose()
    {
        yield return new WaitForSeconds(2);           //两秒钟后
        tips.SetActive(false);                        //关闭提示
    }

    public void GetPass(string inputPass)                            //获得密码
    {
        if (inputPass == password)                              //如果输入的密码正确
        {
            doorType = DoorType.norm;                                  //变回普通门
            thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //开门
            GetComponent<AudioSource>().Play();                              //播放门音效
            isOpen = true;                                                   //开门状态为true
        }

        //GameManager.instance.control.gameObject.SetActive(true);        //打开角色
    }

    private void OnTriggerExit(Collider other)       //当触发器物体离开时
    {
        if (other.tag == "Player" && isOpen == true)                   //如果离开的是玩家
        {
            thisDoor.transform.DOLocalMoveX(orgPos, moveTime);                //关门
            GetComponent<AudioSource>().Play();                               //播放门音效
        }
    }
}
