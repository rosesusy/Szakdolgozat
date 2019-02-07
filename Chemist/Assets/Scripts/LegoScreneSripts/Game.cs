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
        chemistObj.GetComponentInChildren<ChemistAtomModell>().ValenceElectronNumber = playerSettings.currentAtom.shells[playerSettings.currentAtom.shells.Length-1];
        chemistObj.GetComponentInChildren<ChemistAtomModell>().Index = playerSettings.currentAtom.number;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreateChemistModell(int index,BondTypes bond)
    {
        float num = Random.Range(5, 10);
        GameObject Obj = Instantiate(chemistAtomModel, new Vector3(num,num,0),Quaternion.identity);
        Obj.GetComponentInChildren<ChemistAtomModell>().symbol.text = LoadPeriodicTable.table[index].symbol;
        Obj.GetComponentInChildren<ChemistAtomModell>().ValenceElectronNumber = LoadPeriodicTable.table[index].shells[LoadPeriodicTable.table[index].shells.Length-1];
        Obj.GetComponentInChildren<ChemistAtomModell>().Index = index;
        Obj.GetComponentInChildren<ChemistAtomModell>().Bond = bond;
    }
}
