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
        GetComponent<Image>().DOFade(1, 1).OnComplete(logoComplete);     //Logo���ֶ���
    }

    void logoComplete()                      //Logo�������
    {
        StartCoroutine(GotoStart());        //�������س���Я��
    }

    IEnumerator GotoStart()
    {
        AsyncOperation asy = SceneManager.LoadSceneAsync(1);          //���س���

        while (!asy.isDone)                            //������������ʱ
        {
            Debug.Log(asy.progress.ToString());     //��ӡ���ع���
            yield return null;                         //�첽����
        }
    }
}
