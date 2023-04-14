using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
	// var for script
	public playermovement player;
	public Rigidbody rb;
	public Transform et;
	public Transform pt;
	public LayerMask layerMask;
	public bool isGrounded;
	public int hp;
	public int maxHp;
	public Text hpDisplay;
	
	void Start()
	{
		maxHp = hp;
	}

	void Update() {
		rb.AddForce(Vector3.down * 10000 * Time.deltaTime);
		isGrounded = Physics.CheckSphere(et.position, 2.5f, layerMask);
	}

	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			// right and left are relative to player
			Vector3 right = pt.right;
			Vector3 forward = Vector3.Cross(right, Vector3.up);
			hpDisplay.text = hp.ToString() + "/" + maxHp.ToString();
			
			hp -= player.damage;
			if (hp <= 0){
				Destroy(gameObject);
			}
			if (player.lastAttack == player.SW5A) {
				rb.AddForce(forward * 10000 * Time.deltaTime);
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 350000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW5AA) {
				rb.AddForce(forward * 10000 * Time.deltaTime);
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW5A2A) {
				if (isGrounded) {
					rb.AddForce(Vector3.up * 250000 * Time.deltaTime);
				} else {
				rb.AddForce(Vector3.up * 400000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW5AAA) {
				rb.AddForce(forward * 500000 * Time.deltaTime);
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW5AA8A) {
				rb.AddForce(forward * 600000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW2A) {
				rb.AddForce(Vector3.up * 500000 * Time.deltaTime);
			}
			if (player.lastAttack == player.SW6A) {
				rb.AddForce(forward * -1 * 20000 * Time.deltaTime);
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 350000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW6AA) {
				rb.AddForce(forward * -1 * 500000 * Time.deltaTime);
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
				}
			}
			if (player.lastAttack == player.SW8A) {
				if (isGrounded) {
					rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
				} else {
				rb.AddForce(Vector3.up * 400000 * Time.deltaTime);
				}
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
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 100000 * Time.deltaTime);
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
				if (!isGrounded) {
					rb.AddForce(Vector3.up * 300000 * Time.deltaTime);
				}
			}
		}
	}
}
