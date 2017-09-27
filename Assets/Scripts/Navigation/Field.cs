using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class Field : Waypoint {

    public Waypoint previous;
    public Waypoint next;

    public NavigatableField navigatableField;

    public GameObject visibleField;

    private string path;

    private void Awake()
    {
        if (navigatableField == null)
        {
            System.Type type;
            fieldMap.TryGetValue(field, out type);
            navigatableField = gameObject.AddComponent(type) as NavigatableField;
        }

        if(visibleField == null)
        {
            string path = "";
            fieldPath.TryGetValue(field, out path);
            visibleField = Instantiate(Resources.Load<GameObject>(path), transform.position, new Quaternion(-0.707f, 0.0f, 0.0f, 0.707f));
            visibleField.transform.parent = transform;
            //visibleField.transform.localScale = new Vector3(2.5f, 2.5f, 0.0f);
        }
    }

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
        Handles.ConeCap(0, next.transform.position, next.transform.rotation, 1.0f);
    }
}
