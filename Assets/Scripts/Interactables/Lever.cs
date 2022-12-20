using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource leverSound;
    [SerializeField] private LeverList connectedList;
    [SerializeField] private int id;

    public override void Interact()
    {
        TurnLever();
    }

    

    private void TurnLever()
    {
        if (!AnimatorIsPlaying() && !connectedList.solved)
        {
            anim.SetTrigger("swap");
            leverSound.Play();
            connectedList.LeverTurned(id.ToString());
        }
    }
    private bool AnimatorIsPlaying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
