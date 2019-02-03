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
        ColorBlock temp;
        CheckTheElementList();
        buttons = new List<Button>();
        foreach (ElementData e in LoadPeriodicTable.table)
        {
            Button newButton = Instantiate(buttonPrefab,this.transform);
            if (e.valence == 0)
                newButton.interactable = false;
            temp=newButton.GetComponent<Button>().colors;
            temp.normalColor = SetBondColor(e, playerSettings.currentAtom);
            newButton.GetComponent<Button>().colors = temp;
           //newButton.GetComponent<Image>().color = SetBondColor(e, playerSettings.currentAtom);
            newButton.GetComponentInChildren<ElementButtonTextController>().SetFromElementData(2, e);//TODO: A ".es helyett ki kell számítani a egfelelő mennyiséget, a bejelölt atomhoz képest
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
        ColorBlock temp;
        for (int i = 0; i < buttons.Count; i++)
        {
            temp = buttons[i].GetComponent<Button>().colors;
            temp.normalColor = SetBondColor(LoadPeriodicTable.table[i], playerSettings.currentAtom);
            buttons[i].GetComponent<Button>().colors = temp;
            buttons[i].GetComponentInChildren<ElementButtonTextController>().SetFromElementData( 2);
        }
    }

    Color32 SetBondColor(ElementData current_atom,ElementData bonding_atom)//TODO:Ezeknek a határoknak jobban utánna nézni
    {
        //ionoskötés
        if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) >= 2 && current_atom.electronnegativity + bonding_atom.electronnegativity >= 3.5 && current_atom.electronnegativity + bonding_atom.electronnegativity <= 5.5)
            return Color.magenta;
        //kovalens
        else if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) <= 2 && current_atom.electronnegativity + bonding_atom.electronnegativity >= 4)
            return Color.blue;
        //fémes
        else if (System.Math.Abs(current_atom.electronnegativity - bonding_atom.electronnegativity) <= 1 && current_atom.electronnegativity + bonding_atom.electronnegativity <= 3.5)
            return Color.white;
        return Color.clear;
    }
}
