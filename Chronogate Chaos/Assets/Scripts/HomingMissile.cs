using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationModifier;
    [SerializeField] private GameObject blastFX;
    private Transform player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() {
        Vector2 target = player.position;
        target.y += 1;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PlayerGetDamage();
        }
        DestroySelfMissile();
    }

    public void DestroySelfMissile() {
        Instantiate(blastFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
