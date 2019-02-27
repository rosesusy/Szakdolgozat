using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropAtom : MonoBehaviour
{

    private bool _mouseState;
    private bool _rotate;
    private GameObject atom;
    private Vector3 screenSpace;
    private Vector3 offset;
    private float rotSpeed = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(_mouseState);
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo;
            atom = Utility.GetClickedObject(out hitInfo);
            if (atom != null)
            {
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(atom.transform.position);
                offset = atom.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _mouseState = false;
        }
        if (_mouseState)
        {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

            //update the position of the object in the world
            atom.transform.position = curPosition;
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hitInf;
            DestroyAtom(Utility.GetClickedObject(out hitInf));
        }
        if(Input.GetMouseButtonDown(2))
        {
            _rotate = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            _rotate = false;
        }
        if(_rotate)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.RotateAround(Vector3.up, -rotX);
            transform.RotateAround(Vector3.right, rotY);
        }
    }




    void DestroyAtom(GameObject atom)
    {
        if (atom != null)
            Destroy(atom);
        LoadElementsToList.RefreshButtonData(null);
    }

    public void DropMouse()
    {
        _mouseState = false;
        _rotate = false;
    }
}
