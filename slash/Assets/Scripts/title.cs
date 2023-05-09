using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title : MonoBehaviour
{
    public Text t;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("ForestLevel");
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            SceneManager.LoadScene("debug");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            t.text = "Use WASD to move.\nUse K to attack, and J or L to move the camera\nAttacking while holding a direction gives unique attacks\nSee movelist.txt for a full list of attacks\nDestroy all spawners then find the gate to win\nPress ENTER to begin";
        }
    }
}
