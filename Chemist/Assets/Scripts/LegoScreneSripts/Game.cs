using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public PlayerSettings playerSettings;
    public GameObject chemistAtomModel;

    
	// Use this for initialization
	void Start () {
        GameObject chemistObj = Instantiate<GameObject>(chemistAtomModel);
        chemistObj.GetComponentInChildren<ChemistAtomModell>().symbol.text = playerSettings.currentAtom.symbol;
        chemistObj.GetComponentInChildren<ChemistAtomModell>().ValenceElectronNumber = playerSettings.currentAtom.valence;
        chemistObj.GetComponentInChildren<ChemistAtomModell>().Index = playerSettings.currentAtom.number;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreateChemistModell(int index)
    {
        float num = Random.Range(-5, 5);
        GameObject Obj = Instantiate(chemistAtomModel, new Vector3(num,num,0),Quaternion.identity);
        Obj.GetComponentInChildren<ChemistAtomModell>().symbol.text = LoadPeriodicTable.table[index].symbol;
        Obj.GetComponentInChildren<ChemistAtomModell>().ValenceElectronNumber = LoadPeriodicTable.table[index].valence;
        Obj.GetComponentInChildren<ChemistAtomModell>().Index = index;
    }
}
