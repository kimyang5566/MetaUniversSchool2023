using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Begin();
    }

    void Begin()
    {
        transform.DOLocalMoveX(transform.localPosition.x + 1, 1.5f).OnComplete(Finish);
    }

    void Finish()
    {
        transform.DOLocalMoveX(transform.localPosition.x - 1, 1.5f).OnComplete(Begin);
    }

}
