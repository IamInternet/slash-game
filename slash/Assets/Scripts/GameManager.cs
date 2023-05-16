using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void SpawnerDestroyed()
    {
        AudioSource audio = GetComponent<AudioSource>();        
        Debug.Log("Spawner Destroyed!");
        // Play audio clip of destroyed spawner
        audio.Play();
    }
}
