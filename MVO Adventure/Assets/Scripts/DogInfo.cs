using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DogInfo : MonoBehaviour {
    private Text info;
    DogManager dm = new DogManager();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(dm.dogStay == true)
        {
            info.text = "Dog not follow";
        }
        else
        {
            info.text = "Dog follow";
        }
	}

    
}
