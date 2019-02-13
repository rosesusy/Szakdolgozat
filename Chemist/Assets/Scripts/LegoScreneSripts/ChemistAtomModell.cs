using Chemist;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemistAtomModell : MonoBehaviour
{

    public GameObject chemistElectronModel;
    public TextMeshPro symbol;
    public GameObject ionModell;

    private int index;
    private const int MIN_DISTANCE = 1;
    private List<GameObject> valenceElectrons;

    public bool is_in_a_bond = false;
    public bool changeElectrons = false;

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

    public GameObject this[int index]
    {
        get { return this.valenceElectrons[index]; }
    }
    public void AddElectron(GameObject electron)
    {
        this.valenceElectrons.Add(electron);
    }
    public int ElectronCount
    {
        get { return this.valenceElectrons.Count; }
    }
    public void RemoveElectron(GameObject elecron)
    {
        this.valenceElectrons.Remove(elecron);
    }
    public GameObject LastElectron()
    {
        return this.valenceElectrons[this.valenceElectrons.Count - 1];
    }
    // Use this for initialization

    void Start()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        if (this.ValenceElectronNumber != 0)
        {
            float min_rotation_degree = 360 / this.ValenceElectronNumber;
            // this.valenceElectrons = new GameObject[ValenceElectronNumber];
            this.valenceElectrons = new List<GameObject>();
            for (int i = 0; i < this.ValenceElectronNumber; i++)
            {
                GameObject newValenceElectron = Instantiate(chemistElectronModel, new Vector3(x, y, z), Quaternion.identity, this.transform);
                newValenceElectron.SetActive(true);
                newValenceElectron.transform.Rotate(0, 0, min_rotation_degree * i);
                newValenceElectron.transform.Translate(new Vector3(MIN_DISTANCE, 0, 0));
                this.valenceElectrons.Add(newValenceElectron);
            }
        }
    }
    private void Update()
    {
        if (changeElectrons && this.valenceElectrons.Count != 0)
        {
            float rotation_degree = 360 / this.valenceElectrons.Count;
            for (int i = 0; i < this.valenceElectrons.Count; i++)
            {
                valenceElectrons[i].transform.SetPositionAndRotation(valenceElectrons[i].transform.parent.position, Quaternion.identity);
                valenceElectrons[i].transform.Rotate(0, 0, rotation_degree * i);
                valenceElectrons[i].transform.Translate(new Vector3(MIN_DISTANCE, 0, 0));
            }
            changeElectrons = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ChemistAtomModell other_atom = other.gameObject.GetComponent<ChemistAtomModell>();
        if (!other_atom.is_in_a_bond && other_atom.Bond == BondTypes.Ionic && this.ElectronCount > 0 && other_atom.ElectronCount > 0 )
        {

            other.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            this.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            int otherAtomNumber = other_atom.Index;
            int thisAtomNumber = this.Index;
            if (LoadPeriodicTable.table[otherAtomNumber].electronnegativity > LoadPeriodicTable.table[thisAtomNumber].electronnegativity)
            {
                if (other_atom.ElectronCount < 8)
                {
                    CopyElectronsToOtherAtom(this, other_atom);
                    other_atom.changeElectrons = true;
                    this.is_in_a_bond = true;
                    other_atom.is_in_a_bond = true;
                    this.CreateIon(other.gameObject, this.gameObject);
                    LoadElementsToList.RefreshButtonData(other.gameObject);
                }
              
            }
            else
            {
                if (this.ElectronCount < 8)
                {
                    CopyElectronsToOtherAtom(other_atom, this);
                    this.changeElectrons = true;
                    this.is_in_a_bond = true;
                    other_atom.is_in_a_bond = true;
                    this.CreateIon(other.gameObject, this.gameObject);
                    LoadElementsToList.RefreshButtonData(this.gameObject);
                }
            }

        }
    }
    private void CopyElectronsToOtherAtom(ChemistAtomModell from, ChemistAtomModell to)
    {
        for (int i = 0; i < from.ElectronCount; i++)
        {
            to.AddElectron(from[i]);
            to.LastElectron().transform.SetParent(to.transform);
        }
        for (int i = from.ElectronCount-1; i>=0; i--)
        {
            from.RemoveElectron(from[i]);
        }
    }

    private void CreateIon(GameObject other, GameObject this_one )
    {
        if (other.transform.parent != null)
            this_one.transform.SetParent(other.transform.parent.transform);
        else if (this_one.transform.parent != null)
            other.gameObject.transform.SetParent(this_one.transform.parent.transform);
        else
        {
            GameObject newIon = Instantiate(ionModell);
            other.gameObject.transform.SetParent(newIon.transform);
            this_one.transform.SetParent(newIon.transform);
        }
    }
    public void GetDataFromTheList(ElementData e)
    {
        this.ValenceElectronNumber = e.valence;
        symbol.text = e.symbol;
    }
}
