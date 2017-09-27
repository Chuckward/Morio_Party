using UnityEngine;
using UnityEngine.Events;

public abstract class Split : Waypoint {

    public Waypoint previous;

    public Waypoint nextLeft;
    public Waypoint nextRight;

    public GameObject[] arrows;

    public UnityEvent m_EnableArrows;
    public UnityEvent m_DisableArrows;

    public abstract void EnableArrows();

    public abstract void DisableArrows();
}
