using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats {
        public int health = 1;
    }

    public PlayerStats stats = new PlayerStats();

    void Update() {
        if(transform.position.y <= -20) {
            DamagePlayer(1000000);
        }
    }

    public void DamagePlayer(int damage) {
        stats.health -= damage;
        if (stats.health <= 0) {
            GameMaster.KillPlayer(this);
        }
    }

}
