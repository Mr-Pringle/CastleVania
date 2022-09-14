using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    Shoot shootScript;
    public float turretFireDistance;
    public float projectileFireRate;
    float timeSinceLastFire;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        AnimatorClipInfo[] currentClips = anim.GetCurrentAnimatorClipInfo(0);

        if (currentClips[0].clip.name != "SkeletonFire")
        {
            if (GameManager.instance.playerInstance)
            {
                if (GameManager.instance.playerInstance.transform.position.x < transform.position.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
            }

            float distance = Vector2.Distance(GameManager.instance.playerInstance.transform.position, transform.position);

            if (distance <= turretFireDistance)
            {
                sr.color = Color.white;
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
            else
            {
                sr.color = Color.red;
            }
        }

    }

    public override void DestroyMyself()
    {
        base.DestroyMyself();
    }
}
