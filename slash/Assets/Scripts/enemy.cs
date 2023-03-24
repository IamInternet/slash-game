using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	// var for script
	public playermovement player;

	int hp = 200;
	double stun = 0; // if > 0 will not run enemy behaviour

	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			hp -= player.damage;
			stun += 30;
			if (hp <= 0){
				Destroy(gameObject);
			}
		}
		Debug.Log(hp);
	}
}
