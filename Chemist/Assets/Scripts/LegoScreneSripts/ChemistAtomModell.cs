using Chemist;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemistAtomModell : MonoBehaviour {
    public GameObject chemistElectronModel;
    public TextMeshPro symbol;

    private int index;
    private const int MIN_DISTANCE = 1;
    private GameObject[] valenceElectrons;

    public bool is_in_a_bond = false;

    public int ValenceElectronNumber { get; set; }
    public BondTypes Bond { get; set; }
    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.ValenceElectronNumber =5 ;
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        float min_rotation_degree = 360 / this.ValenceElectronNumber;
        if (this.ValenceElectronNumber != 0)
        {
            this.valenceElectrons = new GameObject[ValenceElectronNumber];
            for (int i = 0; i <this.valenceElectrons.Length; i++)
            {
                this.valenceElectrons[i] = Instantiate(chemistElectronModel,new Vector3(x,y,z), Quaternion.identity, this.transform);
                this.valenceElectrons[i].transform.Rotate(min_rotation_degree * i, 0, min_rotation_degree * i);
                this.valenceElectrons[i].transform.Translate(new Vector3(MIN_DISTANCE,0,0));

             }
        }
	}
	
	
    public void GetDataFromTheList(ElementData e)
    {
        this.ValenceElectronNumber=e.valence;
        symbol.text = e.symbol;
    }
}
