using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * 1.001f;
	}
}
