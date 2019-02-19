using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChildren : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount > 0)
            this.transform.GetChild(0).transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
    }
}
