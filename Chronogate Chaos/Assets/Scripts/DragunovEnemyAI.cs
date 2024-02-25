using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragunovEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootingRange;
    [SerializeField] private float lineOfSite;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletParent;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private GameObject blastFX;
    [SerializeField] private GameObject destroyedGameobject;
    private int enemyCurrentHealth = 30;
    private Transform player;
    private float nextFireTime;
    private bool canAttack = false;
    private const string ENEMY_ATTACK = "Attack";
    private const string PLAYER_TAG = "Player";
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite) {
            canAttack = true;
        }
        if (canAttack) {
            Vector2 targetPos = player.position;
            int direction = 0;
            if (player.position.x > transform.position.x) {
                direction = -1;
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                direction = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            targetPos.y += 5;
            targetPos.x += 3 * direction;
            transform.position = Vector2.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time) {
                enemyAnimator.CrossFadeInFixedTime(ENEMY_ATTACK, 0.15f);
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
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
