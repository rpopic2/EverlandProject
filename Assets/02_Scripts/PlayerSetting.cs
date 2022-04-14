using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public enum backgroundImage { Spring, Summer, Fall, Winter };
    public enum BGM { Spring, Summer, Fall, Winter }; 

    [SerializeField] backgroundImage _background;
    [SerializeField] BGM _BGM;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
