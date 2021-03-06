﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPreFab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public CameraShake cameraShake;

    void Start() {
        if(gm == null) {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        if (cameraShake == null) {
            Debug.Log("No Camera Shake");
        }
    }

    public IEnumerator RespawnPlayer() { //TODO: reset level
        yield return new WaitForSeconds(spawnDelay);
        //TODO: sound effect?
        Instantiate(playerPreFab, spawnPoint.position, spawnPoint.rotation);
        //TODO: visual effect?
    }

    public void ShakeEffect() {
        Debug.Log("shaking?");
        cameraShake.Shake(.3f, 1f);
    }

    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemyBird(EnemyBird enemy) {
        GameObject part = Instantiate(enemy.deathParticles, enemy.transform.position, Quaternion.identity).gameObject;
        Destroy(part, 2);
        Destroy(enemy.gameObject);
    }
}
