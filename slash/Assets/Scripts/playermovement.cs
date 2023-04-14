using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
	public int hp = 1000;
	public float gravity;
	public Text hpDisplay;
	// movement vars
	public float speed;
	bool canCancel = true;
	bool canJCancel = true;
	bool canMove = true;
	public Transform cam;
	Rigidbody rb;
	// jumping vars
	public float jumpForce;
	public Transform playerTransform;
	public LayerMask layerMask;
	public bool isGrounded;
	// hitbox vars
	public GameObject lastAttack;
	public int damage;
	// sword
	public GameObject SW2A;
	public GameObject SW4A;
	public GameObject SW4AA;
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
		 // Debug.Log(horizontal + ", " + vertical);
		
		Vector3 direction = Vector3.zero;
		if (horizontal != 0 || vertical != 0) {
             Vector3 right = cam.right;
             Vector3 forward = Vector3.Cross(right, Vector3.up);
             direction = (right * horizontal) + (forward * vertical);
		}
		direction = Vector3.Normalize(direction);
		Vector3.ClampMagnitude(direction, speed);
		if (direction.magnitude >= 0.1f && canMove) {
			// uncomment when camera fixed
			// playerTransform.rotation = Quaternion.LookRotation(direction);
			if (isGrounded) {
				rb.AddForce(direction * (speed * 2) * Time.deltaTime);
			} else rb.AddForce(direction * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJCancel && (lastAttack != SW4A)) {
			rb.AddForce(Vector3.up * jumpForce);
			canMove = true;
			canCancel = true;
		}
		if (canCancel || lastAttack == SW8A || lastAttack == SW2A) rb.AddForce(Vector3.down * gravity * Time.deltaTime);

		if (Input.GetKey(KeyCode.J)) {
			playerTransform.Rotate(0f, -0.5f, 0f);
		} else if (Input.GetKey(KeyCode.L)) {
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
		if (lastAttack != null) {
			lastAttack.SetActive(false);
        }
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
				rb.AddForce((Vector3.Cross(cam.right, Vector3.up)) * jumpForce * 2);
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
			damage = 10;
			canMove = false;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			yield return new WaitForSecondsRealtime(0.2f);
			for (int i = 0; i < 5; i++){
				yield return StartCoroutine(DoAttack(attack, 0.05f, 0.1f, 0.0f, -1f));
			}
			canJCancel = false;
			damage = 50;
			StartCoroutine(DoAttack(SW4AA, 0.2f, 0.3f, 0.5f, -1f));
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

	// die
	void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Enemy Hitbox") {
			Debug.Log("GAH!");
			StopAllCoroutines();
			StartCoroutine("Stun", 0.5f);
			hp -= cInfo.transform.parent.GetComponent<swordghost>().damage; // move damage to enemy script to make this work with multiple enemies
			if (hp <= 0){
				SceneManager.LoadScene("GameOver");
			}
			hpDisplay.text = "HP: " + hp;
		}
	}

	IEnumerator Stun(float time) {
		if (lastAttack != null) {
			lastAttack.SetActive(false);
        }
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		canCancel = false;
		canMove = false;
		yield return new WaitForSecondsRealtime(time);
		canMove = true;
		canCancel = true;
		canJCancel = true;
	}
}