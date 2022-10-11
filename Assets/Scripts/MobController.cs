using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.angularVelocity = new Vector3(Random.Range(0, 1.5f), Random.Range(0, 1.5f), Random.Range(0, 1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
