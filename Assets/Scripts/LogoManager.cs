using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().DOFade(1, 1).OnComplete(logoComplete);     //Logo出现动画
    }

    void logoComplete()                      //Logo动画完成
    {
        StartCoroutine(GotoStart());        //启动加载场景携程
    }

    IEnumerator GotoStart()
    {
        AsyncOperation asy = SceneManager.LoadSceneAsync(1);          //加载场景

        while (!asy.isDone)                            //当场景加载中时
        {
            Debug.Log(asy.progress.ToString());     //打印加载过程
            yield return null;                         //异步加载
        }
    }
}
