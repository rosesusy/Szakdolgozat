using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovalentMoleculaManager : MonoBehaviour
{

    // Use this for initialization
    List<GameObject> side_atoms;
    List<GameObject> bonds;
    GameObject core;

    public void CopyAtomsAndBodsTo(GameObject to)
    {
        CovalentMoleculaManager to_cov = to.GetComponent<CovalentMoleculaManager>();
        CopyBonds(to_cov);
        for (int i = 0; i < side_atoms.Count; i++)
        {
            if (side_atoms[i].transform.childCount != 0)
            {
                to_cov.PutSideAtoms(side_atoms[i].transform.GetChild(0).gameObject); 
            }
        }
        to_cov.SetCoreAtom(this.core.transform.GetChild(0).gameObject);
    }

    public void CopyBonds(CovalentMoleculaManager to_cov)
    {
        if (to_cov.bonds == null)
            to_cov.bonds = Utility.FindObjectsWithTag(to_cov.gameObject.transform, "c_bond_tube");
        for (int i = 0; i < this.bonds.Count; i++)
        {
            if (this.bonds[i].transform.childCount > 1)
            {
                while(this.bonds[i].transform.childCount>1)
                {
                    this.bonds[i].transform.GetChild(1).transform.SetParent(to_cov.bonds[i].transform);
                }
            }
        }
    }

    public bool CreateBond(GameObject satom, GameObject catom)
    {
        ChemistAtomModell s_atom_modell = satom.GetComponent<ChemistAtomModell>();
        ChemistAtomModell c_atom_modell = catom.GetComponent<ChemistAtomModell>();
        if (bonds == null)
            bonds = Utility.FindObjectsWithTag(this.gameObject.transform, "c_bond_tube");//itt 0 lesz a listában
        for (int i = 0; i < bonds.Count; i++)
        {
            if (bonds[i].transform.childCount == 1)//azaz  csak a side atom van benne
            {
                for (int j = 0; j < s_atom_modell.CovalentBoundNumber; j++)
                {
                    s_atom_modell[j].transform.SetParent(bonds[i].transform);
                 
                    c_atom_modell[i+j].transform.SetParent(bonds[i].transform);
                
                }
                return true;
            }
        }
        return true;
    }

    public bool PutSideAtoms(GameObject satom)
    {
        try
        {
            if (side_atoms == null)
                side_atoms = Utility.FindObjectsWithTag(this.transform, "c_side_atom");
            for (int i = 0; i < side_atoms.Count; i++)
            {
                if (side_atoms[i].transform.childCount == 0)//??
                {
                    satom.transform.SetParent(side_atoms[i].transform);
                    satom.transform.SetPositionAndRotation(side_atoms[i].transform.position, Quaternion.identity);
                    break;
                }
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    public bool SetCoreAtom(GameObject core_atom)
    {
        try
        {
            if (core == null)
            {
                core = Utility.FindObjectWithTag(this.transform, "c_core_atom");
            }
            core_atom.transform.SetParent(core.transform);//beilleszük a coreba a másik atomot
            core_atom.transform.SetPositionAndRotation(core.transform.position, Quaternion.identity);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
