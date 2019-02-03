
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementButtonTextController : MonoBehaviour {

    /*public TextMeshPro ElementQuantity;
     public TextMeshPro ElementName;
     public TextMeshPro DataAtomicWeight;
     */
    TextMeshProUGUI[] texts;
    void Start()
    {
        
    }
    public void SetFromElementData(int quantity, Chemist.ElementData element)
    {
        SetFromElementData(quantity);
        texts[0].text = element.symbol;
    }

    internal void SetFromElementData(int quantity)
    {
        if (texts == null)
            texts = this.GetComponentsInChildren<TextMeshProUGUI>();
        texts[1].text = quantity.ToString();
    }
}
