using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        
        if (transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
        {
            return null;
        }

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindParent(GameObject go, string name = null, bool includeSelf = false)
    {
        Transform parentTransform = FindParent<Transform>(go, name, includeSelf);
        return (parentTransform != null) ? parentTransform.gameObject : null;
    }

    public static T FindParent<T>(GameObject go, string name = null, bool includeSelf = false) where T : UnityEngine.Object
    {
        if (go == null)
        {
            return null;
        }

        Transform current = includeSelf ? go.transform : go.transform.parent;

        while (current != null)
        {
            if (string.IsNullOrEmpty(name) || current.name == name)
            {
                T component = current.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }

            current = current.parent;
        }

        return null;
    }
}