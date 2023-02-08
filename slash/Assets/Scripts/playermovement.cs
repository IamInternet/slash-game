using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Input;

public class playermovement : MonoBehaviour
{
	public float speed;
	Rigidbody rb;

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

		Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
		if (direction.magnitude >= 0.1f) {
			rb.AddForce(direction * speed * Time.deltaTime);
		}
    }
}
