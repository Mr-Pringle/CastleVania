using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
            lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "PlayerProjectile")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy curEnemy = collision.gameObject.GetComponent<Enemy>();

                if (curEnemy)
                {
                    curEnemy.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }

        if (gameObject.tag == "EnemyProjectile")
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.lives--;
                Destroy(gameObject);
            }
        }
    }
}
