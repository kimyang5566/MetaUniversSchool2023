using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Com.yoyo.MetaSchool;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;  //����ű�ʵ�� 
    public float moveVal;                 //�����ƶ�����
    public float moveSpeed;               //�����ƶ��ٶ�
    float orgPos;                         //������ʼλ��
    public string[] storyText;            //�����ַ������¼���־���
    public Text storyUI;                  //�����ı�UI
    int storyIndex;                       //����������
    int storyEndNumIndex;                 //����ڼ��ξ���
    public int[] storyEndIndex;           //����ÿ�ξ�������ĳ���
    public GameObject chara1Image;        //�����ɫ1UI
    public GameObject chara2Image;        //�����ɫ2UI
    public int[] showChara;               //����ÿ�仰����ʾ�Ľ�ɫ

    private void Awake()
    {
        instance = this;                            //ʵ����ʼ��
    }

    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.localPosition.y;         //�����ʼλ��
        
    }

    public void Show()                                       //��ʾUI
    {
        transform.DOLocalMoveY(orgPos, moveSpeed);    //�ƶ�����ʼλ��
        ShowText();                                    //��ʾ���ֺͽ�ɫ
    }

    public void Hide()                                       //����UI
    {
        transform.DOLocalMoveY(moveVal, moveSpeed).OnComplete(HideFinish);   //�Ƴ�����
        chara1Image.SetActive(false);             //������ɫ������ʾ
        chara2Image.SetActive(false);
    }

    void HideFinish()                  //�����鴰���Ƴ���������ʱ
    {
        gameObject.SetActive(false);   //�رչ��´���
    }

    public void ShowText()                        //��ʾ���ֺͽ�ɫ
    {
        storyUI.text = storyText[storyIndex];         //ͨ���������ҵ��ڼ������ֲ���ʾ��UI�ı���
        if (showChara[storyIndex] == 0)
        {
            chara1Image.SetActive(false);             //������ɫ������ʾ
            chara2Image.SetActive(false);
        }
        else if (showChara[storyIndex] == 1)
        {
            chara1Image.SetActive(true);              //��ʾ��һ����ɫ
            chara2Image.SetActive(false);
        }
        else
        {
            chara1Image.SetActive(false);             //��ʾ�ڶ�����ɫ
            chara2Image.SetActive(true);
        }
        storyIndex++;                                     //�����ż�1
    }

    public void ClickButton()                         //����ı���ť
    {
        if (storyIndex < storyEndIndex[storyEndNumIndex])  //�����ǰ��������������δ������ξ���ĳ���
        {
            ShowText();                     //��ʾ���ֺͽ�ɫ
        }
        else                                              //����
        {
            GameManager.instance.StopStory();             //ֹͣ���飬������Ϸ
            storyEndNumIndex++;                           //ÿ�ξ����1
        }
    }
}
