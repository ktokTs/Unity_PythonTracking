using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using System.Linq;
using System;

public class PythonProgram
{
    Process MainProcess;
    Process PythonProcess;
    TcpClient Client;
    IPEndPoint RemoteEP;
    TcpListener Listener;
    string ProgramName;
    string UsingIPAddress;
    int UsingPort;
    string ProgramPass;
    bool IsConnection;
    Thread TCPThread;
    public event Action<string> ResponceEvents;

    public PythonProgram(string ProgramName, string UsingIPAddress, int UsingPort, string ProgramPass)
    {
        this.ProgramName = ProgramName;
        this.UsingIPAddress = UsingIPAddress;
        this.UsingPort = UsingPort;
        this.ProgramPass = ProgramPass;
        RemoteEP = new IPEndPoint(IPAddress.Any, UsingPort);
    }

    public void StartPythonProgram()
    {
        if (MainProcess == null)
        {
            MainProcess = new Process();
            MainProcess.StartInfo.FileName = ProgramPass;
            MainProcess.StartInfo.UseShellExecute = true;
            //MainProcess.StartInfo.Arguments = ;

            MainProcess.EnableRaisingEvents = true;
            MainProcess.Exited += MainProcess_Exited;

            MainProcess.Start();

            TCPThread= new Thread(new ThreadStart(Update));
            TCPThread.Start();
        }
    }

    public void EndProcess()
    {
        if (MainProcess != null && MainProcess.HasExited == false)
        {
            IsConnection = false;
            Client.Close();
            MainProcess.CloseMainWindow();
            MainProcess.Dispose();
            UnityEngine.Debug.Log(ProgramName + ": End");
        }
    }

    void MainProcess_Exited(object sender, System.EventArgs e)
    {
        IsConnection = false;
        Client.Close();
        MainProcess.CloseMainWindow();
        MainProcess.Dispose();
        UnityEngine.Debug.Log(ProgramName + ": Exited");
    }

    public void Update()
    {
        try
        {
            Listener = new TcpListener(RemoteEP);
            Listener.Start();
            Client = Listener.AcceptTcpClient();
            IsConnection = true;
            NetworkStream Stream = Client.GetStream();

            GetPID(Stream);
            while(IsConnection)
            {
                Byte[] data = new Byte[20000];
                String RawResponseData = String.Empty;
                Int32 bytes = Stream.Read(data, 0, data.Length);
                RawResponseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                ResponceEvents(RawResponseData);
            }
        }
        catch(System.IO.IOException e)
        {
            UnityEngine.Debug.Log("SocketException happened\n" + e.Message);
        }
        UnityEngine.Debug.Log("IsConnection: false");
    }

    void GetPID(NetworkStream Stream)
    {
        Byte[] data = new Byte[256];
        String responseData = String.Empty;
        Int32 bytes = Stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        UnityEngine.Debug.Log("PID: " +  responseData);

        PythonProcess = Process.GetProcessById(Int32.Parse(responseData));
        Byte[] buffer = System.Text.Encoding.ASCII.GetBytes("responce: " + responseData);
        Stream.Write(buffer, 0, buffer.Length);
    }
}

[System.Serializable]
public class HandsTest
{
    string Index;
    public HandsTestPoint Point;
    public void Show()
    {
        Point.Show();
    }
}

[System.Serializable]
public class HandsTestPoint
{
    public float x;
    public float y;
    public float z;
    public void Show()
    {
        UnityEngine.Debug.Log("x:" + x + "\ny:" + y + "\nz:" + z);
    }
}

/// <summary>
/// <see cref="JsonUtility"/> に不足している機能を提供します。
/// </summary>
public static class JsonHelper
{
    /// <summary>
    /// 指定した string を Root オブジェクトを持たない JSON 配列と仮定してデシリアライズします。
    /// </summary>
    public static T[] FromJson<T>(string json)
    {
        // ルート要素があれば変換できるので
        // 入力されたJSONに対して(★)の行を追加する
        //
        // e.g.
        // ★ {
        // ★     "array":
        //        [
        //            ...
        //        ]
        // ★ }
        //
        string dummy_json = $"{{\"{DummyNode<T>.ROOT_NAME}\": {json}}}";

        // ダミーのルートにデシリアライズしてから中身の配列を返す
        var obj = JsonUtility.FromJson<DummyNode<T>>(dummy_json);
        return obj.array;
    }

    /// <summary>
    /// 指定した配列やリストなどのコレクションを Root オブジェクトを持たない JSON 配列に変換します。
    /// </summary>
    /// <remarks>
    /// 'prettyPrint' には非対応。整形したかったら別途変換して。
    /// </remarks>
    public static string ToJson<T>(IEnumerable<T> collection)
    {
        string json = JsonUtility.ToJson(new DummyNode<T>(collection)); // ダミールートごとシリアル化する
        int start = DummyNode<T>.ROOT_NAME.Length + 4;
        int len = json.Length - start - 1;
        return json.Substring(start, len); // 追加ルートの文字を取り除いて返す
    }

    // 内部で使用するダミーのルート要素
    [Serializable]
    private struct DummyNode<T>
    {
        // 補足:
        // 処理中に一時使用する非公開クラスのため多少設計が変でも気にしない

        // JSONに付与するダミールートの名称
        public const string ROOT_NAME = nameof(array);
        // 疑似的な子要素
        public T[] array;
        // コレクション要素を指定してオブジェクトを作成する
        public DummyNode(IEnumerable<T> collection) => this.array = collection.ToArray();
    }
}