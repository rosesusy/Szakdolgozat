using Chemist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadElementsToList : MonoBehaviour {

    public PlayerSettings playerSettings;
    // Use this for initialization
    public Button buttonPrefab;
    List<Button> buttons;

    void Start () {
        BondTypes bond;
        CheckTheElementList();
        buttons = new List<Button>();
        foreach (ElementData e in LoadPeriodicTable.table)
        {
            Button newButton = Instantiate(buttonPrefab, this.transform);
            if (e.valence == 0)
                newButton.interactable = false;
            bond = SetBond(e, playerSettings.currentAtom);
            newButton.GetComponentInChildren<ElementButtonTextController>().Bond = bond;
            newButton.GetComponentInChildren<ElementButtonTextController>().Index = buttons.Count;
            newButton.GetComponentInChildren<ElementButtonTextController>().SetFromElementData(SetQuantity(bond, e, playerSettings.currentAtom), e);
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

    public void RefreshButtonDatas(ElementData current_atom)
    {
        BondTypes bond;
        for (int i = 0; i < buttons.Count; i++)
        {
            bond = SetBond(LoadPeriodicTable.table[i], current_atom);
            buttons[i].GetComponentInChildren<ElementButtonTextController>().Bond = bond;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().SetFromElementData(SetQuantity(bond, LoadPeriodicTable.table[i], current_atom));
        }
    }

    BondTypes SetBond(ElementData bonding_atom, ElementData current_atom)//TODO:Ezt kitenni statikus osztályba
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
    float SetQuantity(BondTypes bond, ElementData bonding_atom, ElementData current_atom) //TODO:Ezt kitenni statikus osztályba
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
}
