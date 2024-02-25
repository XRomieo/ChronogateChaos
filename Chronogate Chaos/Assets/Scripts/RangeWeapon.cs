using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] Transform bulletRaycastPoint;
    [SerializeField] Transform gunEndPoint;
    [SerializeField] Transform bulletRoundsSpawner;
    [SerializeField] GameObject bulletRounds;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject bulletTracers;
    [SerializeField] float weaponRange = 10f;
    [SerializeField] GameObject hitImpactFX;
    [SerializeField] LayerMask hitLayerMask;
    [SerializeField] bool isAutomatic;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] AudioClip gunShot;
    private float lastfired;  

    private const string ASTRO_TAG = "Astro";
    private const string DRAGUNOV_TAG = "Dragunov";
    private const string ZORO_TAG = "Zoro";
    private const string OBJECT_TAG = "Object";
    private const string MISSILE_TAG = "Missile";

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAutomatic) {
            Vector3 pos = gunEndPoint.position;
            Instantiate(bulletRounds, pos, Quaternion.identity);
            Instantiate(muzzleFlash, gunEndPoint);
            Shoot();
        } else if (Input.GetButton("Fire1")) {
            if (Time.time - lastfired > 1 / fireRate) {
                Vector3 pos = gunEndPoint.position;
                Instantiate(bulletRounds, pos, Quaternion.identity);
                Instantiate(muzzleFlash, gunEndPoint);
                lastfired = Time.time;
                Shoot();
            }
        }

    }

    internal void Shoot() {
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
        AudioSource.PlayClipAtPoint(gunShot, bulletRaycastPoint.position);
        RaycastHit2D objectHit = Physics2D.Raycast(bulletRaycastPoint.position, bulletRaycastPoint.right, weaponRange, hitLayerMask);
        RaycastHit2D tracerHit = Physics2D.Raycast(bulletRaycastPoint.position, bulletRaycastPoint.right, weaponRange);
        var tracer = Instantiate(bulletTracers, gunEndPoint.position, gunEndPoint.rotation);
        if (objectHit) {
            Instantiate(hitImpactFX, tracerHit.point, Quaternion.identity);
            if (objectHit.collider.tag == ASTRO_TAG) {
                objectHit.transform.GetComponent<AstroEnemyAI>().ReceiveDamage(tracerHit.point);
            } else if(objectHit.collider.tag == ZORO_TAG) {
                objectHit.transform.GetComponent<ZoroEnemyAI>().ReceiveDamage(tracerHit.point);
            } else if(objectHit.collider.tag == DRAGUNOV_TAG) {
                objectHit.transform.GetComponent<DragunovEnemyAI>().ReceiveDamage(tracerHit.point);
            } else if(objectHit.collider.tag == OBJECT_TAG) {
                objectHit.transform.GetComponent<ObjectsDamage>().receiveDamage(tracerHit.point);
            } else if(objectHit.collider.tag == MISSILE_TAG) {
                objectHit.transform.GetComponent<HomingMissile>().DestroySelfMissile();
            }
        }
        if (tracerHit) {
            tracer.GetComponent<BulletTracers>().setTargetPos(tracerHit.point);
        } else {
            Vector2 hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tracer.GetComponent<BulletTracers>().setTargetPos(hitPoint);
        }
    }
}
