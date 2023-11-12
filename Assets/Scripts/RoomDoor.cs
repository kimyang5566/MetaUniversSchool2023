using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoomDoor : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float moveVal;
    public float moveTime;
    public Vector2 orgVal;

    // Start is called before the first frame update
    void Start()
    {
        orgVal.x = leftDoor.transform.localPosition.y;
        orgVal.y = rightDoor.transform.localPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            leftDoor.transform.DOLocalMoveY(orgVal.x + moveVal, moveTime);
            rightDoor.transform.DOLocalMoveY(orgVal.y - moveVal, moveTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            leftDoor.transform.DOLocalMoveY(orgVal.x, moveTime);
            rightDoor.transform.DOLocalMoveY(orgVal.y, moveTime);
        }
    }
}
