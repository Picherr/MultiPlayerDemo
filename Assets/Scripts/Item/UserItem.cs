using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{
    [SerializeField]
    private Text playername;

    public void SetPlayerInfo(string name)
    {
        playername.text = name;
    }
}
