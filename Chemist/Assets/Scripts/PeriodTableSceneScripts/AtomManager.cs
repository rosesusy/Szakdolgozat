using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour {
    List<GameObject[]> electronpool = new List<GameObject[]>() { };
    const int SPEED = 1200;
    const int MIN_DISTANCE = 5;
    public TMPro.TextMeshPro text;
    public GameObject electronPrefab;
    public GameObject core;
    private int electronNumber;
    private GameObject[] electrons;

    void Start () {
        float coreX = core.transform.position.x;
        float coreY = core.transform.position.y;
        float coreZ = core.transform.position.z;

        text.text = ElectronNumber.ToString();
        electrons = new GameObject[ElectronNumber];
        for (int i = 0; i < ElectronNumber; i++)
        {
            if (i < 2) {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY+MIN_DISTANCE , coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 1;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1,i)*SPEED-i*10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.red;
            }
            else if(i>=2 && i < 10)
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE+5, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 2;
                //  electrons[i].GetComponent<AtomElctronSpin>().speed = 550;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.magenta;
            }
            else if ((i >= 10 && i < 18)||(i<=20 &&i<30))
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE + 10, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 3;
                //electrons[i].GetComponent<AtomElctronSpin>().speed = 500;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.yellow;
            }
            else if ((i >= 18 && i < 20) || (i <= 30 && i < 36) || (i <= 38 && i < 48)|| (i <= 56 && i < 70))
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE + 15, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 4;
                //electrons[i].GetComponent<AtomElctronSpin>().speed = 450;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.green;
            }
            else if ((i >= 36 && i < 38) || (i <= 48 && i < 54) || (i <= 70 && i < 80) || (i <= 88 && i < 102))
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE+ 20, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 5;
                // electrons[i].GetComponent<AtomElctronSpin>().speed = 400;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.cyan;
            }
            else if ((i >= 54 && i < 56) || (i <= 80 && i < 86) || (i <= 102 && i < 112))
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE + 25, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 6;
                //electrons[i].GetComponent<AtomElctronSpin>().speed = 350;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.blue;
            }
            else if ((i >= 86 && i < 88) || (i <= 112 && i < 118))
            {
                electrons[i] = Instantiate(electronPrefab, new Vector3(coreX, coreY + MIN_DISTANCE + 30, coreZ), Quaternion.identity, this.transform);
                electrons[i].GetComponent<AtomElctronSpin>().LEVEL = 7;
                //electrons[i].GetComponent<AtomElctronSpin>().speed = 300;
                electrons[i].GetComponent<AtomElctronSpin>().speed = Mathf.Pow(-1, i)*SPEED - i * 10;
                electrons[i].GetComponent<TrailRenderer>().startColor = Color.black;
            }
        }
	}

	void Update () {
		
	}

    public int ElectronNumber
    {
        get
        {
            return electronNumber;
        }

        set
        {
            electronNumber = value;
        }
    }
}
