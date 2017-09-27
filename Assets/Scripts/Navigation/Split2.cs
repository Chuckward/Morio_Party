using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[ExecuteInEditMode]
public class Split2 : Split {

    // Use this for initialization
    void Start () {
        m_EnableArrows = new UnityEvent();
        m_EnableArrows.AddListener(EnableArrows);
        m_DisableArrows = new UnityEvent();
        m_DisableArrows.AddListener(DisableArrows);

        arrows = GameObject.FindGameObjectsWithTag("Arrow");
        if (arrows.Length > 0)
        {
            DisableArrows();  
        }
    }

    public override Vector3 LinearPosition(Waypoint previous, float ratio, Waypoint next)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = next.transform.position;
        print("Split next");
        return Vector3.Lerp(p1, p2, ratio);
    }

    public override Quaternion Orientation(Waypoint previous, float ratio, Waypoint next)
    {
        Quaternion q1 = transform.rotation;
        Quaternion q2 = next.transform.rotation;

        return Quaternion.Lerp(q1, q2, ratio);
    }

    public override void EnableArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<MeshRenderer>().enabled = true;
        }
        print("Arrows Enabled");
    }

    public override void DisableArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].GetComponent<MeshRenderer>().enabled = false;
        }
        print("Arrows Disabled");
    }

    private void OnDrawGizmos()
    {
        Handles.DrawDottedLine(transform.position, nextLeft.transform.position, 3.0f);
        Handles.DrawDottedLine(transform.position, nextRight.transform.position, 3.0f);
        Handles.ConeCap(0, nextLeft.transform.position, nextLeft.transform.rotation, 1.0f);
        Handles.ConeCap(0, nextRight.transform.position, nextRight.transform.rotation, 1.0f);
    }
}
