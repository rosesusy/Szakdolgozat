using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMetalicAtom : MonoBehaviour
{

    public int max_number_in_a_row { get; set; }
    int max_in_a_cloud;
    public int Max_in_a_cloud { get { return this.max_number_in_a_row * this.max_number_in_a_row; } }
    public int current_in_a_cloud { get; set; }
    public bool IsModified = false;
    Vector3 N1;
    Vector3 N2;
    Vector3 N3;
    Vector3 N4;
    Vector3 particle;
    float r;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        particle = this.transform.position;
        r = this.transform.GetChild(0).GetComponent<ParticleSystem>().shape.radius;
        N1 = new Vector3(particle.x-r/2, particle.y+r/2 , particle.z);
        //N2 = new Vector3(particle.x + r, particle.y + r - 1, particle.z);
        //N3 = new Vector3(particle.x - r, particle.y - r, particle.z);
        //N4 = new Vector3(particle.x + r, particle.y - r, particle.z);
        int i = 1;
        for (int y = 0; y < max_number_in_a_row && i < this.transform.childCount; y++)//sor azaz y
        {
            for (int x = 0; x < max_number_in_a_row && i < this.transform.childCount; x++)//oszlop azaz x
            {
                this.transform.GetChild(i).position=new Vector3(N1.x + x + 0.25f, N1.y - y - 0.25f, N1.z);
                i++;

            }

        }
    }
}
