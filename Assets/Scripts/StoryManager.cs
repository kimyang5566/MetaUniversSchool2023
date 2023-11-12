using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Com.yoyo.MetaSchool;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;  //定义脚本实例 
    public float moveVal;                 //定义移动距离
    public float moveSpeed;               //定义移动速度
    float orgPos;                         //定义起始位置
    public string[] storyText;            //定义字符数组记录文字剧情
    public Text storyUI;                  //定义文本UI
    int storyIndex;                       //定义索引号
    int storyEndNumIndex;                 //定义第几段剧情
    public int[] storyEndIndex;           //定义每段剧情结束的长度
    public GameObject chara1Image;        //定义角色1UI
    public GameObject chara2Image;        //定义角色2UI
    public int[] showChara;               //定义每句话所显示的角色

    private void Awake()
    {
        instance = this;                            //实例初始化
    }

    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.localPosition.y;         //获得起始位置
        
    }

    public void Show()                                       //显示UI
    {
        transform.DOLocalMoveY(orgPos, moveSpeed);    //移动到起始位置
        ShowText();                                    //显示文字和角色
    }

    public void Hide()                                       //隐藏UI
    {
        transform.DOLocalMoveY(moveVal, moveSpeed).OnComplete(HideFinish);   //移出画面
        chara1Image.SetActive(false);             //两个角色都不显示
        chara2Image.SetActive(false);
    }

    void HideFinish()                  //当剧情窗口移出动画结束时
    {
        gameObject.SetActive(false);   //关闭故事窗口
    }

    public void ShowText()                        //显示文字和角色
    {
        storyUI.text = storyText[storyIndex];         //通过索引号找到第几行文字并显示在UI文本中
        if (showChara[storyIndex] == 0)
        {
            chara1Image.SetActive(false);             //两个角色都不显示
            chara2Image.SetActive(false);
        }
        else if (showChara[storyIndex] == 1)
        {
            chara1Image.SetActive(true);              //显示第一个角色
            chara2Image.SetActive(false);
        }
        else
        {
            chara1Image.SetActive(false);             //显示第二个角色
            chara2Image.SetActive(true);
        }
        storyIndex++;                                     //索引号加1
    }

    public void ClickButton()                         //点击文本按钮
    {
        if (storyIndex < storyEndIndex[storyEndNumIndex])  //如果当前剧情文字索引号未超出这段剧情的长度
        {
            ShowText();                     //显示文字和角色
        }
        else                                              //否则
        {
            GameManager.instance.StopStory();             //停止剧情，继续游戏
            storyEndNumIndex++;                           //每段剧情加1
        }
    }
}
