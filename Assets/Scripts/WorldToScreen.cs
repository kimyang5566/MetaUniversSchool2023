using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldToScreen : MonoBehaviour
{
    public GameObject pos;          //������鴥����������Ϊλ��
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(pos.transform.position);       //�����鴥����λ��ת������Ļ����λ�ø�UI
    }

    
}
