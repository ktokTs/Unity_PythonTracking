using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestScript
{
    string SimpleTestJsonData = "{\"Num\":10, \"String\":\"Test\"}";
    string ArrayTestJsonData = "{\"Nums\":[10, 20], \"String\":\"Test\"}";

    [Test]
    public void Simple()
    {
        TestSimpleClass Res = JsonUtility.FromJson<TestSimpleClass>(SimpleTestJsonData);
        Res.Show();

        TestSimpleClass NewInstance = new TestSimpleClass();
        NewInstance.Num = 5;
        NewInstance.String = "New Test";
        string NewJson = JsonUtility.ToJson(NewInstance);
        Debug.Log(NewJson);
    }

    [Test]
    public void Array()
    {
        TestArrayClass Res = JsonUtility.FromJson<TestArrayClass>(ArrayTestJsonData);
        Res.Show();

        TestArrayClass NewInstance = new TestArrayClass();
        NewInstance.Nums = new int[]{1, 2};
        NewInstance.String = "New Test";
        string NewJson = JsonUtility.ToJson(NewInstance);
        Debug.Log(NewJson);
    }

}


[System.Serializable]
public class TestSimpleClass
{
    public int Num;
    public string String;
    public void Show()
    {
        Debug.Log("Num: " + Num + ", String: " + String);
    }
}

public class TestArrayClass
{
    public int[] Nums;
    public string String;
    public void Show()
    {
        int index = 0;
        foreach (int Num in Nums)
        {
            Debug.Log("Index: " + index +", Num: " + Num);
            index++;
        }
        Debug.Log("String: " + String);
    }
}


    // string TestJsonData = "[{\"Index\": 0, \"Point\": {\"x\": 0.5748553276062012, \"y\": 0.22392258048057556, \"z\": 1.6189521545584284e-07}}, {\"Index\": 1, \"Point\": {\"x\": 0.5047998428344727, \"y\": 0.23730874061584473, \"z\": 0.007076524663716555}}, {\"Index\": 2, \"Point\": {\"x\": 0.46768999099731445, \"y\": 0.3166828155517578, \"z\": 0.01802011765539646}}, {\"Index\": 3, \"Point\": {\"x\": 0.45838671922683716, \"y\": 0.40557312965393066, \"z\": 0.029370876029133797}}, {\"Index\": 4, \"Point\": {\"x\": 0.4551030695438385, \"y\": 0.46735456585884094, \"z\": 0.041305724531412125}}, {\"Index\": 5, \"Point\": {\"x\": 0.49175259470939636, \"y\": 0.42615625262260437, \"z\": 0.013983598910272121}}, {\"Index\": 6, \"Point\": {\"x\": 0.4780159592628479, \"y\": 0.5371034741401672, \"z\": 0.021876811981201172}}, {\"Index\": 7, \"Point\": {\"x\": 0.47141900658607483, \"y\": 0.594441831111908, \"z\": 0.02707100100815296}}, {\"Index\": 8, \"Point\": {\"x\": 0.4678054451942444, \"y\": 0.6380933523178101, \"z\": 0.03037109784781933}}, {\"Index\": 9, \"Point\": {\"x\": 0.5263741612434387, \"y\": 0.454677015542984, \"z\": 0.01343383826315403}}, {\"Index\": 10, \"Point\": {\"x\": 0.5063556432723999, \"y\": 0.5701847672462463, \"z\": 0.019337961450219154}}, {\"Index\": 11, \"Point\": {\"x\": 0.4943229556083679, \"y\": 0.6306325197219849, \"z\": 0.021766746416687965}}, {\"Index\": 12, \"Point\": {\"x\": 0.483406126499176, \"y\": 0.6736308336257935, \"z\": 0.023746704682707787}}, {\"Index\": 13, \"Point\": {\"x\": 0.5569302439689636, \"y\": 0.4643248915672302, \"z\": 0.013633569702506065}}, {\"Index\": 14, \"Point\": {\"x\": 0.5362354516983032, \"y\": 0.5678220391273499, \"z\": 0.018914131447672844}}, {\"Index\": 15, \"Point\": {\"x\": 0.5214012861251831, \"y\": 0.6239602565765381, \"z\": 0.018875520676374435}}, {\"Index\": 16, \"Point\": {\"x\": 0.5084859132766724, \"y\": 0.6665945053100586, \"z\": 0.018645387142896652}}, {\"Index\": 17, \"Point\": {\"x\": 0.5849424004554749, \"y\": 0.4574938714504242, \"z\": 0.014741094782948494}}, {\"Index\": 18, \"Point\": {\"x\": 0.5671853423118591, \"y\": 0.5459781885147095, \"z\": 0.02059382013976574}}, {\"Index\": 19, \"Point\": {\"x\": 0.5549178123474121, \"y\": 0.593900203704834, \"z\": 0.023901639506220818}}, {\"Index\": 20, \"Point\": {\"x\": 0.543161928653717, \"y\": 0.6303700804710388, \"z\": 0.026699110865592957}}]";
