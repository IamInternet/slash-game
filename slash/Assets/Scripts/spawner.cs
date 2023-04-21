using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public int maxcd;
    int cd;
    public GameObject prefab;
    Transform et;
    enemy escript;
    // information for spawned enemy
    public playermovement player;
    public Transform pt;
    public LayerMask layerMask;
    public GameObject eattack;
    public int distance;
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        cd = maxcd;
        et = GetComponent<Transform>();
        escript = GetComponent<enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cd < 1){
            GameObject Enemy = Instantiate(prefab,new Vector3(et.position.x, et.position.y, et.position.z + 5), Quaternion.identity);
            // set swordghost.cs vars
            Enemy.GetComponent<swordghost>().player = this.player;
            Enemy.GetComponent<swordghost>().pt = this.pt;
            Enemy.GetComponent<swordghost>().layerMask = this.layerMask;
            Enemy.GetComponent<swordghost>().pt = this.pt;
            Enemy.GetComponent<swordghost>().distance = this.distance;
            // set enemy.cs vars
            Enemy.GetComponent<enemy>().hp = this.hp;
            Enemy.GetComponent<enemy>().player = escript.player;
            Enemy.GetComponent<enemy>().pt = escript.pt;
            Enemy.GetComponent<enemy>().layerMask = escript.layerMask;
            Enemy.GetComponent<enemy>().hpDisplay = escript.hpDisplay;
            cd = maxcd;
        }
        // Debug.Log(cd);
        cd--;
    }
}
