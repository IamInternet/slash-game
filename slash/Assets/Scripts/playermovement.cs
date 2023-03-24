using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
dev notes basically:
StartCoroutine, 
attack method should be a coroutine, allows it to stop. useful for multi hits, and will be used to stop hitboxes
if enable/disable doesn't work, try disable trigger in the object. IT WORKED though
*/

public class playermovement : MonoBehaviour
{
	public float gravity;
	// movement vars
	public float speed;
	bool canCancel = true;
	bool canJCancel = true;
	bool canMove = true;
	Rigidbody rb;
	// jumping vars
	public float jumpForce;
	public Transform playerTransform;
	public LayerMask layerMask;
	public bool isGrounded;
	// hitbox vars
	private GameObject lastAttack;
	public int damage;
	// sword
	public GameObject SW2A;
	public GameObject SW4A;
	public GameObject SW5A;
	public GameObject SW5AA;
	public GameObject SW5A2A;
	public GameObject SW5AAA;
	public GameObject SW5AA8A;
	public GameObject SW6A;
	public GameObject SW6AA;
	public GameObject SW8A;
	// end hitbox vars

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		isGrounded = Physics.CheckSphere(playerTransform.position, 1.5f, layerMask);

		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
		if (direction.magnitude >= 0.1f && canMove) {
			if (isGrounded) {
				rb.AddForce(direction * (speed * 2) * Time.deltaTime);
			} else rb.AddForce(direction * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJCancel) {
			rb.AddForce(Vector3.up * jumpForce);
			canMove = true;
			canCancel = true;
		}
		if (canCancel || lastAttack == SW8A || lastAttack == SW2A) rb.AddForce(Vector3.down * gravity * Time.deltaTime);

		if (Input.GetKey(KeyCode.LeftArrow)) {
			playerTransform.Rotate(0f, -0.5f, 0f);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			playerTransform.Rotate(0f, 0.5f, 0f);
        }

		// attack
		if (Input.GetKeyDown(KeyCode.K)) {
			if (Input.GetKey(KeyCode.W)) {
				if (canCancel) {
					Debug.Log("YRAGH!!!");
					StopAllCoroutines();
					StartCoroutine("FindAttack", SW8A);
				}
			} else if (Input.GetKey(KeyCode.S)) {
				if (canCancel) {
					Debug.Log("CONTRUCTION ATTACK!");
					StopAllCoroutines();
					StartCoroutine("FindAttack", SW2A);
				}
			} else if (Input.GetKey(KeyCode.D)) {
				if (canCancel) {
					Debug.Log("EAT THIS!!!");
					StopAllCoroutines();
					StartCoroutine("FindAttack", SW6A);
				}
			} else if (Input.GetKey(KeyCode.A)) {
				if (canCancel) {
					Debug.Log("I'll PROVE that this is a circle!");
					StopAllCoroutines();
					StartCoroutine("FindAttack", SW4A);
				}
			} else if (canCancel) {
				Debug.Log("TAKE THAT!!!");
				StopAllCoroutines();
				StartCoroutine("FindAttack", SW5A);
			}
		}
	}

	// Input: name of attack to use
	// finds an attack and calls function to perform it
	IEnumerator FindAttack(GameObject attack) {
		damage = 0;


		if (attack == SW5A) {
			damage = 20;
			if (!canMove) {
				if (lastAttack == SW5A) {
					damage = 20;
					attack = SW5AA;
				} else if (lastAttack == SW5AA) {
					damage = 40;
					attack = SW5AAA;
					StartCoroutine(DoAttack(attack, 0.1f, 0.2f, 0.5f, -1f));
					yield break;
				}
			}
		}
		if (attack == SW2A) {
			if (!canMove) {
				if (lastAttack == SW5A) {
					damage = 20;
					attack = SW5A2A;
					StartCoroutine(DoAttack(attack, 0.2f, 0.2f, 0.5f, -1f));
					yield break;
				}
			}
			damage = 50;
			attack = SW2A;
			rb.AddForce(Vector3.up * jumpForce * 1.5f);
			lastAttack = attack;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			canCancel = false;
			canMove = false;
			yield return new WaitForSecondsRealtime(0.1f);
			attack.SetActive(true);
			yield return new WaitForSecondsRealtime(0.35f);
			attack.SetActive(false);
			yield return new WaitForSecondsRealtime(0.5f);
			while (true) {
				// prevents a freeze, don't change this
				if (!isGrounded) yield return new WaitForSecondsRealtime(0.2f); else break;
			}
			canMove = true;
			canJCancel = true;
			canCancel = true;
			yield break;
		}
		if (attack == SW6A) {
			damage = 30;
			if (!canMove) {
				if (lastAttack == SW6A) {
					damage = 60;
					attack = SW6AA;
				}
			}
		}
		if (attack == SW8A) {
			if (!canMove && lastAttack == SW5AA) {
				damage = 70;
				attack = SW5AA8A;
			} else {
				// this will be a special case that will call some unique code and end the function
				lastAttack = attack;
				damage = 80;
				canCancel = false;
				canJCancel = false;
				rb.AddForce(Vector3.up * jumpForce);
				rb.AddForce(Vector3.forward * jumpForce);
				yield return new WaitForSecondsRealtime(0.2f);
				while (true) {
					// prevents a freeze, don't change this
					if (!isGrounded) yield return new WaitForSecondsRealtime(0.2f); else break;
				}

				StartCoroutine(DoAttack(attack, 0.0f, 0.2f, 0.5f, -1));
				rb.Sleep();
				yield break;
			}
		}
		if (attack == SW4A) {
			damage = 50;
			StartCoroutine(DoAttack(attack, 0.1f, 0.5f, 0.5f, -1f));
			yield break;
		}

		StartCoroutine(DoAttack(attack, 0.1f, 0.2f, 0.5f, 0.1f));
	}
	// attack script, used by above method
	// 
	IEnumerator DoAttack(GameObject attack, float startup, float active, float recovery, float cancelw) {
		lastAttack = attack;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		canCancel = false;
		canMove = false;
		yield return new WaitForSecondsRealtime(startup);
		attack.SetActive(true);
		yield return new WaitForSecondsRealtime(active);
		attack.SetActive(false);
		if (cancelw != -1f) {
			yield return new WaitForSecondsRealtime(cancelw);
			canCancel = true;
			yield return new WaitForSecondsRealtime(recovery - cancelw);
			canMove = true;
		} else {
			yield return new WaitForSecondsRealtime(recovery);
			canMove = true;
			canCancel = true;
		}
		canJCancel = true;
	}
}