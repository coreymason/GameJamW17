using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collInfo)
    {
        EnemyBird  bd = collInfo.collider.GetComponent<EnemyBird>();
        if (bd != null)
        {
            Debug.Log("hitttttttttttttttttt");
            bd.DamageEnemy(1000);
        }
    }
}
