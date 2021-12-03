using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float health = 30.0f;
    public Text hpBar;
    private Transform target;

    [SerializeField] private float basicDamage = 10f;
    [SerializeField] private float basicSpeed = 1f;
    private float canAttack;

    private void Update() {
        if (health <= 0.0f) {
            Debug.Log(name + " died!");
            GameObject.Destroy(gameObject);
        }
        hpBar.text = health.ToString();
    }

    private void FixedUpdate() {
        if (target) {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            target = null;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (basicSpeed <= canAttack) {
                other.gameObject.GetComponent<PlayerStats>().UpdateHealth(-basicDamage);
                canAttack = 0f;
            } else {
                canAttack += Time.deltaTime;
            }
            
        }
    }

    public void TakeDamage(float dmg) {
      health -= dmg;
    }
}
