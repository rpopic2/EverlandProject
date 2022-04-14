using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Player player;

    public void LeftBtnDown()
    {
        player.LeftMove = true;
    }
    public void LeftBtnUp()
    {
        player.LeftMove = false;
    }
    public void RightBtnDown()
    {
        player.RightMove = true;
    }
    public void RightBtnUp()
    {
        player.RightMove = false;
    }

    public void JumpBtnDown()
    {
        player.Jump = true;
    }
}
