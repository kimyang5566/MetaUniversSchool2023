using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    public static Password instance;     //����ʵ��
    public Text screenTxt;               //������ĻUI
    public GameObject door;              //���������������
    public GameObject passUI;            //��������UI
    
    void Awake()
    {
        instance = this;                 //�Լ���Ϊʵ��
    }

    public void OpenUI()
    {
        passUI.SetActive(true);          //�����봰��UI
    }

    public void PressButton( string v )    //���°�ť
    {
        screenTxt.text = screenTxt.text + v;     //��Ļ��������
    }

    public void ClearScreen()              //������հ�ť
    {
        screenTxt.text = "";               //�����Ļ����
    }

    public void Close()
    {
        passUI.SetActive(false);         //�ر����봰��
    }

    public void SendPass()               //���ȷ�Ϸ�������
    {
        door.SendMessage("GetPass", screenTxt.text);     //���ŷ�����Ϣ�������뷢�͵���
        Close();
    }
}
