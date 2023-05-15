using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Gate : MonoBehaviour
{
    private GameObject[] array;

    void OnCollisionEnter (Collision collision)
    {
    if (collision.gameObject.CompareTag("Player"))
    {	    
        array = GameObject.FindGameObjectsWithTag("SPAWNER");
        if (array.Length == 0)
        {
            SceneManager.LoadScene("GameWin");
        } else
        {
            // Add UI Quick Tip Here: "Destroy All Spawners To Win!"
        }
    }
    }

    void Update() {
        array = GameObject.FindGameObjectsWithTag("SPAWNER");
        if (array.Length == 0)
        {
            if (this.gameObject.transform.GetChild(2) != null) {
                Destroy(this.gameObject.transform.GetChild(0).gameObject);
                Destroy(this.gameObject.transform.GetChild(1).gameObject);
            }
        }
    }
}