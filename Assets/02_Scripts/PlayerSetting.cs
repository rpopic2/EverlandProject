using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSetting : MonoBehaviour
{
    public enum backgroundImage { Morning, Afternoon, Evening, Night };
    public enum BGM { Morning, Afternoon, Evening, Night };

    public Sprite treasureImg;//최종 보물 이미지

    public string playerName;//플레이어 이름 - 엔딩 크레딧에 들어감.

    [SerializeField] backgroundImage _background;
    [SerializeField] BGM _BGM;

    public GameObject username1;
    public GameObject username2;

    public GameObject ShowPanel;
    public GameObject ClosePanel;

    public Sprite[] bgSprite;
    public Sprite[] tentSprite;

    void Start()
    {
        if ( _background == backgroundImage.Morning)
        {
            for (int i = 0; i < 14; i++)
            {
                GameObject.Find("Background_Sky").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = bgSprite[0];
            }
            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[0];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[1];
        }

        else if (_background == backgroundImage.Afternoon)
        {
            for (int i = 0; i < 14; i++)
            {
                GameObject.Find("Background_Sky").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = bgSprite[1];
            }
            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[2];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[3];
        }

        else if (_background == backgroundImage.Evening)
        {
            for (int i = 0; i < 14; i++)
            {
                GameObject.Find("Background_Sky").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = bgSprite[2];
            }
            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[4];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[5];
        }

        else if (_background == backgroundImage.Night)
        {
            for (int i = 0; i < 14; i++)
            {
                GameObject.Find("Background_Sky").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = bgSprite[3];
            }
            GameObject.Find("House_back").GetComponent<SpriteRenderer>().sprite = tentSprite[6];
            GameObject.Find("House_Front").GetComponent<SpriteRenderer>().sprite = tentSprite[7];
        }
        username1.GetComponent<Text>().text = playerName;
        username2.GetComponent<Text>().text = playerName;
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
        ClosePanel.gameObject.SetActive(false);
        ShowPanel.gameObject.SetActive(true);
    }

    public void BackBtn()
    {
        ShowPanel.gameObject.SetActive(false);
        ClosePanel.gameObject.SetActive(true);
    }

    public void CloseBtn()
    {
        Application.Quit();
    }
}
