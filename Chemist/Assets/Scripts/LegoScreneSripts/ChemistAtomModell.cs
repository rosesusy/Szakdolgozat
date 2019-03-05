using Chemist;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChemistAtomModell : MonoBehaviour
{

    //public GameObject chemistElectronModel;
    public TextMeshPro symbol;
    //public GameObject ionModell;
    public PlayerSettings playerSettings;
    private int index;
    private const int MIN_DISTANCE = 1;
    private List<GameObject> valenceElectrons;
    public bool is_in_a_bond = false;
    public bool changeElectrons = false;
    public bool is_side_atom = false;
    public int LastShellPopulation { get; set; }

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
    public int CovalentConectedAtomPc
    {
        get; set;
    }
    public int CovalentBoundNumber
    {
        get; set;
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
        playerSettings.SetAllCovalentModell();
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        if (this.LastShellPopulation != 0)
        {
            float min_rotation_degree = 360 / this.LastShellPopulation;
            this.valenceElectrons = new List<GameObject>();
            for (int i = 0; i < this.LastShellPopulation; i++)
            {
                GameObject newValenceElectron = Instantiate(playerSettings.chemistElectronModel, new Vector3(x, y, z), Quaternion.identity, this.transform);
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
        int otherAtomNumber;
        int thisAtomNumber;
        ChemistAtomModell other_atom = other.gameObject.GetComponent<ChemistAtomModell>();

        if (other_atom.Bond.Equals(BondTypes.Ionic) && (this.Bond.Equals(BondTypes.Ionic) || this.Bond.Equals(BondTypes.None)) && !other_atom.is_in_a_bond && this.ElectronCount > 0 && other_atom.ElectronCount > 0)
        {

            other.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            this.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            otherAtomNumber = other_atom.Index;
            thisAtomNumber = this.Index;
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
        else if (other_atom.Bond.Equals(BondTypes.Covalent) && (this.Bond.Equals(BondTypes.Covalent) || this.Bond.Equals(BondTypes.None)) && !other_atom.is_in_a_bond&&!other_atom.is_side_atom&&!this.is_side_atom)
        {
            other.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            this.gameObject.GetComponent<DragAndDropAtom>().DropMouse();
            otherAtomNumber = other_atom.Index;
            thisAtomNumber = this.Index;
            int otherAtomValence = LoadPeriodicTable.table[otherAtomNumber].valence;
            int otherAtomLastShellNumber = other_atom.LastShellPopulation;
            int thisAtomValence = LoadPeriodicTable.table[thisAtomNumber].valence;
            int thisAtomLastShellNumber = this.LastShellPopulation;

            if (other_atom.CovalentConectedAtomPc < otherAtomValence)
            {
                if (otherAtomNumber == thisAtomNumber)//ha ugyanazokat akrjuk összerakni
                {

                }
                else if (otherAtomValence > thisAtomValence)//ha a másik atom tud több atomhoz kapcsolódni, ő lesz a modell közepén
                {
                    CreateMolecula(other.gameObject, this.gameObject, otherAtomValence, otherAtomLastShellNumber, thisAtomValence);
                }
                else//ha nem akkor pedig ez
                {
                    CreateMolecula(this.gameObject, other.gameObject, thisAtomValence, thisAtomLastShellNumber, otherAtomValence);
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
        for (int i = from.ElectronCount - 1; i >= 0; i--)
        {
            from.RemoveElectron(from[i]);
        }
    }
    private void CreateIon(GameObject other, GameObject this_one)
    {
        if (other.transform.parent != null)
            this_one.transform.SetParent(other.transform.parent.transform);
        else if (this_one.transform.parent != null)
            other.gameObject.transform.SetParent(this_one.transform.parent.transform);
        else
        {
            GameObject newIon = Instantiate(playerSettings.ionModell);
            other.gameObject.transform.SetParent(newIon.transform);
            this_one.transform.SetParent(newIon.transform);
        }
    }

    private void CreateMolecula(GameObject core_atom, GameObject side_atom, int core_atom_valence, int core_atom_last_shell_number, int side_atom_valance)
    {
        GameObject molecula = null;
        GameObject oldmolecula = null;
        if (core_atom.GetComponent<ChemistAtomModell>().CovalentConectedAtomPc < core_atom_valence && core_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber < core_atom_valence)
        {
            core_atom.GetComponent<ChemistAtomModell>().CovalentConectedAtomPc++;
            if (core_atom_last_shell_number + side_atom_valance <= 8)
            {
                if (side_atom_valance > 3)
                {
                    side_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber = 3;
                    core_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber += 3;
                }
                else
                {
                    side_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber = side_atom_valance;
                    core_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber += side_atom_valance;
                }
            }
            else
            {
                side_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber = 1;
                core_atom.GetComponent<ChemistAtomModell>().CovalentBoundNumber++;
            }
        }

        if (core_atom.transform.parent == null)
        {
            molecula = GetMoleculeStructure(core_atom_valence, core_atom_last_shell_number, core_atom.GetComponent<ChemistAtomModell>().CovalentConectedAtomPc);//létrehozzuk a megfelelő molekulát
        }
        else
        {
            oldmolecula = Utility.GetFinalParent(core_atom);
            molecula = GetMoleculeStructure(core_atom_valence, core_atom_last_shell_number, core_atom.GetComponent<ChemistAtomModell>().CovalentConectedAtomPc);//létrehozzuk a megfelelő molekulát
            oldmolecula.GetComponent<CovalentMoleculaManager>().CopyAtomsAndBodsTo(molecula);
            Destroy(oldmolecula);
        }
        if (molecula.GetComponent<CovalentMoleculaManager>().SetCoreAtom(core_atom))
            core_atom.GetComponent<ChemistAtomModell>().is_in_a_bond = true;
        if (molecula.GetComponent<CovalentMoleculaManager>().PutSideAtoms(side_atom))
        {
            molecula.GetComponent<CovalentMoleculaManager>().CreateBond(side_atom, core_atom);
            side_atom.GetComponent<ChemistAtomModell>().is_in_a_bond = true;
            side_atom.GetComponent<ChemistAtomModell>().is_side_atom = true;
        }
    }

    GameObject GetMoleculeStructure(int core_atom_valence, int core_atom_last_shell_population, int connected_atom_pc)
    {
        GameObject prefab;
        int E = (core_atom_last_shell_population - core_atom_valence) / 2;
        int X = connected_atom_pc;

        prefab = playerSettings.GetCovalentModellFromValence(X, E);
        return Instantiate(prefab);
    }

    public void GetDataFromTheList(ElementData e)
    {
        this.LastShellPopulation = e.valence;
        symbol.text = e.symbol;
    }

}
