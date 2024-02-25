using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationModifier;
    [SerializeField] private GameObject blastFX;
    private GameObject target;
    private Rigidbody2D fireballRB;

    void Start() {
        fireballRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        fireballRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PlayerGetDamage();
        }
        DestroySelf();
    }

    public void DestroySelf() {
        Instantiate(blastFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
