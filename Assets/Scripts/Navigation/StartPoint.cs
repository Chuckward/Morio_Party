﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class StartPoint : Waypoint {

    public Waypoint next;

    public override Vector3 LinearPosition(Waypoint previous, float ratio, Waypoint next)
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = this.next.transform.position;

        return Vector3.Lerp(p1, p2, ratio);
    }

    public override Quaternion Orientation(Waypoint previous, float ratio, Waypoint next)
    {
        Quaternion q1 = transform.rotation;
        Quaternion q2 = this.next.transform.rotation;

        return Quaternion.Lerp(q1, q2, ratio);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawDottedLine(transform.position, next.transform.position, 3.0f);
        Handles.ConeCap(0, next.transform.position/2, next.transform.rotation, 1.0f);
    }
}
