using UnityEngine;
using System.Collections;

public class ProbeController : MonoBehaviour
{
    public Transform mirrorPlaneTransform;
    ReflectionProbe probe;

    void Start()
    {
        this.probe = GetComponent<ReflectionProbe>();
    }

    void Update()
    {
        var diffY = mirrorPlaneTransform.position.y - Camera.main.transform.position.y;
        this.probe.transform.position = new Vector3(
            Camera.main.transform.position.x,
            Camera.main.transform.position.y + diffY, //* -1,
            Camera.main.transform.position.z
        );

        probe.RenderProbe();
    }
}