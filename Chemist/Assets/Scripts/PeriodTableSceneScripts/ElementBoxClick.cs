using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBoxClick : MonoBehaviour {

    public GameObject atomModell;
    public PlayerSettings playerSettings;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        GameObject oldAtom = GameObject.FindGameObjectWithTag("AtomModell");
        if (oldAtom != null)
            Destroy(oldAtom);
        GameObject newAtom = Instantiate<GameObject>(playerSettings.atomModellPrefab);
        newAtom.GetComponentInChildren<AtomManager>().ElectronNumber = this.GetComponent<PeriodicTableBoxElementAndTextes>().ElectronNumber;
        playerSettings.currentAtom= this.GetComponent<PeriodicTableBoxElementAndTextes>().Current;
    }
    
}
