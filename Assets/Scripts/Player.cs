using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    float walkSpeed = 0.0f;
    [SerializeField]
    float fallSpeed = 1.0f;
    [SerializeField]
    float jumpPower = 1.0f;
    private Rigidbody2D rb;
    private bool isGrounded;

    [System.Serializable]
    public class PlayerStats {
        public int health = 1;
    }

    public PlayerStats stats = new PlayerStats();

    void start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        MoveHorizontal();
        Jump();
        if (transform.position.y <= -20) {
            DamagePlayer(1000000);
        }
    }

    void MoveHorizontal() {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rb.velocity.y * fallSpeed) * Time.deltaTime;
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector2.up * jumpPower);
        }
    }

    void OnCollisionStay(Collision coll) {
        isGrounded = true;
    }

    void OnCollisionExit(Collision coll) {
        if (isGrounded) {
            isGrounded = false;
        }
    }

    public void DamagePlayer(int damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            GameMaster.KillPlayer(this);
        }
    }

}
