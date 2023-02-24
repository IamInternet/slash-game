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
	bool canAttack = true;
	Rigidbody rb;
	// jumping vars
	public float jumpForce;
	public Transform playerTransform;
	public LayerMask layerMask;
	public bool isGrounded;
	// hitbox vars
	public GameObject test;
	public int damage;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		isGrounded = Physics.CheckSphere(playerTransform.position, 0.6f, layerMask);

		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
		if (direction.magnitude >= 0.1f) {
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
			if (canAttack){
				Debug.Log("TAKE THIS!!!");
				StartCoroutine("Attack", test);
			}
		}
    }

	/* Input: name of attack to use
	spawns hitbox of requested attack, and if it's a special attack that needs multiple hits it will manage that as well */
	public IEnumerator Attack(GameObject attack){
		damage = 0;
		

		if (attack == test){
			damage = 10;
		}
		if (attack != test){
			// idk
		} else{
			// basic attack script
			canAttack = false;
			attack.SetActive(true);
			yield return new WaitForSecondsRealtime(1);
			attack.SetActive(false);
			canAttack = true;
		}
	}
}
