using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MovingType { Horizontal_Left, Horizontal_Right, Vertical_Down, Vertical_Up };

    [SerializeField] MovingType moveType;
    [SerializeField] float moveArea;
    [SerializeField] float moveSpeed;

    bool isTruning;

    Vector3 currPos;
    Vector3 returnPos;

    void Start()
    {
        isTruning = false;
        initialize_Pos();
    }

    void Update()
    {
        calc_distance();
        move_platform();
    }

    private void initialize_Pos()
    {
        currPos = transform.position;

        switch(moveType)
        {
            case MovingType.Vertical_Down:
                returnPos = transform.position + new Vector3(0, -moveArea, 0);
                break;
            case MovingType.Vertical_Up:
                returnPos = transform.position + new Vector3(0, moveArea, 0);
                break;
            case MovingType.Horizontal_Left:
                returnPos = transform.position + new Vector3(-moveArea, 0, 0);
                break;
            case MovingType.Horizontal_Right:
                returnPos = transform.position + new Vector3(moveArea, 0, 0);
                break;
        }
    }

    private void calc_distance()
    {
        if (Vector3.Distance(transform.position, returnPos) < 0.5f)
        {
            isTruning = true;
        }
        else if (Vector3.Distance(transform.position, currPos) < 0.5f)
        {
            isTruning = false;
        }
    }

    private void move_platform()
    {
        if(isTruning)
        {
            transform.position = Vector3.Lerp(transform.position, currPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, returnPos, moveSpeed * Time.deltaTime);
        }
    }
}
