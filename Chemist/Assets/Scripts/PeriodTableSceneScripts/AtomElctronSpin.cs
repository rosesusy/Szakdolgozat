using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomElctronSpin : MonoBehaviour
{

    public GameObject atomCore;
    public Vector3 rot;
    public float speed = 1000;
    public int LEVEL;
    // public Vector3 core = new Vector3(55f, -1f, 0f);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (atomCore != null)
        {
            transform.RotateAround(atomCore.transform.position, rot, Time.deltaTime * speed);
            /* Vector3 relativePos = (atomCore.transform.position + new Vector3(1.5f, 0, 0)) - this.transform.position;
             Quaternion rotation = Quaternion.LookRotation(relativePos);

             Quaternion current = this.transform.localRotation;

             this.transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
             this.transform.Translate(0, 0, speed/100 * Time.deltaTime);
     */
        }
    }
}
