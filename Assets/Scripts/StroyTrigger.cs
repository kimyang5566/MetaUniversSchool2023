using Com.yoyo.MetaSchool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StoryType         //�����������
{
    TextStory,                //������
    AnimationStory,           //��ͷ���������ﶯ��
    JumpScene                 //��ת������
}

public class StroyTrigger : MonoBehaviour
{
    public GameObject nextTrigger;                       //������һ�����鴥����
    public int gotoSceneID;                              //��Ҫ��ת�ĳ���ID
    public StoryType storyType;                          //������������
    public Image image;             //����UIͼ�����

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponentInChildren<Image>();      //�������UIͼ�����
    }

    private void OnBecameVisible()      //����������������Ұ
    {
        image.enabled = true;           //UIͼ�������
        Debug.Log("Visible");
    }

    private void OnBecameInvisible()   //�������뿪�������Ұ
    {
        image.enabled = false;         //UIͼ������ر�
        Debug.Log("inVisible");
    }

    private void OnTriggerEnter(Collider other)         //���������������ʱ
    {
        if (other.tag == "Player")                      //�����������ΪPlayer
        {
            if (storyType == StoryType.TextStory )        //��������������
            {
                GameManager.instance.StartStory();          //��������
                nextTrigger.SetActive(true);                //������һ�����鴥����
                gameObject.SetActive(false);                //�رմ˾��鴥����
            }
            else if (storyType == StoryType.JumpScene)      //���������ת������
            {
                SceneManager.LoadScene(gotoSceneID);        //������
            }
            else
            {

            }
        }
    }
}
