using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //===================PlatformPrefab
    [SerializeField] GameObject DropPlatform;
    //===================PlatformPrefab

    public bool isGameStart;
    public bool isGameEnd;
    public bool isReSpawning;

    public AudioClip intro;
    public AudioClip Ending;

    public AudioClip Morning;
    public AudioClip Afternoon;
    public AudioClip Evening;
    public AudioClip Night;

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isGameEnd = false;
        isReSpawning = false;
        Invoke("PlayIntro", 2.5f);
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
                this.GetComponent<AudioSource>().clip = intro;
                break;

            case "morning":
                this.GetComponent<AudioSource>().clip = Morning;
                break;

            case "afternoon":
                this.GetComponent<AudioSource>().clip = Afternoon;
                break;
            
            case "evening":
                this.GetComponent<AudioSource>().clip = Evening;
                break;

            
            case "night":
                this.GetComponent<AudioSource>().clip = Night;
                break;
            
            case "ending":
                this.GetComponent<AudioSource>().clip = Ending;
                break;
        }
        this.GetComponent<AudioSource>().Play();
    }

    public void PlayIntro()
    {
        PlaySound("intro");
    }
    public void GameStart_Button()
    {
        if (!isGameStart)
        {
            isGameStart = true;
            Start_Panel.SetActive(false);
            Player_UI_Panel.SetActive(true);

            if (GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>()._background == PlayerSetting.backgroundImage.Morning)
            {
                PlaySound("morning");
            }

            else if (GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>()._background == PlayerSetting.backgroundImage.Afternoon)
            {
                PlaySound("afternoon");
            }

            else if (GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>()._background == PlayerSetting.backgroundImage.Evening)
            {
                PlaySound("evening");
            }

            else if (GameObject.Find("PlayerSetting").GetComponent<PlayerSetting>()._background == PlayerSetting.backgroundImage.Night)
            {
                PlaySound("night");
            }

            VC_Start.Priority = 9;
            VC_Playing.Priority = 10;
        }
    }

    public void GameEnd()
    {
        VC_End.Priority = 11;
        isGameEnd = true;
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

        for(int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                c.a = 0.5f;
            else if (i % 2 == 1)
                c.a = 1f;

            SR.color = c;

            yield return new WaitForSeconds(0.1f);
        }

        isReSpawning = false;
    }


    public void Create_Drop_Platform(Vector3 Pos)
    {
        GameObject go = Instantiate(DropPlatform, Pos, Quaternion.identity);
        go.GetComponent<DropPlatform>().StartAppear();
    }

}
