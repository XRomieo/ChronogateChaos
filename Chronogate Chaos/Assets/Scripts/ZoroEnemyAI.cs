using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZoroEnemyAI : MonoBehaviour
{
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject missileParent;
    [SerializeField] private GameObject missileParent1;
    [SerializeField] private GameObject laser;
    [SerializeField] private Transform laserSpawner;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject blastFX;
    [SerializeField] private GameObject destroyedGameobject;
    private int enemyCurrentHealth = 500;
    private float nextFireTime;
    private int isLaser = 0;
    private Transform player;
    private bool spotted = false;
    private const string LASER_ATTACK = "Attack";
    private Vector2 targetPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootLaser());
    }

    void Update()
    {
        if (nextFireTime < Time.time) {
            Instantiate(missile, missileParent.transform.position, Quaternion.identity);
            Instantiate(missile, missileParent1.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        if(isLaser == 1) {
            if(!spotted) {
                targetPos = player.position;
                targetPos.y += 1;
                spotted = true;
            }
            var tracer = Instantiate(laser, laserSpawner.position, laserSpawner.rotation);
            tracer.GetComponent<BulletTracers>().setTargetPos(targetPos);
        } else {
            spotted = false;
        }
    }

    public void ChangeLaserState(int state) {
        isLaser = state;
    }

    private IEnumerator ShootLaser() {
        yield return new WaitForSeconds(10f);
        StopAllCoroutines();
        playerAnimator.CrossFadeInFixedTime(LASER_ATTACK, 0.15f);
        StartCoroutine(ShootLaser());
    }

    public void ReceiveDamage(Vector2 point) {
        enemyCurrentHealth -= 1;
        if (enemyCurrentHealth <= 0) {
            DestroyEnemy();
        }
    }
    private void DestroyEnemy() {
        var blast = Instantiate(blastFX, transform.position, Quaternion.identity);
        blast.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(destroyedGameobject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
