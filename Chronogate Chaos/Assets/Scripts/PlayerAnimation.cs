using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private string currentAnimState;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private RuntimeAnimatorController playerAnimator_Aim;
    [SerializeField] private RuntimeAnimatorController playerAnimator_Normal;
    [SerializeField] private AudioClip[] footstepsSFX;

    public void ChangeAnimationState(string newAnimState) {
        if (currentAnimState != newAnimState) {
            currentAnimState = newAnimState;
            playerAnimator.CrossFadeInFixedTime(newAnimState, 0.15f);
            }
    }

    public void ChangePlayerWeaponState(bool isRangeWeapon) {
        if (isRangeWeapon) { 
            playerAnimator.runtimeAnimatorController = playerAnimator_Aim;
        } else {
            playerAnimator.runtimeAnimatorController = playerAnimator_Normal;
        }
    }

    public Animator GetAnimator() {
        return playerAnimator;
    }

    public void playFootStep() {
        AudioSource.PlayClipAtPoint(footstepsSFX[UnityEngine.Random.Range(0, footstepsSFX.Length)], transform.position);
    }
}
