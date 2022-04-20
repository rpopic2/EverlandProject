using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSetting : MonoBehaviour
{
    public enum backgroundImage { Spring, Summer, Fall, Winter };
    public enum BGM { Spring, Summer, Fall, Winter };

    public Sprite treasureImg;//최종 보물 이미지

    public string playerName;//플레이어 이름 - 엔딩 크레딧에 들어감.

    [SerializeField] backgroundImage _background;
    [SerializeField] BGM _BGM;

    public GameObject username1;
    public GameObject username2;

    public GameObject ShowPanel;
    public GameObject ClosePanel;


    float time = 0f;
    float fadeTime = 2f;


    void Start()
    {
        username1.GetComponent<Text>().text = playerName;
        username2.GetComponent<Text>().text = playerName;
        //StartCoroutine("FadeOut", 3);
        //Invoke("HeadPhonePlz", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
