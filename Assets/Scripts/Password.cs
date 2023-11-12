using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    public static Password instance;     //定义实例
    public Text screenTxt;               //定义屏幕UI
    public GameObject door;              //定义密码关联的门
    public GameObject passUI;            //定义密码UI
    
    void Awake()
    {
        instance = this;                 //自己作为实例
    }

    public void OpenUI()
    {
        passUI.SetActive(true);          //打开密码窗口UI
    }

    public void PressButton( string v )    //按下按钮
    {
        screenTxt.text = screenTxt.text + v;     //屏幕密码增加
    }

    public void ClearScreen()              //按下清空按钮
    {
        screenTxt.text = "";               //清空屏幕文字
    }

    public void Close()
    {
        passUI.SetActive(false);         //关闭密码窗口
    }

    public void SendPass()               //点击确认发送密码
    {
        door.SendMessage("GetPass", screenTxt.text);     //给门发送消息，将密码发送到门
        Close();
    }
}
