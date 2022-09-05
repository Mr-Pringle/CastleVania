using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyWalker : Enemy
{

    Rigidbody2D rb;

    public float speed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
        {
            speed = 3;
        }
    }

    // Update is called once per frame
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);

        Debug.Log("Enemy walker took " + dmg + " damage.");
    }

    

    private void Update()
    {
        {
            AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

            if (curClips[0].clip.name == "ZombieWalk")
            {
                if (!sr.flipX)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    public void IsDead()
    {
        anim.SetTrigger("Die");
        rb.velocity = Vector2.zero;
    }
}
