using Chemist;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeriodicTableBoxElementAndTextes : MonoBehaviour {

    //public static Element ActiveElement;
    private int electronNumber;
    private ElementData current;


    public TextMeshPro ElementNumber;
    public TextMeshPro ElementName;
    public TextMeshPro ElementFullName;
    public TextMeshPro DataAtomicWeight;

    public int ElectronNumber
    {
        get
        {
            return electronNumber;
        }
    }

    public ElementData Current
    {
        get
        {
            return current;
        }

        set
        {
            current = value;
        }
    }

    public void SetFromElementData(ElementData element)
    {
        this.Current = element;
        ElementNumber.text = element.number.ToString();
        electronNumber = element.number;
        ElementName.text = element.symbol;
        ElementFullName.text = element.name;
        DataAtomicWeight.text = string.Format("{0:0.00}", element.atomic_mass);
    }
}
