using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMove : MonoBehaviour {

    public CameraRail rail;
    public CameraRail IntroductionRail;

    public UnityEvent m_StartCamera;
    public UnityEvent m_StartIntroductionCamera;
    public UnityEvent m_ZoomIn;

    private int currentSegment;
    private float transition;
    private bool isCompleted;
    private bool zoomInCompleted;

    private MapLogic mapLogicScript;

    private void Awake()
    {
        m_StartCamera = new UnityEvent();
        m_StartIntroductionCamera = new UnityEvent();
        m_ZoomIn = new UnityEvent();
        
        isCompleted = true;
        zoomInCompleted = true;

        mapLogicScript = GameObject.Find("Map").GetComponent<MapLogic>();

        m_StartIntroductionCamera.AddListener(StartIntroduction);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!zoomInCompleted)
        {
            ZoomIn();
        }

        if(!isCompleted)
        {
            Play();
        }
    }

    private void Play()
    {
        if (currentSegment == IntroductionRail.GetLength() - 1)
        {
            mapLogicScript.m_introductionFinished.Invoke();
            isCompleted = true;
            print("Introduction completed");
            return;
        }

        transition += Time.deltaTime * 1 / 2.5f;
        if(transition > 1)
        {
            transition = 0;
            currentSegment++;
            print("Segment:" + currentSegment.ToString());
            print("Nodes:" + IntroductionRail.GetLength().ToString());
        } else if(transition < 0)
        {
            transition = 1;
            currentSegment--;
        }

        transform.position = rail.CatmullPosition(currentSegment, transition);
        transform.rotation = rail.Orientation(currentSegment, transition);
    }

    private void ZoomIn()
    {

    }

    private void StartIntroduction()
    {
        rail = IntroductionRail;
        isCompleted = false;
    }
}
