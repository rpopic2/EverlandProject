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
    public AudioClip damage;
    public AudioClip zoomin;
    public AudioClip zoomout;
    public AudioClip pet;
    public AudioClip cabinet;

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
                SFX_AudioSource.loop = true;
                SFX_AudioSource.volume = 0.3f;
                break;

            case "jump":
                SFX_AudioSource.clip = jump;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "success":
                SFX_AudioSource.clip = success;
                SFX_AudioSource.loop = true;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "click":
                SFX_AudioSource.clip = click;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.9f;
                break;

            case "damage":
                SFX_AudioSource.clip = damage;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "zoomin":
                SFX_AudioSource.clip = zoomin;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "zoomout":
                SFX_AudioSource.clip = zoomout;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "pet":
                SFX_AudioSource.clip = pet;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;

            case "cabinet":
                SFX_AudioSource.clip = cabinet;
                SFX_AudioSource.loop = false;
                SFX_AudioSource.volume = 0.6f;
                break;
        }
        SFX_AudioSource.Play();
    }

    public void LeftBtnDown()
    {
        player.LeftMove = true;
        if (player.isGround == true)
        {
            PlaySound("footstep");
        }
    }
    public void LeftBtnUp()
    {
        player.LeftMove = false;
        SFX_AudioSource.Stop();
    }
    public void RightBtnDown()
    {
        player.RightMove = true;
        if (player.isGround == true)
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
