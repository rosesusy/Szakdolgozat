using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemistElectronModel : MonoBehaviour {
    public GameObject atomCore;
    public Vector3 rot;
    public float speed = 1000;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (atomCore != null)
        {
            transform.RotateAround(this.transform.parent.transform.position, rot, Time.deltaTime * speed);

        }
    }
}
