using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject loadingUI;          //定义loading的界面
    public Image loadingBar;              //定义loading进度条
    public static bool newGame;           //定义静态变量

    public void NewGame()          //新游戏
    {
        newGame = true;                     //静态变量设置为新游戏
        loadingUI.SetActive(true);          //打开Loading界面
        StartCoroutine(GotoStart());        //启动加载场景携程
    }

    public void Continue()          //新游戏
    {
        newGame = false;                     //静态变量设置为继续游戏
        loadingUI.SetActive(true);          //打开Loading界面
        StartCoroutine(GotoStart());        //启动加载场景携程
    }
    IEnumerator GotoStart()
    {
        AsyncOperation asy = SceneManager.LoadSceneAsync(2);          //加载场景

        while (!asy.isDone)                            //当场景加载中时
        {
            loadingBar.fillAmount = asy.progress;      //显示加载过程
            yield return null;                         //异步加载
        }
    }
}
