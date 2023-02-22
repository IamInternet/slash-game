using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	// var for script
	public playermovement player;

	int hp = 50;

	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			hp -= player.damage;
			if (hp <= 0){
				Destroy(gameObject);
			}
		}
		Debug.Log(hp);
	}
}
