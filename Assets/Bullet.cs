using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        //GameObject.Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        //GameObject.Destroy(this);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
