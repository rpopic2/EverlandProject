using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject Start_Panel;
    [SerializeField] GameObject Player_UI_Panel;

    [SerializeField] GameObject player;
    [SerializeField] Transform RespawnPoint;

    [SerializeField] Cinemachine.CinemachineVirtualCamera VC_Start;
    [SerializeField] Cinemachine.CinemachineVirtualCamera VC_Playing;
    [SerializeField] Cinemachine.CinemachineVirtualCamera VC_End;

    [SerializeField] GameObject BG_Sun_Particles;
    [SerializeField] GameObject MainCamera;

    [SerializeField] Button[] MoveButtons;

    //===================PlatformPrefab
    [SerializeField] GameObject DropPlatform;
    //===================PlatformPrefab

    public bool isGameStart;
    public bool isGameEnd;
    public bool isReSpawning;

    public AudioClip intro;
    public AudioClip Ending;

    public AudioClip everland;

    AudioSource BG_AudioSource;

    public Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isGameEnd = false;
        isReSpawning = false;
        Invoke("PlayIntro", 2.5f);
        BG_AudioSource = GameObject.Find("SoundManager").transform.GetChild(0).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameEnd)
        {
            Vector3 SP_Pos = BG_Sun_Particles.transform.position;

            SP_Pos.x = MainCamera.transform.position.x;

            BG_Sun_Particles.transform.position = SP_Pos;
        }
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case"intro":
                BG_AudioSource.clip = intro;
                BG_AudioSource.loop = true;
                break;

            case "everland":
                BG_AudioSource.clip = everland;
                BG_AudioSource.loop = true;
                break;
            
            case "ending":
                BG_AudioSource.volume = 0.4f;
                BG_AudioSource.clip = Ending;
                BG_AudioSource.loop = false;
                break;
        }
        BG_AudioSource.Play();
    }

    public void GameStart_Button()
    {
        if (!isGameStart)
        {
            GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("gameStart");
            Anim.SetBool("isStartClick", true);
            Invoke("GameStart", 1.7f);
        }
    }

    public void GameStart()
    {
        isGameStart = true;
        Start_Panel.SetActive(false);
        Player_UI_Panel.SetActive(true);

        GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("click");

        if (GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>()._background == PlayerSetting.backgroundImage.everland)
        {
            PlaySound("everland");
        }

        VC_Start.Priority = 9;
        VC_Playing.Priority = 10;

        GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("zoomout");
    }

    public void GameEnd()
    {
        VC_End.Priority = 11;
        isGameEnd = true;
        Invoke("BG_SoundOff", 2f);
    }

    public void BG_SoundOff()
    {
        BG_AudioSource.Stop();
    }

    public void setRespawnPoint(Vector3 Pos)
    {
        RespawnPoint.position = Pos;
    }

    public void ReSpawn()
    {
        player.transform.position = RespawnPoint.position;
        StartCoroutine(ReSpawnEffect());
    }

    IEnumerator ReSpawnEffect()
    {
        SpriteRenderer SR = player.GetComponent<SpriteRenderer>();
        Color c = SR.color;
        isReSpawning = true;

        foreach(Button b in MoveButtons)
        {
            b.interactable = false;
        }

        for(int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                c.a = 0.5f;
            else if (i % 2 == 1)
                c.a = 1f;

            SR.color = c;

            yield return new WaitForSeconds(0.1f);
        }

        foreach(Button b in MoveButtons)
        {
            b.interactable = true;
        }

        isReSpawning = false;
    }


    public void Create_Drop_Platform(Vector3 Pos)
    {
        GameObject go = Instantiate(DropPlatform, Pos, Quaternion.identity);
        go.GetComponent<DropPlatform>().StartAppear();
    }

}
