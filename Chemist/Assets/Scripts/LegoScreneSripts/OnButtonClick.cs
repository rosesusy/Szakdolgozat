using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonClick : MonoBehaviour {

    
	public void CreateModel(GameObject game)
    {
        game.GetComponent<Game>().CreateChemistModell(GetComponent<ElementButtonTextController>().Index);
    }
}
