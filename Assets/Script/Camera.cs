using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Vector3 StartPos;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    bool Trace;

    void Start()
    {
        StartPos = Target.transform.position - this.transform.position;
    }

    void Update()
    {
        if (!Trace)
            return;
        this.transform.position = Target.transform.position - StartPos;
    }
}
