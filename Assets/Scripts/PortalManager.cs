using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.yoyo.MetaSchool;
using DG.Tweening;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    public Transform gotoPosition;
    public float portalTime;

    

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.portalEffect.SetActive(true);
        //GameManager.instance.portalEffect.GetComponent<Image>().DOFade(1, 1.0f);
        StartCoroutine(PortalTime());
        
    }
                                                                                                                        
    IEnumerator PortalTime()
    {
        yield return new WaitForSeconds(portalTime);
        GameManager.instance.localPlayer.transform.position = gotoPosition.position + new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        GameManager.instance.localPlayer.transform.rotation = gotoPosition.rotation;
        FadeComplete();
        //GameManager.instance.portalEffect.GetComponent<Image>().DOFade(0, 1.0f).OnComplete(FadeComplete);
    }

    void FadeComplete()
    {
        Debug.Log("PortalFinish");
        GameManager.instance.portalEffect.SetActive(false);
    }
}
