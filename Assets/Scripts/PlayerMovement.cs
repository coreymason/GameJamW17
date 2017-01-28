using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 0.0f;
    [SerializeField]
    float fallSpeed = 1.0f;
    [SerializeField]
    float jumpPower = 1.0f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveHorizontal();
        Jump();
	}

    void MoveHorizontal()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rb.velocity.y * fallSpeed) * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpPower);
        }
    }
}
