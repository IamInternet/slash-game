using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Gate : MonoBehaviour
{
  public GameObject[] array;

  void OnCollisionEnter (Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {	    
      array = GameObject.FindGameObjectsWithTag("SPAWNER");
      if (array.Length == 0)
      {
        SceneManager.LoadScene("GameOver");
      } else
      {
        // Add UI Quick Tip Here: "Destroy All Spawners To Win!"
      }
    }
  }
}