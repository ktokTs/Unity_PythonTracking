using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class PipeController : MonoBehaviour
{
    [SerializeField]
    GameObject HandObject;
    Hand HandScript;
    PythonProgram PythonProgram = new PythonProgram("Python Program", "127.0.0.1", 50007, "C:\\Users\\anpan\\programing\\python\\Unity\\dist\\main\\main.exe");
    // Start is called before the first frame update

    SynchronizationContext MainThread;
    string str;
    void Start()
    {
        MainThread = SynchronizationContext.Current;
        HandScript = HandObject.GetComponent<Hand>();
        PythonProgram.ResponceEvents += InvokeMethod;
    }

    void InvokeMethod(string str)
    {
        this.str = str;
        MainThread.Post(__ =>
        {
            ResponceEvents();
        }, null);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PythonProgram.StartPythonProgram();
        } 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PythonProgram.EndProcess();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PythonProgram.Update();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Test();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HandsTest a = new HandsTest();
            a.Point = new HandsTestPoint();
            a.Point.x = 1;
            a.Point.y = 1;
            a.Point.z = 1;
            HandsTest[] b = new HandsTest[1];
            b[0] = a;
            HandScript.MovePoint(b);
        }

    }

    void ResponceEvents()
    {
        string Data = str;
        //print(Data);
        if (Data == null || Data == "0")
            return;
        HandsTest[] HandsPoints = JsonHelper.FromJson<HandsTest>(Data);
        // foreach(HandsTest HandsPoint in HandsPoints)
            // HandsPoint.Show();
        HandScript.MovePoint(HandsPoints);
    }
    void Test()
    {
        string Data = "[{\"Index\": 0, \"Point\": {\"x\": 0.5811007022857666, \"y\": 0.492609441280365, \"z\": 5.026776648264786e-07}}, {\"Index\": 1, \"Point\": {\"x\": 0.5172950029373169, \"y\": 0.5353304147720337, \"z\": -0.034726519137620926}}]";
        //string Data = "\"a\": [1, 2, 3]";
        HandsTest[] array = JsonHelper.FromJson<HandsTest>(Data);
        // foreach(HandsTest test in array)
            // test.Show();
        
        HandScript.MovePoint(array);
    }
}


