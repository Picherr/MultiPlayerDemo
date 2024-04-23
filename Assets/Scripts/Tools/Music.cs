using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] PreClips;
    public AudioClip[] GameClips;
    private AudioSource AS;

    private int PreLen;
    private int GameLen;

    private void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
        PreLen = PreClips.Length;
        GameLen = GameClips.Length;
        PrePlay();
    }

    public void PrePlay()
    {
        StopCoroutine("gamePlay");
        AS.Stop();
        StartCoroutine("prePlay");
    }

    IEnumerator prePlay()
    {
        int index = 0;
        while (true)
        {
            AS.clip = PreClips[index];
            AS.Play();
            index = (index + 1) % PreLen;

            yield return new WaitForSeconds(AS.clip.length);
        }
    }

    public void GamePlay()
    {
        StopCoroutine("prePlay");
        AS.Stop();
        StartCoroutine("gamePlay");
    }

    IEnumerator gamePlay()
    {
        int index = 0;
        while (true){
            AS.clip = GameClips[index];
            AS.Play();
            index = (index + 1) % GameLen;

            yield return new WaitForSeconds(AS.clip.length);
        }
    }
}
