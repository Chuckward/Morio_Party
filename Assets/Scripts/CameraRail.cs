using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRail : MonoBehaviour {

    private Transform[] node;

    private void Start()
    {
        node = GetComponentsInChildren<Transform>();
    }

    public Vector3 LinearPosition(int segment, float ratio)
    {
        Vector3 p1 = node[segment].position;
        Vector3 p2 = node[segment + 1].position;

        return Vector3.Lerp(p1, p2, ratio);
    }

    public Vector3 CatmullPosition(int segment, float ratio)
    {
        Vector3 p1, p2, p3, p4;

        if(segment == 0)
        {
            p1 = node[segment].position;
            p2 = p1;
            p3 = node[segment + 1].position;
            p4 = node[segment + 2].position;
        }
        else if(segment == node.Length - 2)
        {
            p1 = node[segment - 1].position;
            p2 = node[segment].position;
            p3 = node[segment + 1].position;
            p4 = p3;
        }
        else if(segment == node.Length - 1)
        {
            p1 = node[segment - 2].position;
            p2 = node[segment - 1].position;
            p3 = node[segment].position;
            p4 = p3;
        }
        else
        {
            p1 = node[segment - 1].position;
            p2 = node[segment].position;
            p3 = node[segment + 1].position;
            p4 = node[segment + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        float x = 0.5f * ((2.0f * p2.x) + (-p1.x + p3.x) * ratio + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2 + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);
        float y = 0.5f * ((2.0f * p2.y) + (-p1.y + p3.y) * ratio + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2 + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);
        float z = 0.5f * ((2.0f * p2.z) + (-p1.z + p3.z) * ratio + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2 + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);

        return new Vector3(x, y, z);
    }

    public Quaternion Orientation(int segment, float ratio)
    {
        if(segment < node.Length - 1)
        {
            Quaternion q1 = node[segment].rotation;
            Quaternion q2 = node[segment + 1].rotation;
            return Quaternion.Lerp(q1, q2, ratio);
        } 
        else
        {
            Quaternion q1 = node[segment].rotation;
            Quaternion q2 = q1;
            return Quaternion.Lerp(q1, q2, ratio);
        }
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < node.Length - 1; i++)
        {
            Handles.DrawDottedLine(node[i].position, node[i + 1].position, 3.0f);
        }
    }

    public int GetLength()
    {
        return node.Length;
    }
}
