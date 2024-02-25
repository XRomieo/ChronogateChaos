using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootingRange;
    [SerializeField] private float lineOfSite;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletParentRight;
    [SerializeField] private GameObject bulletParentLeft;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private GameObject blastFX;
    [SerializeField] private GameObject destroyedGameobject;
    [SerializeField] private GameObject hitFX;
    private int enemyCurrentHealth = 10;
    private Transform player;
    private bool canAttack = false;
    private const string ENEMY_ATTACK = "Attack";
    private bool isMissile1Destroyed = true;
    private bool isMissile2Destroyed = true;
    private GameObject missile1;
    private GameObject missile2;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (!missile1)
        {
            isMissile1Destroyed = true;
        }
        if (!missile2) {
            isMissile2Destroyed = true;
        }
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite) {
            canAttack = true;
        }
        if (canAttack) {
            Vector2 targetPos = player.position;
            targetPos.y += 7;
            transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            if (distanceFromPlayer <= shootingRange && isMissile1Destroyed && isMissile2Destroyed) {
                enemyAnimator.CrossFadeInFixedTime(ENEMY_ATTACK, 0.15f);
                missile1 = Instantiate(bullet, bulletParentLeft.transform.position, Quaternion.identity);
                missile2 = Instantiate(bullet, bulletParentRight.transform.position, Quaternion.identity);
                isMissile1Destroyed = false;
                isMissile2Destroyed = false;
            }
        }
    }

    public void ReceiveDamage(Vector2 point) {
        enemyCurrentHealth -= 1;
        if (enemyCurrentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy() {
        var blast = Instantiate(blastFX, transform.position, Quaternion.identity);
        blast.transform.localScale = new Vector3(2, 2, 2);
        Instantiate(destroyedGameobject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
