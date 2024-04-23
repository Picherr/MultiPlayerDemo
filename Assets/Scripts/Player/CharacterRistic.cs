using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    None,
    Fire,
    Ice,
    Poison
}

public class CharacterRistic : MonoBehaviour
{
    public string username;
    public bool isLocal;
    public CharacterType charactertype;
    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
}
