using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherUIManager : MonoBehaviour
{
    public GameObject[] loginUIs;
    public GameObject[] selectCharaUIs;
    public GameObject[] finalUIs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GotoStep(int step)
    {
        if (step == 1)
        {
            UISetActive(loginUIs, true);
            UISetActive(selectCharaUIs, false);
            UISetActive(finalUIs, false);
        }
        else if (step == 2)
        {
            UISetActive(loginUIs, false);
            UISetActive(selectCharaUIs, true);
            UISetActive(finalUIs, false);
        }
        else if (step == 3)
        {
            UISetActive(loginUIs, false);
            UISetActive(selectCharaUIs, false);
            UISetActive(finalUIs, true);
        }
    }

    void UISetActive(GameObject[] uis,bool var)
    {
        foreach (GameObject item in uis)
        {
            item.SetActive(var);
        }
    }
}
