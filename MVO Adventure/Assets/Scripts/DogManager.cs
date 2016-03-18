using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DogManager : MonoBehaviour {

    private Vector3 player;
    private Vector2 playerdirection;
    private Rigidbody2D rb2D;
    private float x;
    private float y;
    public float speed;
    public bool dogStay = false;
    private Text dogInfo;
    

    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update() {
        dogInfo = GameObject.Find("DogInfo").GetComponent<Text>();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DogStay();
            dogInfo.text = "Dog Not Follows (Press Q)";
        }
        if (dogStay != true)
        {
            player = GameObject.Find("Player").transform.position;

            x = player.x - transform.position.x + 0.4f;
            y = player.y - transform.position.y;

            playerdirection = new Vector2(x, y);
            GetComponent<Rigidbody2D>().AddForce(playerdirection.normalized * speed);
            dogInfo.text = "Dog Follows (Press Q)";


        }
    }

    void DogStay()
    {

        if(dogStay == true)
        {
            dogStay = false;
        }
        else
        {
            dogStay = true;
        }
    }

    //IEnumerator DogFollows()
    //{
    //    dogInfo.text = "Dog Follows";
    //    dogInfo.enabled = true;
    //    yield return new WaitForSeconds(2);
    //    dogInfo.enabled = false;
    //}

    //IEnumerator DogNotFollows()
    //{
    //    dogInfo.text = "Dog Not Follows";
    //    dogInfo.enabled = true;
    //    yield return new WaitForSeconds(2);
    //    dogInfo.enabled = false;
    //}


}
