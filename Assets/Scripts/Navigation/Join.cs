using UnityEngine;

public abstract class Join : Waypoint {

    public Waypoint next;

    public NavigatableField navigatableField;

    public GameObject visibleField;

    private void Awake()
    {
        if (navigatableField == null)
        {
            System.Type type;
            fieldMap.TryGetValue(field, out type);
            navigatableField = gameObject.AddComponent(type) as NavigatableField;
        }

        if (visibleField == null)
        {
            string path = "";
            fieldPath.TryGetValue(field, out path);
            visibleField = Instantiate(Resources.Load<GameObject>(path), transform.position, new Quaternion(-0.707f, 0.0f, 0.0f, 0.707f));
            visibleField.transform.parent = transform;
            //visibleField.transform.localScale = new Vector3(2.5f, 2.5f, 0.0f);
        }
    }
}
