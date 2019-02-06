
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementButtonTextController : MonoBehaviour {

    public BondTypes Bond { get; set; }

    TextMeshProUGUI[] texts;
    void Start()
    {
        
    }
    public void SetFromElementData(float quantity, Chemist.ElementData element)
    {
        SetFromElementData(quantity);
        texts[0].text = element.symbol;
    }

    internal void SetFromElementData(float quantity)
    {
        if (Bond != null)
            SetColorFromBond();
        if (texts == null)
            texts = this.GetComponentsInChildren<TextMeshProUGUI>();
        texts[1].text = quantity.ToString();
    }
    private void SetColorFromBond()
    {
        ColorBlock temp;
        temp = this.GetComponent<Button>().colors;
        if (Bond.Equals(BondTypes.Covalent))
            temp.normalColor = Color.cyan;
        else if (Bond.Equals(BondTypes.Ionic))
            temp.normalColor = Color.red;
        else if (Bond.Equals(BondTypes.Metalic))
            temp.normalColor = Color.white;
        else if (Bond.Equals(BondTypes.None))
            this.GetComponent<Button>().interactable = false;
        this.GetComponent<Button>().colors = temp;
    }
}
