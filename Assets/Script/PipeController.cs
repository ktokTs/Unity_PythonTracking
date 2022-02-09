using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    Python PythonPipe = new Python();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PythonPipe.StartProcess();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PythonPipe.EndProcess();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PythonPipe.CheckProcess();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PythonPipe.Update();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PythonPipe.GetPID();
        }
    }
}
