using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracers : MonoBehaviour
{
    private Vector2 endPos;
    private float progress;
    private float speed = 40f;

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector2.Lerp(transform.position, endPos, progress);
    }

    public void setTargetPos(Vector2 pos) {  endPos = pos; }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Player") {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PlayerGetDamage();
        }
    }
}
