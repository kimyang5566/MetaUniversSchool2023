using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    public GameObject[] textModels;

    // Start is called before the first frame update
    void Start()
    {
        PlayerExit();
    }

    public void PlayerEnter()
    {
       foreach (GameObject item in textModels)
       {
           item.SetActive(true);
       }
    }

    public void PlayerExit()
    {
        foreach (GameObject item in textModels)
        {
            item.SetActive(false);
        }
    }
}
