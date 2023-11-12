using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Com.yoyo.MetaSchool;

public enum TriggerType
{
    ShowObj,
    ShowScreen,
    PlayVideo
}

public class ExhibitionTrigger : MonoBehaviour
{
    public bool hasPlayButton;
    public GameObject[] showUIs;
    public TriggerType triggerType;
    public AudioSource audioS;
    public GameObject playButton;
    public bool is3dObj;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void PlayerEnter()
    {
        if (triggerType == TriggerType.ShowObj)
        {
            foreach (GameObject showUI in showUIs)
            {
                showUI.SetActive(true);
                showUI.transform.localScale = new Vector3(0, 0, 0);
                showUI.transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo);
            }

            if (hasPlayButton)
            {
                playButton.SetActive(true);
            }
            //audioS.enabled = true;
            if (!is3dObj)
            {
                GameManager.instance.postBlur.SetActive(true);
            }
            
        }
    }

    public void PlayerExit()
    {
        if (triggerType == TriggerType.ShowObj)
        {
            foreach (GameObject showUI in showUIs)
            {
                showUI.transform.DOScale(0, 0.5f).SetEase(Ease.InExpo).OnComplete(CloseComplete);
            }
            if (hasPlayButton)
            {
                playButton.SetActive(false);
            }
            GameManager.instance.postBlur.SetActive(false);
            //audioS.Stop();
            //audioS.enabled = false;
        }
    }

    void CloseComplete()
    {
        foreach (GameObject showUI in showUIs)
        {
            showUI.SetActive(false);
        }

    }
}
