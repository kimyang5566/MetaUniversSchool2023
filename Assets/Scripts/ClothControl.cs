using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothControl : MonoBehaviour
{
    public Cloth[] cloth;
    public Transform playersRoot;
    public GameObject testCube;
    public static ClothControl instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("RefreshPlayer", 2, 10);
        //cloth[0].capsuleColliders = new CapsuleCollider[20] { testCube.GetComponent<CapsuleCollider>(),new CapsuleCollider(), new CapsuleCollider(), new CapsuleCollider(), new CapsuleCollider(), new CapsuleCollider(), new CapsuleCollider(), new CapsuleCollider(),
        //    new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider(),new CapsuleCollider() };
    }

    // Update is called once per frame
    public void SetPlayer(CapsuleCollider playerCol)
    {
        //GameObject[] charas = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log("player.length " + charas.Length);
        for (int i = 0; i < cloth.Length; i++)
        {
            cloth[i].capsuleColliders = new CapsuleCollider[1] { playerCol };
            
        }
    }
}
