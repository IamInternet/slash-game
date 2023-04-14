using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("ForestLevel");
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SceneManager.LoadScene("debug");
        }
    }
}
