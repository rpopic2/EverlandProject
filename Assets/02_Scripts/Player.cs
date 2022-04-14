using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    Rigidbody2D RB;
    Vector3 moveVelocity = Vector2.zero;

    public bool LeftMove;
    public bool RightMove;
    public bool Jump;
    public bool isGround;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        isGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        input();
        Move();
    }

    public void input()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        moveVelocity = new Vector3(xInput, 0, 0);

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                isGround = false;

                RB.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                RB.AddForce(jumpVelocity, ForceMode2D.Impulse);
            }
        }
    }

    public void Move()
    {
        if (!GameManager.instance.isReSpawning)
        {
            if (LeftMove)
            {
                moveVelocity = Vector3.left;
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;
            }
            else if (RightMove)
            {
                moveVelocity = Vector3.right;
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;
            }

            if (Jump && isGround)
            {
                isGround = false;
                Jump = false;

                Debug.Log("Jump");

                RB.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                RB.AddForce(jumpVelocity, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        
        if(collision.CompareTag("SavePoint"))
        {
            GameManager.instance.setRespawnPoint(collision.transform.position);
        }
        else if(collision.CompareTag("DeadZone"))
        {
            GameManager.instance.ReSpawn();
        }
        else if (collision.CompareTag("EquipPoint"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(collision.CompareTag("Goal"))
        {
            // goal effect
            collision.GetComponent<Animator>().SetBool("Open", true);
        }
    }

}
