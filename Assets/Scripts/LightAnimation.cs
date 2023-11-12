using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightDataList
{
    public GameObject[] lightDataList;
    public GameObject this[int index]
    {
        get
        {
            return lightDataList[index];
        }
    }
    public void LightData(int index)
    {
        this.lightDataList = new GameObject[index];
    }
} 

public class LightAnimation : MonoBehaviour
{
    public LightDataList[] lights;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LightShine", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void LightShine()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                lights[i].lightDataList[j].GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.red);
            }
        }
        for (int m = 0; m < 4; m++)
        {
            lights[index].lightDataList[m].GetComponent<Renderer>().materials[1].SetColor("_EmissionColor", Color.white);
        }
        if (index >=4 )
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }
}
