using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;
    string msg = null;

    public override void OnEnter()
    {
        base.OnEnter();
        text.CrossFadeAlpha(0, 0.1f, false);
        uiMag.SetMessagePanel(this);
    }

    private void Update()
    {
        if (msg != null)
        {
            ShowText(msg);
            msg = null;
        }
    }

    public void ShowMessage(string str, bool sync = false)
    {
        if (sync)
        {
            // “Ï≤Ωœ‘ æ
            msg = str;
        }
        else
        {
            ShowText(str);
        }
    }

    private void ShowText(string str)
    {
        text.text = str;
        text.CrossFadeAlpha(1, 0.1f, false);
        Invoke("HideText", 1.0f);
    }

    private void HideText()
    {
        text.CrossFadeAlpha(0, 1.0f, false);
    }
}
