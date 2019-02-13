using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshList : MonoBehaviour {

    private GameObject atom;
    private bool _mouseState;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo;
            atom = GetClickedObject(out hitInfo);
            if (atom != null)
            {
                _mouseState = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
        }
        if (_mouseState)
        {
            ChemistAtomModell atom_modell = atom.GetComponent<ChemistAtomModell>();
            if(atom.transform.parent==null)
                LoadElementsToList.RefreshButtonDatas(LoadPeriodicTable.table[atom_modell.Index]);
            else
            { LoadElementsToList.RefreshButtonData(atom); }
        }
    }
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            
        }
        return target;
    }
}
