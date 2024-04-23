using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected UIManager uiMag;

    protected GameFace face
    {
        get
        {
            return GameFace.Face;
        }
    }

    public UIManager SetUIMag
    {
        set
        {
            uiMag = value;
        }
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnPause()
    {

    }

    public virtual void OnRecovery()
    {

    }

    public virtual void OnExit()
    {

    }
}
