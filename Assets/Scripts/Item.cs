using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject pickText;         //����ʰȡ����
    public int id;                      //���屾����id
    public GameObject openTrigger;      //�����Ƿ��ܴ򿪴�����
    public GameObject sound;            //��������

    // Start is called before the first frame update
    void Start()
    {
        pickText.SetActive(false);          //��ʼ��ʱ�ر�����
    }

    private void OnTriggerEnter(Collider other)     //����������ʱ
    {
        if (other.tag == "Player")                 //�жϽ��������ǲ������
        {
            pickText.SetActive(true);              //��ʾʰȡ����
        }
    }

    private void OnTriggerExit(Collider other)      //�������뿪ʱ
    {
        if (other.tag == "Player")                  //�жϽ��������ǲ������
        {
            pickText.SetActive(false);              //����ʰȡ����
        }
    }

    private void Update()
    {
        if (pickText.activeSelf == true)           //������Ѿ���ʾ
        {
            if (Input.GetKeyDown(KeyCode.F))       //�������F��ʰȡ
            {
                PackManager.instance.ChangeItem(id, true);  //���������л�õ���
                if (openTrigger != null)                    //����й���������
                {
                    openTrigger.SetActive(true);            //�򿪴�����
                    Instantiate(sound, transform.position, transform.rotation);      //������Ԥ��ʵ��������������������
                }
                gameObject.SetActive(false);                //������ʧ
            }
        }
    }
}
