using UnityEngine;
using System.Collections;

public class ShopEnter1 : MonoBehaviour {

    private float speed = 10f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject dog = GameObject.Find("Dog");
            GameObject Exit = GameObject.Find("InsideShop");
            other.transform.position = Exit.transform.position;
            dog.transform.position = Exit.transform.position;
        }
    }
}
