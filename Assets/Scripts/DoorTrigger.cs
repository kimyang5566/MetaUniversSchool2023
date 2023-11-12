using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;              //ʹ��dotween�����

public enum DoorType                   //�����ŵ�����
{
    norm,                              //��ͨ��
    key,                               //��ҪԿ�״򿪵���
    password                           //������
}

public class DoorTrigger : MonoBehaviour
{
    public GameObject thisDoor;          //����������
    public float moveDistance;           //�����ƶ�����
    public float moveTime;               //����ʱ��
    float orgPos;                        //������ʼλ��
    public DoorType doorType;            //��������ŵ�����
    public string password;              //��������ŵ�����
    public int keyID;                    //������Ŷ�ӦԿ��
    public GameObject tips;              //������ʾ����
    public bool isOpen;                  //�����ŵĿ���״̬

    // Start is called before the first frame update
    void Start()
    {
        orgPos = thisDoor.transform.localPosition.x;    //��ԭ����λ�ø���ʼλ�ñ���
    }

    private void OnTriggerEnter(Collider other)       //�����������������ʱ
    {
        if (other.tag == "Player")                    //�������������
        {
            if (doorType == DoorType.norm)
            {
                thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //����
                GetComponent<AudioSource>().Play();                              //��������Ч
                isOpen = true;                                                   //����״̬Ϊtrue
            }
            else if (doorType == DoorType.password)
            {
                //GameManager.instance.control.gameObject.SetActive(false);        //�رս�ɫ
                Password.instance.door = gameObject;          //���Ÿ�������ű�
                Debug.Log(Password.instance);
                Password.instance.OpenUI();                   //������UI
            }
            else                          //�����Կ����
            {
                if ( PackManager.instance.itemGet[keyID] )      //�������Ŷ�Ӧ��Կ���Ѿ��õ�
                {
                    thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //����
                    GetComponent<AudioSource>().Play();                              //��������Ч
                    isOpen = true;                                                   //����״̬Ϊtrue
                }
                else            //����
                {
                    tips.SetActive(true);             //��ʾ��ʾ
                    StartCoroutine(TipsClose());         //�����ر�Я��
                }
            }
        }
    }

    IEnumerator TipsClose()
    {
        yield return new WaitForSeconds(2);           //�����Ӻ�
        tips.SetActive(false);                        //�ر���ʾ
    }

    public void GetPass(string inputPass)                            //�������
    {
        if (inputPass == password)                              //��������������ȷ
        {
            doorType = DoorType.norm;                                  //�����ͨ��
            thisDoor.transform.DOLocalMoveX(moveDistance, moveTime);         //����
            GetComponent<AudioSource>().Play();                              //��������Ч
            isOpen = true;                                                   //����״̬Ϊtrue
        }

        //GameManager.instance.control.gameObject.SetActive(true);        //�򿪽�ɫ
    }

    private void OnTriggerExit(Collider other)       //�������������뿪ʱ
    {
        if (other.tag == "Player" && isOpen == true)                   //����뿪�������
        {
            thisDoor.transform.DOLocalMoveX(orgPos, moveTime);                //����
            GetComponent<AudioSource>().Play();                               //��������Ч
        }
    }
}
