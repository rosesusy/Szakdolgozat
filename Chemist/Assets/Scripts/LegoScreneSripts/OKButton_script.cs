using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKButton_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OK()
    {
        this.transform.parent.transform.gameObject.SetActive(false);
    }
}
