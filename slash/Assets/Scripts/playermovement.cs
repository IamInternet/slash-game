using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Input;

public class playermovement : MonoBehaviour
{
	public float gravity;
	// movement vars
	public float speed;
	Rigidbody rb;
	// jumping vars
	public float jumpForce;
	public Transform groundCheck;
	public LayerMask layerMask;
	public bool isGrounded;

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
		isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, layerMask);

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
    }
}
