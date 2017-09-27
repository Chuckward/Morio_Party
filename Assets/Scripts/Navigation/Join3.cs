using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Join3 : Join {

    public Waypoint previousLeft;
    public Waypoint previousCenter;
    public Waypoint previousRight;

    public override Vector3 LinearPosition(Waypoint previous, float ratio, Waypoint next)
    {
        throw new System.NotImplementedException();
    }

    public override Quaternion Orientation(Waypoint previous, float ratio, Waypoint next)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        Handles.DrawDottedLine(transform.position, next.transform.position, 3.0f);
        Handles.ConeCap(0, next.transform.position, next.transform.rotation, 1.0f);
        //Handles.ConeCap(0, nextRight.transform.position, nextRight.transform.rotation, 1.0f);
    }
}
