using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	int hp = 10;

	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			hp--;
			if (hp <= 0){
				Destroy(gameObject);
			}
		}
		Debug.Log(hp);
	}
}
