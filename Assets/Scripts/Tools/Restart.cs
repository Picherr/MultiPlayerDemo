using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public static List<GameObject> PanelsToDestroy = new List<GameObject>();

    public static void DestroyPanels()
    {
        foreach(var panel in PanelsToDestroy)
        {
            Debug.Log(panel.name);
            Destroy(panel);
        }
        PanelsToDestroy.Clear();
        PlayerManager.ClearPlayers();
    }
}
