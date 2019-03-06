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
    public GameObject metalModell;

    public GameObject ax4_e0;
    public GameObject ax1e3;
    public GameObject ax2e0;
    public GameObject ax2e1;
    public GameObject ax2e2;
    public GameObject ax3e0;
    public GameObject ax3e1;
    public GameObject ax5e0;
    public GameObject ax6e0;
    public GameObject ax7e0;
    public GameObject ax8e0;
    //public GameObject playerTwoBulletPrefab;
    //[Range(0.1f, 3f)] public float bulletspeed = 0.2f;
    //[Range(0.1f, 3f)] public float speed = 0.1f;
    //public const int lifeCount = 5;
    public Chemist.ElementData currentAtom;
    //public int player2Point;
    //public float startX = 9.5f;
    //public float startY = 5f;
    //public float startZ = 0;

    private Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();
    public void SetAllCovalentModell()
    {
        if (models.Count == 0)
        {
            models.Add("40", ax4_e0);
            models.Add("10", ax1e3);//debug miatt
            models.Add("13", ax1e3);
            models.Add("20", ax2e0);
            models.Add("21", ax2e1);
            models.Add("22", ax2e2);
            models.Add("23", ax2e0);
            models.Add("30", ax3e0);
            models.Add("31", ax3e1);
            models.Add("50", ax5e0);
            models.Add("60", ax6e0);
            models.Add("70", ax7e0);
            models.Add("80", ax8e0);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="valence">kötö elektronok</param>
    /// <param name="E">nem kötő elektron párok</param>
    /// <returns></returns>
    public GameObject GetCovalentModellFromValence(int valence, int E)
    {
        GameObject temp;
        if (models.ContainsKey(string.Format("{0}{1}", valence, E)))
            temp = models[string.Format("{0}{1}", valence, E)];
        else
            temp = models[string.Format("{0}{1}", valence, 0)];
        return temp;
    }
}
