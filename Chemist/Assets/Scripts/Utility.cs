using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility  {

    public static List<GameObject> FindObjectsWithTag(this Transform parent, string tag) //ki lehet tenni egy helperbe
    {
        List<GameObject> taggedGameObjects = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == tag)
            {
                taggedGameObjects.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                taggedGameObjects.AddRange(FindObjectsWithTag(child, tag));
            }
        }
        return taggedGameObjects;
    }
    public static GameObject FindObjectWithTag(this Transform parent, string tag) //ki lehet tenni egy helperbe
    {
        GameObject g = null;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == tag)
            {
                g= child.gameObject;
                break;

            }
            if (child.childCount > 0)
            {
                g= FindObjectWithTag(child, tag);
            }
        }
        return g;
    }
    public static GameObject GetClickedObject(out RaycastHit hit)//ki lehet tenni statikusba
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            if (target.GetComponent<ChemistAtomModell>().is_in_a_bond)
                target = GetFinalParent(target);
        }

        return target;
    }
    public static GameObject GetFinalParent(GameObject g)//ki lehet tenni statikusba
    {
        if (g.transform.parent == null)
            return g;
        return GetFinalParent(g.transform.parent.gameObject);
    }
}
