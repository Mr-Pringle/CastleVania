using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : StateMachineBehaviour
{
    public GameObject explosion;


    protected PlayerController currentPlayer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        currentPlayer = animator.gameObject.GetComponent<PlayerController>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Instantiate(explosion, currentPlayer.groundCheck.position, currentPlayer.groundCheck.rotation);
    }
}
