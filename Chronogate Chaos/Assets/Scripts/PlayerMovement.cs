using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private float horizontal;
    private bool isFacingRight = true;
    private bool canPlayerMove = true;
    private bool isRangeWeapon = true;
    private bool canSlide = true;
    private bool isSliding = false;
    private GameObject mainCamera;
    private int playerHealth = 20;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float slidePower = 2f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Transform playerRotator;
    [SerializeField] private Transform spine;
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform rightArm;
    [SerializeField] private Transform bulletShooter;
    [SerializeField] private CapsuleCollider2D normalCollider;
    [SerializeField] private CapsuleCollider2D slideCollider;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private GameManager gameManager;

    private const string MAIN_CAMERA = "MainCamera";
    // Animations Name
    private const string PLAYER_IDLE = "Idle";
    private const string PLAYER_WALK = "Walk";
    private const string JUMP_START = "JumpStart";
    private const string SLIDE_START = "SlideStart";

    private void Awake() {
        mainCamera = GameObject.FindGameObjectWithTag(MAIN_CAMERA);
        slideCollider.enabled = false;
        normalCollider.enabled = true;
    }

    void Update() {
        if (!isSliding) { 
            playerAnimation.ChangePlayerWeaponState(isRangeWeapon);
        }
        if (isRangeWeapon && !isSliding) {
            
            PlayerAimLookAt();
        }
        if (canPlayerMove) {
            horizontal = Input.GetAxisRaw("Horizontal");

            if(Input.GetButtonDown("Slide") && canSlide && IsGrounded() && rb.velocity.x != 0f) {
                StartCoroutine(PlayerSlide());
            }

            if (Input.GetButtonDown("Jump") && IsGrounded() && !isSliding) {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (!IsGrounded() && !isSliding) {
                playerAnimation.ChangeAnimationState(JUMP_START);
            }
            if (!isRangeWeapon && !isSliding) {
                Flip();
            }
        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void FixedUpdate() {
        if (!isSliding) {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (IsGrounded()) {
                if (rb.velocity.x != 0f) {
                    playerAnimation.ChangeAnimationState(PLAYER_WALK);
                } else {
                    playerAnimation.ChangeAnimationState(PLAYER_IDLE);
                }
            }
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void PlayerAimLookAt() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perendicular = spine.position - mousePos;
        Quaternion val = Quaternion.LookRotation(Vector3.forward, perendicular);
        spine.rotation = val;
        val *= Quaternion.Euler(0, 0, -90);
        if (transform.position.x - mousePos.x > 0 && isFacingRight) {
            Flip(true);
            bulletShooter.rotation *= Quaternion.Euler(0f, 180f, 0f);
        } else if (transform.position.x - mousePos.x < 0 && !isFacingRight) {
            Flip(true);
            bulletShooter.rotation *= Quaternion.Euler(0f, -180f, 0f);
        }
    }

    private void Flip(bool isForced = false) {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f || isForced) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = playerRotator.localScale;
            localScale.x *= -1f;
            playerRotator.localScale = localScale;
        }
    }

    private IEnumerator PlayerSlide() {
        rb.velocity = new Vector2(horizontal * speed * slidePower, rb.velocity.y);
        playerAnimation.ChangePlayerWeaponState(false);
        slideCollider.enabled = true;
        normalCollider.enabled = false;
        isSliding = true;
        playerAnimation.ChangeAnimationState(SLIDE_START);
        yield return new WaitForSeconds(0.6f);
        playerAnimation.ChangePlayerWeaponState(isRangeWeapon);
        slideCollider.enabled = false;
        normalCollider.enabled = true;
        isSliding = false;
        canSlide = false;
        StartCoroutine(PlayerRefreshSlide());
    }

    private IEnumerator PlayerRefreshSlide() {
        yield return new WaitForSeconds(0.4f);
        canSlide = true;
    }

    public void PlayerGetDamage() {
        playerHealth -= 1;
        playerHealthBar.fillAmount = (float)playerHealth / 20f;
        if (playerHealth <= 0) {
            gameManager.RestartGameScene();
        }
    }
}
