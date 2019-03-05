using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionElectronsInABond : MonoBehaviour
{

    // Use this for initialization
    Vector3 c_end;
    Vector3 s_end;
    void Start()
    {
        // vec = this.transform.position-this.transform.GetChild(0).GetChild(0).position;
    }


    void Update()
    {
        c_end = this.transform.position;
        s_end = this.transform.GetChild(0).position;
        //vec = this.transform.position - this.transform.GetChild(0).GetChild(0).position;
        if (this.transform.childCount > 1)
        {
            int n = transform.childCount - 1;
            int m = 1;
            while (m < transform.childCount && n > 0)
            {
                this.transform.GetChild(m).position = new Vector3(
                        (n * c_end.x +m* s_end.x) / this.transform.childCount,
                        (n * c_end.y +m* s_end.y) / this.transform.childCount,
                        (n * c_end.z +m* s_end.z) / this.transform.childCount
                    );
                m++;
                n--;
            }
            
            //this.transform.GetChild(1).position = new Vector3(
            //        (2 * c_end.x + s_end.x) / this.transform.childCount,
            //        (2 * c_end.y + s_end.y) / this.transform.childCount,
            //        (2 * c_end.z + s_end.z) / this.transform.childCount
            //    );
            //this.transform.GetChild(2).position = new Vector3(
            //        ( c_end.x + 2*s_end.x) / 3,
            //        ( c_end.y + 2*s_end.y) / 3,
            //        ( c_end.z + 2*s_end.z) / 3
            //    );
            //for (int i = 1; i < this.transform.childCount; i++)
            //{
            //    this.transform.GetChild(i).transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            //    this.transform.GetChild(i).transform.position = vec + new Vector3(i+1, i+1, i+1);
            //}

        }
    }
}
//float rotation_degree = 360 / this.valenceElectrons.Count;
//            for (int i = 0; i< this.valenceElectrons.Count; i++)
//            {
//                valenceElectrons[i].transform.SetPositionAndRotation(valenceElectrons[i].transform.parent.position, Quaternion.identity);
//                valenceElectrons[i].transform.Rotate(0, 0, rotation_degree* i);
//                valenceElectrons[i].transform.Translate(new Vector3(MIN_DISTANCE, 0, 0));
//            }