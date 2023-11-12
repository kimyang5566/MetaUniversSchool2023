using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;  //����ű�ʵ��
    public GameObject packUI;          //���屳��UI
    public GameObject[] allItemsUI;      //�������е���Ʒ
    public bool[] itemGet;             //����������Ʒ���״̬

    private void Awake()
    {
        instance = this;               //���Լ���Ϊʵ��
    }

    // Start is called before the first frame update
    void Start()
    {
        packUI.SetActive(false);        //��Ϸ��ʼʱ�رձ���
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))    //������Tab��
        {
            if (packUI.activeSelf == true)      //�������UI�Ǵ򿪵�
            {
                packUI.transform.DOScaleY(0, 0.2f).OnComplete(CloseFinish);  //���Ŷ���
                
            }
            else                                //����
            {
                packUI.SetActive(true);         //�򿪱���UI
                packUI.transform.DOScaleY(1, 0.2f);      //���Ŷ���
            }
        }
    }

    void CloseFinish()                  //�������ʱ
    {
        packUI.SetActive(false);        //�رձ���UI
    }

    public void ChangeItem(int id, bool v)      //���������ӻ���ٵ���
    {
        itemGet[id] = v;                        //�ı���Ʒ���״̬
        allItemsUI[id].SetActive(v);            //�ı䱳������ƷUI��״̬
    }

    public void ShowPicture(GameObject obj)     //��ʾͼƬ
    {
        obj.SetActive(true);                    //�Ѷ�ӦͼƬ��ʾ
    }
}
