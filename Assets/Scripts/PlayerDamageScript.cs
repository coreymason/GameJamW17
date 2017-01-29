using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    [SerializeField]
    int damage = 1;

    void OnCollision2D(Collider2D hit)
    {
        if (hit.GetComponent<EnemyBird>() != null)
        {
            hit.GetComponent<EnemyBird>().DamageEnemy(damage);
        }
    }
}
