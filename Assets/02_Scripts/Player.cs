using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;

    Rigidbody2D RB;
    Vector3 moveVelocity = Vector2.zero;

    public bool LeftMove = true;
    public bool RightMove = true;
    public bool Jump = false;
    public bool isGround;
    public bool isMove = false;
    public bool isFinish = false;

    Animator animator;

    public GameObject treasure;
    public GameObject particle;
    public GameObject BtnUI;
    public GameObject chest;
    public GameObject FinishPoint;
    public GameObject EndingCredit;
    public GameObject EndingEffect;

    [SerializeField] Cinemachine.CinemachineVirtualCamera VC_Start;
    [SerializeField] Cinemachine.CinemachineVirtualCamera VC_Playing;

    int playerLayer, platformLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        isGround = true;

        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        input();
        Move();//무브가 꺼져야지 인풋의 애니메이션이 작동함.

        if (isMove == true)
        {
            moveVelocity = Vector3.right;
            transform.position += moveVelocity * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isRunning", true);
        }

        if (RB.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        else
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
        
    }

    public void input()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        moveVelocity = new Vector3(xInput, 0, 0);

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;


        if (xInput == 1 || xInput == -1)
        {
            animator.SetBool("isRunning", true);
            //animator.SetBool("isJumpping", false);
        }

        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumpping", false);
        }

        if ((xInput == 1 && Input.GetKeyDown(KeyCode.Space)) || (xInput == -1 && Input.GetKeyDown(KeyCode.Space)))
        {
            if (isGround)
            {
                isGround = false;

                RB.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                RB.AddForce(jumpVelocity, ForceMode2D.Impulse);
                animator.SetBool("isRunning", true);
                animator.SetBool("isJumpping", true);
            }
        }

        if (xInput == 1)//오른쪽 바라보게
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
        else if (xInput == -1)//왼쪽 바라보게
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                isGround = false;

                RB.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                RB.AddForce(jumpVelocity, ForceMode2D.Impulse);
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumpping", true);
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
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("isRunning", true);
            }
            else if (RightMove)
            {
                moveVelocity = Vector3.right;
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;
                transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("isRunning", true);
            }

            if (Jump && isGround)
            {
                isGround = false;
                Jump = false;

                Debug.Log("Jump");
            
                RB.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                RB.AddForce(jumpVelocity, ForceMode2D.Impulse);

                animator.SetTrigger("doJumping");
                animator.SetBool("isJumpping", true);
                //animator.SetBool("isRunning", false);
            }

            if (!LeftMove && !RightMove)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumpping", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.CompareTag("SavePoint"))
        {
            GameManager.instance.setRespawnPoint(collision.transform.position);
        }
        else if (collision.CompareTag("DeadZone"))
        {
            GameManager.instance.ReSpawn();
            Invoke("DamageEffect", 0.2f);
            Invoke("HealEffect", 0.8f);
        }
        else if (collision.CompareTag("EquipPoint"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (collision.CompareTag("Goal"))
        {
            // goal effect
            BtnUI.gameObject.SetActive(false);
            collision.GetComponent<Animator>().SetBool("Open", true);
            Invoke("ParticleShow", 0.5f);
        }
        else if (collision.CompareTag("FinishLine") && isFinish == false)
        {
            Debug.Log("끝");

            GameManager.instance.GameEnd();

            isMove = false;
            RightMove = false;
            LeftMove = false;

            Invoke("FinishJump", 3f);
            Invoke("FinishJump", 4.5f);
            Invoke("FinishJump", 6f);

            isFinish = true;

            Invoke("EndingShow", 7.5f);
            Invoke("Ending", 8.7f);
        
        }
    }

    public void TreasureShow()
    {
        if (isMove == false)
        {
            VC_Start.Priority = 10;
            VC_Playing.Priority = 9;
            treasure.GetComponent<SpriteRenderer>().sprite = PlayerSetting.FindObjectOfType<PlayerSetting>().treasureImg;
            treasure.gameObject.SetActive(true);
            chest.gameObject.SetActive(false);
            //isMove = true;//버튼 형식이면 이거 주석처리 해야 멈춤 -> 버튼이 계속 입력으로 처리 되어서 그런듯
        }
    }

    public void ParticleShow()
    {
        particle.gameObject.SetActive(true);
        Invoke("TreasureShow", 1f);
    }

    public void FinishJump()
    { 
        if(isGround)
        {
            isGround = false;
            Jump = false;

            RB.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            RB.AddForce(jumpVelocity, ForceMode2D.Impulse);
            animator.SetBool("isJumpping", true);
        }
    }
    public void DamageEffect()
    {
        Color color1 = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color;
        color1.a = 0.5f;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = color1;

        Color color2 = transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color;
        color2.a = 0.5f;
        transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = color2;

        Color color3 = transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color;
        color3.a = 0.5f;
        transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = color3;


        Color color4 = transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>().color;
        color4.a = 0.5f;
        transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>().color = color4;


        Color color5 = transform.GetChild(0).GetChild(5).GetComponent<SpriteRenderer>().color;
        color5.a = 0.5f;
        transform.GetChild(0).GetChild(5).GetComponent<SpriteRenderer>().color = color5;

        Color color6 = transform.GetChild(0).GetChild(6).GetComponent<SpriteRenderer>().color;
        color6.a = 0.5f;
        transform.GetChild(0).GetChild(6).GetComponent<SpriteRenderer>().color = color6;

        //BtnUI.gameObject.SetActive(false);
    }

    public void HealEffect()
    {
        Color color1 = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color;
        color1.a = 1f;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = color1;

        Color color2 = transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color;
        color2.a = 1f;
        transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = color2;

        Color color3 = transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color;
        color3.a = 1f;
        transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = color3;


        Color color4 = transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>().color;
        color4.a = 1f;
        transform.GetChild(0).GetChild(4).GetComponent<SpriteRenderer>().color = color4;


        Color color5 = transform.GetChild(0).GetChild(5).GetComponent<SpriteRenderer>().color;
        color5.a = 1f;
        transform.GetChild(0).GetChild(5).GetComponent<SpriteRenderer>().color = color5;

        Color color6 = transform.GetChild(0).GetChild(6).GetComponent<SpriteRenderer>().color;
        color6.a = 1f;
        transform.GetChild(0).GetChild(6).GetComponent<SpriteRenderer>().color = color6;

        BtnUI.gameObject.SetActive(true);
    }
    public void Ending()
    {
        EndingEffect.gameObject.SetActive(false);
        EndingCredit.gameObject.SetActive(true);
    }

    public void EndingShow()
    {
        EndingEffect.gameObject.SetActive(true);
    }

    public void ClickToFinish()
    { 
    
    }    
}
