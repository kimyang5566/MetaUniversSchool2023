using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorControl : MonoBehaviour
{
    public Vector2 doorMoveData;
    public float doorMoveTime;
    public GameObject leftDoor;
    public GameObject rightDoor;
    Vector2 orgPositionX;

    // Start is called before the first frame update
    void Start()
    {
        orgPositionX = new Vector2(leftDoor.transform.localPosition.x, rightDoor.transform.localPosition.x);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            leftDoor.transform.DOLocalMoveX(doorMoveData.x, doorMoveTime);
            rightDoor.transform.DOLocalMoveX(doorMoveData.y, doorMoveTime);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            leftDoor.transform.DOLocalMoveX(orgPositionX.x, doorMoveTime);
            rightDoor.transform.DOLocalMoveX(orgPositionX.y, doorMoveTime);
        }
    }
}
