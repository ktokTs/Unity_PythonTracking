using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System;

public class Python
{
    /// <summary>
    ///起動する外部プロセス
    /// </summary>
    Process exProcess;
    Process PythonProcess;
    static UdpClient MainUdp;
    static UdpClient GetPIDUdp;
    IPEndPoint remoteEP = null;
    int i = 0;
    
    bool IsConnection;
    Thread receiveThread;
    
    // Update is called once per frame
    public void StartProcess()
    {
        if (exProcess == null)
        {
            exProcess = new Process();
            exProcess.StartInfo.FileName = "C:\\Users\\anpan\\programing\\python\\Unity\\dist\\main.exe";
            //exProcess.StartInfo.Arguments = arg;
    
            //外部プロセスの終了を検知してイベントを発生させます.
            exProcess.EnableRaisingEvents = true;
            exProcess.Exited += exProcess_Exited;
            
    
            //外部のプロセスを実行する
            exProcess.Start();
            UDPStart();
            PythonProcess = GetPID();
            receiveThread= new Thread(new ThreadStart(Update));
            receiveThread.Start();
        }
    }

    public void EndProcess()
    {
            UnityEngine.Debug.Log(exProcess.Id);
            UnityEngine.Debug.Log(exProcess.ProcessName);
        UnityEngine.Debug.Log("End2");
        UnityEngine.Debug.Log(exProcess.Id);
        if (exProcess.HasExited == false)
        {
            UnityEngine.Debug.Log("End2");
            PythonProcess.CloseMainWindow();
            PythonProcess.Dispose();
            exProcess.CloseMainWindow();
            exProcess.Dispose();
            //exProcess.Kill();
            //exProcess = null;
        }
    }

    public void CheckProcess()
    {
        UnityEngine.Debug.Log("Check");
        if (exProcess == null)
            return;
        UnityEngine.Debug.Log(exProcess.HasExited);
        UnityEngine.Debug.Log("End2");
        IsConnection = false;
        exProcess.CloseMainWindow();
        exProcess.Dispose();
    }
    
    void exProcess_Exited(object sender, System.EventArgs e)
    {
        UnityEngine.Debug.Log("Event!");
        exProcess.CloseMainWindow();
        exProcess.Dispose();
        IsConnection = false;
        //exProcess = null;
    }

    void UDPStart ()
    {
        int LOCA_LPORT = 50007;
        MainUdp = new UdpClient(LOCA_LPORT);
        MainUdp.Client.ReceiveTimeout = 5000;
        GetPIDUdp = new UdpClient(50006);
        GetPIDUdp.Client.ReceiveTimeout = 5000;
    }

    public void Update()
    {
        while (IsConnection)
        {
            try
            {
                IPEndPoint remoteEP = null;
                byte[] data = MainUdp.Receive(ref remoteEP);
                string text = Encoding.UTF8.GetString(data);

                
                PythonTest a = JsonUtility.FromJson<PythonTest>(text);//読み込んだJSONファイルをPlayerData型に変換して返す
                a.Show();
            }
            catch (SocketException e)
            {
                UnityEngine.Debug.Log("接続の終了");
                return;
            }
        }
    }

    public Process GetPID()
    {
        IPEndPoint remoteEP = null;
        byte[] data = GetPIDUdp.Receive(ref remoteEP);
        string text = Encoding.UTF8.GetString(data);
        IsConnection = true;
        return Process.GetProcessById(Int32.Parse(text));
    }
}

[System.Serializable]
public class PythonTest
{
    public int Count;
    public int Num;
    public void Show()
    {
        UnityEngine.Debug.Log("Count " + Count);
        UnityEngine.Debug.Log("Num " + Num);
    }
}

