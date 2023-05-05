using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : MonoBehaviour
{
 void OnCollisionEnter (Collision collision)
  {

	if (collision.gameObject.CompareTag("Player"))
	{
	    SceneManager.LoadScene("GameOver");
	}
  }
}

	
