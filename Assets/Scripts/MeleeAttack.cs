using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    float attackDuration = 0.5f;

    private bool attacking = false;

	// Update is called once per frame
	void Update ()
    {
        CheckForAttack();
	}

    void CheckForAttack()
    {
        if (Input.GetButtonDown("Fire1") && Player.current.isGrounded == true)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (attacking == false)
        {
            gameObject.transform.FindChild("MeleeHitbox").gameObject.SetActive(true);
            Player.current.GetComponent<Animator>().SetTrigger("Attack");
            attacking = true;
            Invoke("DisableHitbox", attackDuration);
        }
    }

    void DisableHitbox()
    {
        gameObject.transform.FindChild("MeleeHitbox").gameObject.SetActive(false);
        attacking = false;
    }
}
