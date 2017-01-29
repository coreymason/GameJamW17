using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : MonoBehaviour {

    public CameraShake cameraShake;

    [System.Serializable]
    public class EnemyStats {
        public int health = 1;
    }

    public EnemyStats stats = new EnemyStats();
    public Transform deathParticles;

    void start() {
        if(deathParticles == null) {
            //Debug.Log("No death particles");
        }
    }

    public void DamageEnemy(int damage) {
        stats.health -= damage;
        if(stats.health <= 0) {
            GameMaster.KillEnemyBird(this);
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo) {
        Player player = colInfo.collider.GetComponent<Player>();
        if(player != null) {
            cameraShake.Shake(.3f, 1f);
            player.DamagePlayer(1);
            DamageEnemy(100000);
        }
    }

}
