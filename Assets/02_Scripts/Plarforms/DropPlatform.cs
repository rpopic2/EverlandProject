using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    [SerializeField] float respawnTime;

    BoxCollider2D collider;
    Rigidbody2D RB;
    SpriteRenderer SR;
    Vector3 currPos;
    bool isDrop;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        isDrop = false;
        currPos = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDrop)
        {
            StartCoroutine(Drop());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DeadZone"))
        {
            GameManager.instance.Create_Drop_Platform(currPos);
            Destroy(this.gameObject);
        }
    }

    public void StartAppear()
    {
        StartCoroutine(Appear());
    }

    public IEnumerator Appear()
    {
        if (collider == null)
            collider = GetComponent<BoxCollider2D>();

        collider.enabled = false;

        yield return new WaitForSecondsRealtime(respawnTime);

        Color c = SR.color;
        
        while(c.a < 1)
        {
            SR.color = c;
            c.a += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        collider.enabled = true;
    }

    IEnumerator Drop()
    {
        isDrop = true;

        Vector3 direction;

        for(int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
                direction = Vector3.right * 0.1f;
            else
                direction = Vector3.left * 0.1f;

            transform.position = transform.position += direction;

            yield return new WaitForSeconds(0.1f);
        }

        RB.bodyType = RigidbodyType2D.Dynamic;
        RB.gravityScale = 1;
    }
}
