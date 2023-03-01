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
	bool canMove = true;
	Rigidbody rb;
	// jumping vars
	public float jumpForce;
	public Transform playerTransform;
	public LayerMask layerMask;
	public bool isGrounded;
	// hitbox vars
	private GameObject lastAttack;
	public GameObject test;
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
    void Update(){
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		isGrounded = Physics.CheckSphere(playerTransform.position, 1.5f, layerMask);

		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
		if (direction.magnitude >= 0.1f && canMove) {
			if (isGrounded){
				rb.AddForce(direction * (speed * 2) * Time.deltaTime);
			} else rb.AddForce(direction * speed * Time.deltaTime);
		}
			
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
			rb.AddForce(Vector3.up * jumpForce);
		}
		rb.AddForce(Vector3.down * gravity * Time.deltaTime);

		if (Input.GetKey(KeyCode.LeftArrow)){
			playerTransform.Rotate(0f,-0.5f,0f);
		} else if (Input.GetKey(KeyCode.RightArrow)){
			playerTransform.Rotate(0f,0.5f,0f);
		}

		if (Input.GetKeyDown(KeyCode.J)){
			if (canMove){
				Debug.Log("TAKE THIS!!!");
				StopAllCoroutines();
				StartCoroutine("Attack", test);
			}
		}
		// attack
		if (Input.GetKeyDown(KeyCode.K)){
			if (Input.GetKey(KeyCode.W)){
				if (canMove || canCancel){
					Debug.Log("YRAGH!!!");
					StopAllCoroutines();
					StartCoroutine("Attack", SW8A);
				}
			}else if (Input.GetKey(KeyCode.D)){
				if (canMove || canCancel){
					Debug.Log("EAT THIS!!!");
					StopAllCoroutines();
					StartCoroutine("Attack", SW6A);
				}
			} else if (canMove || canCancel){
				Debug.Log("TAKE THAT!!!");
				StopAllCoroutines();
				StartCoroutine("Attack", SW5A);
			}
		}
    }

	// Input: name of attack to use
	// spawns hitbox of requested attack, and if it's a special attack that needs multiple hits it will manage that as well
	IEnumerator Attack(GameObject attack){
		damage = 0;
		

		if (attack == test){
			damage = 10;
		}
		if (attack == SW5A){
			damage = 10;
			if (!canMove){
				if (lastAttack == SW5A){
					damage = 20;
					attack = SW5AA;
				} else if (lastAttack == SW5AA){
					damage = 50;
					attack = SW5AAA;
				}
			}
		}
		if (attack == SW6A){
			damage = 30;
			if (!canMove){
				/*if (lastAttack == SW6A){
					damage = 60;
					attack = SW6AA;
				}*/
			}
		}
		if (attack == SW8A){
			if (!canMove && SW5AA){
					damage = 70;
					attack = SW5AA8A;
			} // else{
			// this will be a special case that will call some unique code and end the function
			damage = 50;
		}
		lastAttack = attack;

			// basic attack script
			canCancel = false;
			canMove = false;
			yield return new WaitForSecondsRealtime(0.2f);
			attack.SetActive(true);
			yield return new WaitForSecondsRealtime(0.2f);
			attack.SetActive(false);
			yield return new WaitForSecondsRealtime(0.5f);
			canCancel = true;
			yield return new WaitForSecondsRealtime(0.5f);
			canMove = true;
	}
}