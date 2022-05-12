using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Player player;

    AudioSource SFX_AudioSource;

    public AudioClip footstep;
    public AudioClip jump;
    public AudioClip success;
    public AudioClip click;

    public void Start()
    {
        SFX_AudioSource = GameObject.Find("SoundManager").transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "footstep":
                SFX_AudioSource.clip = footstep;
                break;

            case "jump":
                SFX_AudioSource.clip = jump;
                break;

            case "success":
                SFX_AudioSource.clip = success;
                break;

            case "click":
                SFX_AudioSource.clip = click;
                break;
        }
        SFX_AudioSource.Play();
    }

    public void LeftBtnDown()
    {
        player.LeftMove = true;
        PlaySound("footstep");
    }
    public void LeftBtnUp()
    {
        player.LeftMove = false;
        SFX_AudioSource.Stop();
    }
    public void RightBtnDown()
    {
        player.RightMove = true;
        PlaySound("footstep");
    }
    public void RightBtnUp()
    {
        player.RightMove = false;
        SFX_AudioSource.Stop();
    }

    public void JumpBtnDown()
    {
        player.Jump = true;
        PlaySound("jump");
    }
}
