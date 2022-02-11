using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    GameObject[] HandPoint;
    // Start is called before the first frame update
    void Start()
    {
        HandPoint = new GameObject[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            HandPoint[i] = this.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    public void MovePoint(HandsTest[] MovePoints)
    {
        int index = 0;
        Debug.Log("a");
        foreach(HandsTest MovePoint in MovePoints)
        {
            Debug.Log(HandPoint[index].name);
            HandsTestPoint Point = MovePoint.Point;
            HandPoint[index].transform.position = new Vector3(Point.x, -Point.y, Point.z);
            index++;
        }
    }
}
