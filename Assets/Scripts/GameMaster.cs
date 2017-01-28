using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPreFab;
    public Transform spawnPoint;
    public int spawnDelay = 2;

    void Start() {
        if(gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(spawnDelay);
        //TODO: sound effect?
        Instantiate(playerPreFab, spawnPoint.position, spawnPoint.rotation);
        //TODO: visual effect?
    }

	public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy) {
        Destroy(enemy.gameObject);
    }
}
