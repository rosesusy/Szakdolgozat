using Chemist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadElementsToList : MonoBehaviour {

    public PlayerSettings playerSettings;
    // Use this for initialization
    public Button buttonPrefab;
   static List<Button> buttons;

    void Start () {
        BondTypes bond;
        float quantity;
        CheckTheElementList();
        buttons = new List<Button>();
        foreach (ElementData e in LoadPeriodicTable.table)
        {
            Button newButton = Instantiate(buttonPrefab, this.transform);
            
            bond = SetBond(e, playerSettings.currentAtom);
            quantity = SetQuantity(bond, e, playerSettings.currentAtom);
            if (e.valence == 0)
                newButton.interactable = false;
            newButton.GetComponentInChildren<ElementButtonTextController>().Bond = bond;
            newButton.GetComponentInChildren<ElementButtonTextController>().Index = buttons.Count;
            newButton.GetComponentInChildren<ElementButtonTextController>().SetFromElementData(quantity, e);
            buttons.Add(newButton);
      
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckTheElementList()
    {
        if(LoadPeriodicTable.table==null)
        {
            LoadPeriodicTable.LoadAllElements();
        }
    }

    public static void RefreshButtonDatas(ElementData current_atom)
    {
        BondTypes bond;
        float quantity;
        for (int i = 0; i < buttons.Count; i++)
        {
            bond = SetBond(LoadPeriodicTable.table[i], current_atom);
            quantity = SetQuantity(bond, LoadPeriodicTable.table[i], current_atom);
            if (LoadPeriodicTable.table[i].valence == 0 )
                buttons[i].interactable = false;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().Bond = bond;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().SetFromElementData(quantity);
        }
    }

    public static void RefreshButtonData(GameObject current_atom)
    {
        BondTypes bond;
        float quantity;
        for (int i = 0; i < buttons.Count; i++)
        {
            bond = SetBond(LoadPeriodicTable.table[i], LoadPeriodicTable.table[current_atom.GetComponent<ChemistAtomModell>().Index]);
            quantity = SetQuantityInABond(bond, LoadPeriodicTable.table[i], current_atom);
            if (LoadPeriodicTable.table[i].valence == 0)
                buttons[i].interactable = false;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().Bond = bond;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().SetFromElementData(quantity);
        }
    }

    static BondTypes SetBond(ElementData bonding_atom, ElementData current_atom)//TODO:Ezt kitenni statikus osztályba
    {
        if (current_atom.valence == 0)
            return BondTypes.None;
        //ionoskötés
        else if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) >1.5 && current_atom.electronnegativity + bonding_atom.electronnegativity >= 3)
            return BondTypes.Ionic;
        //kovalens
        else if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) <= 1.5 && current_atom.electronnegativity + bonding_atom.electronnegativity >= 3)
            return BondTypes.Covalent;
        //fémes
        else if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) <= 1 && current_atom.electronnegativity + bonding_atom.electronnegativity <= 3.5)
            return BondTypes.Metalic;
        return BondTypes.None;
    }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bond">A Kötés tipusa</param>
    /// <param name="bonding_atom">Ezt az atomot akarom a bonddal</param>
    /// <param name="current_atom">Ehhez az atomhoz kötni, ő a fő atom</param>
    /// <returns></returns>
    static float SetQuantity(BondTypes bond, ElementData bonding_atom, ElementData current_atom) //TODO:Ezt kitenni statikus osztályba
    {
        if (bond.Equals(BondTypes.Covalent))
        {
            //if (bonding_atom.valence > current_atom.valence)
            //    return 0;
            //else 
            return current_atom.valence / (float)bonding_atom.valence;
        }
        else if (bond.Equals(BondTypes.Ionic))
        {
            int current_atom_last_shell_index = current_atom.shells.Length;
            int bonding_atom_last_shell_index = bonding_atom.shells.Length;
            return (8 - current_atom.shells[current_atom_last_shell_index - 1]) /(float) bonding_atom.shells[bonding_atom_last_shell_index - 1];
        }
        else if(bond.Equals(BondTypes.Metalic))
        {
            //TODO:Megcsinálni ide a fémes kötéshez tartozó cuccost
        }
        return 0;
    }

    static float SetQuantityInABond(BondTypes bond, ElementData bonding_atom, GameObject current_atom) //TODO:Ezt kitenni statikus osztályba
    {
        ChemistAtomModell current_atom_modell = current_atom.GetComponent<ChemistAtomModell>();
        if (bond.Equals(BondTypes.Covalent))
        {
            //TODO:Megcsinálni a kovalens kötéshez
            return LoadPeriodicTable.table[ current_atom_modell.Index].valence-current_atom_modell.CovalentBoundNumber/ (float)bonding_atom.valence;
        }
        else if (bond.Equals(BondTypes.Ionic))
        {
            int current_atom_valence_electrons = current_atom.GetComponent<ChemistAtomModell>().ElectronCount;
            int bonding_atom_last_shell_index = bonding_atom.shells.Length;
            if (current_atom_valence_electrons < 8 && current_atom_valence_electrons > 0)
                return (8 - current_atom_modell.ElectronCount) / (float)bonding_atom.shells[bonding_atom_last_shell_index - 1];
            else
                return 0;
        }
        else if (bond.Equals(BondTypes.Metalic))
        {
            //TODO:Megcsinálni ide a fémes kötéshez tartozó cuccost
        }
        return 0;
    }

}