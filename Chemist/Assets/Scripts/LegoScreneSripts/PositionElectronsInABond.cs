using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionElectronsInABond : MonoBehaviour
{
    Vector3 c_end;
    Vector3 s_end;
    void Start()
    {
    }


    void Update()
    {
        c_end = this.transform.position;
        s_end = this.transform.GetChild(0).position;
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
        }
    }
}
