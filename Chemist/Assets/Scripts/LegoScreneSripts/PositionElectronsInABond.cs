using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionElectronsInABond : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	
	void Update () {
        if (this.transform.childCount > 1)
        {
            for (int i = 1; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
            
        }
    }
}
