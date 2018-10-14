using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("MENU");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
