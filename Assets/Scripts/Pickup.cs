using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Pickups
    {
        Powerups,
        Life,
        Score

    }

    public Pickups currentPickup;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayer = collision.gameObject.GetComponent<PlayerController>();
            AudioSourceManager sfxManager = collision.gameObject.GetComponent<AudioSourceManager>();

            switch (currentPickup)
            {
                case Pickups.Life:
                    GameManager.instance.lives++;
                    Debug.Log("Life was picked up");
                    break;
                case Pickups.Powerups:
                    curPlayer.StartJumpForceChange();
                    Debug.Log("Powerup was picked up");
                    break;
                case Pickups.Score:
                    GameManager.instance.score += 50;
                    Debug.Log("Score was picked up");
                    break;
            }
            sfxManager.Play(pickupSound, false);

            Destroy(gameObject);
        }
    }
}
