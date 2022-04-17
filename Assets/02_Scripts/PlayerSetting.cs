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

    void Start()
    {
        username1.GetComponent<Text>().text = playerName;
        username2.GetComponent<Text>().text = playerName;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
