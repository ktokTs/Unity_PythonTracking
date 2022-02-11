using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Vector3 StartPos;
    [SerializeField]
    GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = Target.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Target.transform.position - StartPos;
    }
}
