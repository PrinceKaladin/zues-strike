using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnscripts : MonoBehaviour
{
    public GameObject[] spawns;
    public Vector2 spawnPosition;
    public int delay;
    public float fromx;
    public float tox;
    
    private void Start()
    {
        StartCoroutine(CreateObject());
    }
    IEnumerator CreateObject() {

        while (true) {

            Instantiate(spawns[Random.Range(0, spawns.Length)],new Vector2(Random.Range(fromx,tox),spawnPosition.y), Quaternion.identity);
            yield return new WaitForSeconds(delay);
            
            

        
        }
            
    }

}
