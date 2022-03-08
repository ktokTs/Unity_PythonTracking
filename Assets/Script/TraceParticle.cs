using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceParticle : MonoBehaviour
{
    GameObject[] HandPoint;
    [SerializeField, Range(0, 10000)]
    float Ratio;
    [SerializeField, Range(0, 10000)]
    float XYRatio;
    Vector2 OffSet;
    Vector2[] LinearTrans = new Vector2[4];

    Transform MyTransform;

    int i;

    List<float[]> Sample = new List<float[]>{
    new float[]{32.93521404f, 52.08194256f}, // Right
    new float[]{31.74056709f, 53.68223786f}, // Up
    new float[]{30.5018872f, 52.53005624f}, // Left
    new float[]{31.6133827f, 51.15163922f}, // Down
    new float[]{31.69776276f, 52.36146897f}, // OffSet
    };

    void Start()
    {
        OffSet = new Vector2();
        MyTransform = this.transform;
    }

    public void MovePoint(Hands[] MovePoints)
    {
        Hands RightEye = MovePoints[473];
        Hands LeftEye = MovePoints[468];
        Hands TopEye = MovePoints[362];
        Hands TailEye = MovePoints[263];

        // Hands RightEye = MovePoints[0];
        // i++;
        // RightEye.Point.x = Sample[i % 4][0];
        // RightEye.Point.y = Sample[i % 4][1];

        Vector2 Center = new Vector2(TailEye.Point.x + TopEye.Point.x, TailEye.Point.y + TopEye.Point.y);
        Center /= 2;

        MyTransform.position = new Vector3(
            (RightEye.Point.x - Center.x) * XYRatio - OffSet.x,
            -((RightEye.Point.y - Center.y) * XYRatio - OffSet.y),
            0);


        // Debug.Log("TailEye.x = " + TailEye.Point.x + ", TopEye.x = " + TopEye.Point.x + 
        // "\nCenter.x = " + Center.x + 
        // "\nRightEye.x = " + RightEye.Point.x +
        // "TailEye.y = " + TailEye.Point.y + ", TopEye.y = " + TopEye.Point.y + 
        // "\nCenter.y = " + Center.y + 
        // "\nRightEye.y = " + RightEye.Point.y
        // );
    }

    public void SetOffSet(Hands[] MovePoints)
    {
        Hands RightEye = MovePoints[473];
        Hands LeftEye = MovePoints[468];
        Hands TopEye = MovePoints[362];
        Hands TailEye = MovePoints[263];

        // Hands RightEye = MovePoints[0];
        // RightEye.Point.x = Sample[4][0];
        // RightEye.Point.y = Sample[4][1];

        Vector2 Center = new Vector2(TailEye.Point.x + TopEye.Point.x, TailEye.Point.y + TopEye.Point.y);
        Center /= 2;

        OffSet =  new Vector3(
            (RightEye.Point.x - Center.x) * XYRatio,
            (RightEye.Point.y - Center.y) * XYRatio
            );

        Debug.Log("TailEye.x = " + TailEye.Point.x + ", TopEye.x = " + TopEye.Point.x + 
        "\nCenter.x = " + Center.x + 
        "\nRightEye.x = " + RightEye.Point.x
        );
    }

    public void SetLinearTrans(Hands[] EyePoint, int Num)
    {
        Hands RightEye = EyePoint[473];
        Hands LeftEye = EyePoint[468];

        if (0 <= Num && Num <= 3)
        {
            LinearTrans[Num].x = RightEye.Point.x;
            LinearTrans[Num].y = RightEye.Point.y;
        }
        else
        {
            int index = 0;
            foreach (Vector2 OffSet in LinearTrans)
            {
                Debug.Log(index + ", " + "x: " + OffSet.x + ", y: " + OffSet.y);
            }
        }
    }
}
