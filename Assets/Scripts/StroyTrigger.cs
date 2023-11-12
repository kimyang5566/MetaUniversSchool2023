using Com.yoyo.MetaSchool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StoryType         //定义剧情类型
{
    TextStory,                //文字型
    AnimationStory,           //镜头动画和人物动画
    JumpScene                 //跳转场景型
}

public class StroyTrigger : MonoBehaviour
{
    public GameObject nextTrigger;                       //定义下一个剧情触发器
    public int gotoSceneID;                              //将要跳转的场景ID
    public StoryType storyType;                          //定义剧情的类型
    public Image image;             //定义UI图像组件

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();      //获得自身UI图像组件
    }

    private void OnBecameVisible()      //当物体进入摄像机视野
    {
        image.enabled = true;           //UI图像组件打开
        Debug.Log("Visible");
    }

    private void OnBecameInvisible()   //当物体离开摄像机视野
    {
        image.enabled = false;         //UI图像组件关闭
        Debug.Log("inVisible");
    }

    private void OnTriggerEnter(Collider other)         //触发器有物体进入时
    {
        if (other.tag == "Player")                      //如果进入物体为Player
        {
            if (storyType == StoryType.TextStory )        //如果是文字类剧情
            {
                GameManager.instance.StartStory();          //开启剧情
                nextTrigger.SetActive(true);                //激活下一个剧情触发器
                gameObject.SetActive(false);                //关闭此剧情触发器
            }
            else if (storyType == StoryType.JumpScene)      //否则如果跳转场景型
            {
                SceneManager.LoadScene(gotoSceneID);        //跳场景
            }
            else
            {

            }
        }
    }
}
