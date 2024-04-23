using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cdTime : MonoBehaviour
{
    private Image cdImg;
    private Text CDTime;

    private void Awake()
    {
        cdImg = gameObject.GetComponent<Image>();
        CDTime = transform.Find("cdTime").GetComponent<Text>();
    }

    private void OnEnable()
    {
        cdImg.fillAmount = 1;

    }

    private void OnDisable()
    {
        StopCoroutine("CD");
        if (gameObject.transform.parent.name == "Attack")
        {
            GunController.AttackAbled = true;
        }
        else
        {
            GunController.UltAbled = true;
        }
    }

    public void cdBegin(int cd)
    {
        StartCoroutine("CD", cd);
    }

    IEnumerator CD(int cd)
    {
        for(int i = cd; i > 0; i--)
        {
            if (i != cd)
            {
                cdImg.fillAmount -= (float)1 / cd;
            }
            CDTime.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        if (gameObject.transform.parent.name == "Attack")
        {
            GunController.AttackAbled = true;
        }
        else
        {
            GunController.UltAbled = true;
        }
        gameObject.SetActive(false);
    }
}
