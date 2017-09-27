using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

[ExecuteInEditMode]
public class Split3 : Split {

    public Waypoint nextStraight;

	// Use this for initialization
	void Start () {
		arrows = GameObject.FindGameObjectsWithTag("Arrow");
        m_EnableArrows = new UnityEvent();
        m_EnableArrows.AddListener(EnableArrows);
        m_DisableArrows = new UnityEvent();
        m_DisableArrows.AddListener(DisableArrows);
    }

    public override void EnableArrows()
    {
        throw new System.NotImplementedException();
    }

    public override void DisableArrows()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override Vector3 LinearPosition(Waypoint previous, float ratio, Waypoint next)
    {
        throw new System.NotImplementedException();
    }

    public override Quaternion Orientation(Waypoint previous, float ratio, Waypoint next)
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Handles.DrawDottedLine(transform.position, nextLeft.transform.position, 3.0f);
        Handles.DrawDottedLine(transform.position, nextStraight.transform.position, 3.0f);
        Handles.DrawDottedLine(transform.position, nextRight.transform.position, 3.0f);
        //Handles.ConeCap(0, nextLeft.transform.position, Orientation(0.5f, nextLeft), 1.0f);
        //Handles.ConeCap(0, nextLeft.transform.position, Orientation(0.5f, nextRight), 1.0f);
    }

    
}
