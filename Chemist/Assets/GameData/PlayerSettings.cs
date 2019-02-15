using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Chemist/PlayerData")]
public class PlayerSettings : ScriptableObject
{

    public GameObject atomModellPrefab;
    public GameObject electronPrefab;
    public GameObject atomCorePrefab;
    public GameObject atomBoxModellPrefab;
    public GameObject atomButtonPrefab;
    public GameObject chemistElectronModel;
    public GameObject ionModell;
    public GameObject ax4_e0;
    //public GameObject playerTwoBulletPrefab;
    //[Range(0.1f, 3f)] public float bulletspeed = 0.2f;
    //[Range(0.1f, 3f)] public float speed = 0.1f;
    //public const int lifeCount = 5;
    public Chemist.ElementData currentAtom;
    //public int player2Point;
    //public float startX = 9.5f;
    //public float startY = 5f;
    //public float startZ = 0;

}
