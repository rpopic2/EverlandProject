using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishPlatform : MonoBehaviour
{
    [SerializeField] float VanishTime;

    SpriteRenderer SR;
    new BoxCollider2D collider;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        start_vanish();
    }

    public void start_vanish()
    {
        if(SR.color.a == 1)
        {
            StartCoroutine(Vanish());
        }
        else
        {
            StartCoroutine(Appear());
        }
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(1.5f);

        Color c = SR.color;

        while(c.a > 0)
        {
            c.a -= 0.1f;

            SR.color = c;

            yield return new WaitForSeconds(VanishTime / 10);
        }

        collider.isTrigger = true;

        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(1.5f);

        Color c = SR.color;

        while (c.a < 1)
        {
            c.a += 0.1f;

            SR.color = c;

            if(c.a > 0.5f)
                collider.isTrigger = false;

            yield return new WaitForSeconds(VanishTime / 10);
        }

        StartCoroutine(Vanish());
    }
}
