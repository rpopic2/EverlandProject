using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSetting : MonoBehaviour
{
    public enum backgroundImage { everland };
    //public enum BGM { Morning, Afternoon, Evening, Night };

    //public Sprite treasureImg;//최종 보물 이미지

    public string playerName;//플레이어 이름 - 엔딩 크레딧에 들어감.

    [SerializeField] public backgroundImage _background;
    //[SerializeField] BGM _BGM;

    public GameObject username1;
    public GameObject username2;

    public GameObject name1;
    public GameObject name2;
    public GameObject name3;
    public GameObject name4;
    public GameObject name5;


    public GameObject ShowPanel;
    public GameObject ClosePanel;

    public Sprite[] frontSprite;
    public Sprite[] backSprite;
    public Sprite[] cloudSprite;
    public Sprite[] skySprite;

    
    public GameObject[] TileMaps;

    public GameObject[] NormalPlatforms;
    public GameObject[] VanishPlatforms;
    public GameObject[] DropPlatforms;
    public GameObject[] MovingPlatforms;
    public GameObject[] Small_NormalPlatforms;

    public Sprite[] Large_Platform_sprites;
    public Sprite[] Middle_Platform_sprites;
    public Sprite[] Small_Platform_sprites;

    void Start()
    {

        username1.GetComponent<Text>().text = playerName;
        username2.GetComponent<Text>().text = playerName;
        name1.GetComponent<Text>().text = playerName;
        name2.GetComponent<Text>().text = playerName;
        name3.GetComponent<Text>().text = playerName;
        name4.GetComponent<Text>().text = playerName;
        name5.GetComponent<Text>().text = playerName;
        //TileMaps[(int)_background].SetActive(true);

        foreach (GameObject go in NormalPlatforms)
        {
            go.GetComponent<SpriteRenderer>().sprite = Large_Platform_sprites[(int)_background];
        }

        foreach(GameObject go in VanishPlatforms)
        {
            go.GetComponent<SpriteRenderer>().sprite = Middle_Platform_sprites[(int)_background];
        }

        foreach(GameObject go in DropPlatforms)
        {
            go.GetComponent<SpriteRenderer>().sprite = Middle_Platform_sprites[(int)_background];
        }

        foreach(GameObject go in MovingPlatforms)
        {
            go.GetComponent<SpriteRenderer>().sprite = Middle_Platform_sprites[(int)_background];
        }

        foreach(GameObject go in Small_NormalPlatforms)
        {
            go.GetComponent<SpriteRenderer>().sprite = Small_Platform_sprites[(int)_background];
        }
        
        if ( _background == backgroundImage.everland)
        {
            GameObject.Find("Background_Sky").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = frontSprite[0];

            GameObject.Find("Background_Sky").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = backSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[0];

            GameObject.Find("Background_Sky").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = cloudSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = cloudSprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = cloudSprite[0];

            GameObject.Find("Background_Sky").transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = skySprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sprite = skySprite[0];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sprite = skySprite[0];
        }

        /*else if (_background == backgroundImage.Afternoon)
        {
            GameObject.Find("Background_Sky").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = frontSprite[1];

            GameObject.Find("Background_Sky").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = backSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[1];

            GameObject.Find("Background_Sky").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = cloudSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = cloudSprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = cloudSprite[1];

            GameObject.Find("Background_Sky").transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = skySprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sprite = skySprite[1];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sprite = skySprite[1];

            GameObject.Find("BG_Sun&Particle").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = particleSprite[1];
            GameObject.Find("BG_Sun&Particle").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = particleSprite[4];
            GameObject.Find("BG_Sun&Particle").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sunSprite[1];

            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[2];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[3];
        }

        else if (_background == backgroundImage.Evening)
        {
            GameObject.Find("Background_Sky").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = frontSprite[2];

            GameObject.Find("Background_Sky").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = backSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[2];

            GameObject.Find("Background_Sky").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = cloudSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = cloudSprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = cloudSprite[2];

            GameObject.Find("Background_Sky").transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = skySprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sprite = skySprite[2];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sprite = skySprite[2];

            GameObject.Find("BG_Sun&Particle").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = particleSprite[2];
            GameObject.Find("BG_Sun&Particle").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sunSprite[2];

            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[4];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[5];
        }

        else if (_background == backgroundImage.Night)
        {
            GameObject.Find("Background_Sky").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = frontSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = frontSprite[3];

            GameObject.Find("Background_Sky").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = backSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = backSprite[3];

            GameObject.Find("Background_Sky").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = cloudSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(0).GetComponent<SpriteRenderer>().sprite = cloudSprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(2).GetChild(1).GetComponent<SpriteRenderer>().sprite = cloudSprite[3];

            GameObject.Find("Background_Sky").transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = skySprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().sprite = skySprite[3];
            GameObject.Find("Background_Sky").transform.GetChild(3).GetChild(1).GetComponent<SpriteRenderer>().sprite = skySprite[3];

            GameObject.Find("BG_Sun&Particle").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = particleSprite[3];
            GameObject.Find("BG_Sun&Particle").transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sunSprite[3];

            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[6];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[7];
        }*/
        

    }

    public void HeadPhonePlz()
    {
        GameObject.Find("HeadPhonePlz").SetActive(false);
    }

    public IEnumerator FadeOut(float time)
    {
        Color color = GameObject.Find("HeadPhonePlz").GetComponent<Image>().color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            GameObject.Find("HeadPhonePlz").GetComponent<Image>().color = color;
            yield return null;
        }
    }

    public void ShowCloseBtn()
    {
        GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("Negativeclick");
        ClosePanel.gameObject.SetActive(false);
        ShowPanel.gameObject.SetActive(true);
    }

    public void BackBtn()
    {
        GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("click");
        ShowPanel.gameObject.SetActive(false);
        ClosePanel.gameObject.SetActive(true);
    }

    public void CloseBtn()
    {
        GameObject.Find("Canvas").GetComponent<ButtonController>().PlaySound("click");
        Application.Quit();
    }
}
