using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject loadingUI;          //����loading�Ľ���
    public Image loadingBar;              //����loading������
    public static bool newGame;           //���徲̬����

    public void NewGame()          //����Ϸ
    {
        newGame = true;                     //��̬��������Ϊ����Ϸ
        loadingUI.SetActive(true);          //��Loading����
        StartCoroutine(GotoStart());        //�������س���Я��
    }

    public void Continue()          //����Ϸ
    {
        newGame = false;                     //��̬��������Ϊ������Ϸ
        loadingUI.SetActive(true);          //��Loading����
        StartCoroutine(GotoStart());        //�������س���Я��
    }
    IEnumerator GotoStart()
    {
        AsyncOperation asy = SceneManager.LoadSceneAsync(2);          //���س���

        while (!asy.isDone)                            //������������ʱ
        {
            loadingBar.fillAmount = asy.progress;      //��ʾ���ع���
            yield return null;                         //�첽����
        }
    }
}
