using Chemist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPeriodicTable : MonoBehaviour {

    public GameObject cubePrefab;

    public float separationDistance;
    public float radius;
    public static List<ElementData> table;
	// Use this for initialization
	void Start () {
        // Parse the elements out of the json file
        //TextAsset asset = Resources.Load<TextAsset>("JSON/PeriodicTableJSON");
        LoadAllElements();
        foreach (ElementData e in table)
        {
            GameObject newElement = Instantiate<GameObject>(cubePrefab);
            newElement.GetComponentInChildren<PeriodicTableBoxElementAndTextes>().SetFromElementData(e);
            newElement.transform.localPosition = new Vector3((e.xpos * separationDistance - separationDistance)-55 ,
                                                            separationDistance - e.ypos * separationDistance+25,
                                                            radius);
            newElement.transform.localRotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public static void LoadAllElements()
    {
        TextAsset asset = Resources.Load<TextAsset>("JSON2/PeriodicTableJSON");
        table = ElementDataAll.FromJSON(asset.text).elements;
    }
}
