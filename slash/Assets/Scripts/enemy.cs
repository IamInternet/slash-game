using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	// var for script
	public playermovement player;
	public Rigidbody rb;
	public Transform et;
	public Transform pt;

	int hp = 2000;
	double stun = 0; // if > 0 will not run enemy behaviour

	void Update() {
		rb.AddForce(Vector3.down * 10000 * Time.deltaTime);
	}

	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			// right and left are relative to player
			Vector3 right = pt.right;
			Vector3 forward = Vector3.Cross(right, Vector3.up);
			
			hp -= player.damage;
			stun += 30;
			if (hp <= 0){
				Destroy(gameObject);
			}
			if (player.lastAttack == player.SW5A) {
				rb.AddForce(forward * 20000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW5AA) {
				rb.AddForce(forward * 20000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW5A2A) {
				rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW5AAA) {
				rb.AddForce(forward * 200000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW5AA8A) {
				rb.AddForce(forward * 200000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW2A) {
				rb.AddForce(Vector3.up * 500000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW6A) {
				rb.AddForce(forward * -1 * 20000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW6AA) {
				rb.AddForce(forward * -1 * 200000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW8A) {
				rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW4A) {
				if (pt.position.z < et.position.z) {
					rb.AddForce(forward * -1 * 20000 * Time.deltaTime);
				} else {
					rb.AddForce(forward * 20000 * Time.deltaTime);
                }
				if (pt.position.x < et.position.x) {
					rb.AddForce(right * -1 * 20000 * Time.deltaTime);
				} else {
					rb.AddForce(right * 20000 * Time.deltaTime);
                }
			}
			if (player.lastAttack == player.SW4AA) {
				if (pt.position.z < et.position.z) {
					rb.AddForce(forward * 500000 * Time.deltaTime);
				} else {
					rb.AddForce(forward * -1 * 500000 * Time.deltaTime);
                }
				if (pt.position.x < et.position.x) {
					rb.AddForce(right * 500000 * Time.deltaTime);
				} else {
					rb.AddForce(right * -1 * 500000 * Time.deltaTime);
                }
			}

		}
		Debug.Log(hp);
	}
}
