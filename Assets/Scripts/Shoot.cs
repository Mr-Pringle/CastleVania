using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    SpriteRenderer sr;
    AudioSourceManager sfxManager;

    public float projectileSpeed;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public Projectile projectilePrefab;
    public AudioClip fireSFX;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sfxManager = GetComponent<AudioSourceManager>();

        if (projectileSpeed <= 0)
            projectileSpeed = 9.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log("Please set up default values in inspector for the Shoot script on " + gameObject.name);
        }
    }

    public void Fire()
    {
        if (sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab,
                spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab,
                spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;
        }

        sfxManager.Play(fireSFX, false);
    }

   
}
