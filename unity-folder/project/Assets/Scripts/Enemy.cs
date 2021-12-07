using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, EnemyInterface
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
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>().Logger.GetComponentInChildren<FeedInvoker>().ItemAddIndicator("Tinder", 1);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerActions>().Logger.GetComponentInChildren<FeedInvoker>().ItemAddIndicator("Seed", 1);
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
      Debug.Log(GetScale().x + " " + GetScale().y + " " + GetScale().z);
    }

    ///////////////////////////////////////////////
    // Enemy Interface Function Implementations: //
    ///////////////////////////////////////////////

    public Vector2 GetPosition() {
      return transform.position;
    }

    public void SetPosition(Vector2 newPosition) {
      transform.position = newPosition;
    }
  
    public Vector3 GetScale() {
      return transform.localScale;
    }

    public void SetScale(Vector3 newScale) {
      transform.localScale = newScale;
    }
    public Color GetColor() {
      return GetComponent<SpriteRenderer>().color;
    }
    public void SetColor(Color setColor) {
      GetComponent<SpriteRenderer>().color = setColor;
    }
    public float GetSpeed() {
      return speed;
    }
    public void SetSpeed(float newSpeed) {
      speed = newSpeed;
    }
    public float GetHealth() {
      return health;
    }
    public void SetHealth(float newHealth) {
      health = newHealth;
    }
    public float GetBasicDMG() {
      return basicDamage;
    }
    public void SetBasicDMG(float newBasicDMG) {
      basicDamage = newBasicDMG;
    }
    public float GetBasicSpeed() {
      return basicSpeed;
    }
    public void SetBasicSpeed(float newBasicSpeed) {
      basicSpeed = newBasicSpeed;
    }
}
