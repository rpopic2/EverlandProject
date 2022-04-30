using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    [Header("Initialize")]
    [SerializeField] Transform StartPos;
    [SerializeField] Transform GoalPos;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Image FillAreaImg;
    [SerializeField] Image PlayerIconImg;

    float GoalDistance;
    float PlayerDistance;

    void Start()
    {
        GoalDistance = GoalPos.position.x - StartPos.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDistance = PlayerPos.position.x - StartPos.position.x;

        FillAreaImg.fillAmount = PlayerDistance / GoalDistance;
    }
}
