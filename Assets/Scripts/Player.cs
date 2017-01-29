using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player current;

    [SerializeField]
    public float walkSpeed = 0.0f;
    [SerializeField]
    float jumpPower = 1.0f;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public bool isGrounded;

    [System.Serializable]
    public class PlayerStats
    {
        public int health = 1;
    }

    public PlayerStats stats = new PlayerStats();

    void Awake()
    {
        current = this;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveHorizontal();
        Jump();
        if (transform.position.y <= -20)
        {
            DamagePlayer(1000000);
        }
    }

    void MoveHorizontal()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (isGrounded)
        {
            isGrounded = false;
        }
    }

    public void DamagePlayer(int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            // GameMaster.KillPlayer(this);
        }
    }

}