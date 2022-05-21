using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject mob;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(mob, new Vector3(Random.Range(transform.position.x-10, transform.position.x+10), 0, Random.Range(transform.position.z-10,transform.position.z+10)), Quaternion.identity);
    }
}
