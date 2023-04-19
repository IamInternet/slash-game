using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordghost : MonoBehaviour
{
    public playermovement player;
	Rigidbody rb;
	Transform et;
	public Transform pt;
	public LayerMask layerMask;
	public bool isGrounded;
    private float stun;
    private GameObject eattack;
    private int cd;
    public int damage;
    public int distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        et = GetComponent<Transform>();
        eattack = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(et.position, 2.2f, layerMask);
        // Debug.Log(stun);
        Vector3 playerRight = pt.right;
		Vector3 playerForward = Vector3.Cross(playerRight, Vector3.up);
        Vector3 direction = (playerRight) + (playerForward);
        et.LookAt(pt);
        et.eulerAngles = new Vector3(
            et.eulerAngles.x * 0,
            et.eulerAngles.y,
            et.eulerAngles.z * 0
        );
        Vector3 right = et.right;
		Vector3 forward = Vector3.Cross(right, Vector3.up);
        if (stun <= 0) {
            if (cd < 0) {
                if (Vector3.Distance(et.position, pt.position) < distance) {
                    rb.velocity = Vector3.zero;
		            rb.angularVelocity = Vector3.zero;
                    cd = 100000;
                    damage = 100;
                    StartCoroutine(DoAttack(eattack, 0.5f, 0.1f, 60));
                } else {
                    rb.AddForce(forward * 8000 * Time.deltaTime);
                }
            }
        }
        if (isGrounded){
            stun--;
            cd--;
        }
    }

    void OnTriggerEnter (Collider cInfo) {
		if (cInfo.GetComponent<Collider>().tag == "Hitbox") {
			stun = 60f;
        }
    }

    IEnumerator DoAttack(GameObject attack, float startup, float active, int recovery) {
		yield return new WaitForSecondsRealtime(startup);
        if (stun <= 0) {
		    attack.SetActive(true);
        }
		yield return new WaitForSecondsRealtime(active);
		attack.SetActive(false);
		cd = recovery;
	}
}
