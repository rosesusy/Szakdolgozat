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

    BondTypes SetBond(ElementData bonding_atom, ElementData current_atom)//TODO:Ezeknek a határoknak jobban utánna nézni
    {
        //ionoskötés
        if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) >= 2 && current_atom.electronnegativity + bonding_atom.electronnegativity >= 3)
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
    float SetQuantity(BondTypes bond, ElementData bonding_atom, ElementData current_atom) //TODO:Megcsinálni Ionosa és fémesre is
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

        }
        return 0;
    }
}
