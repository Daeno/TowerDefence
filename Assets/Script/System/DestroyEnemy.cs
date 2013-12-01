using UnityEngine;
using System.Collections;

public class DestroyEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coli){
		if (coli.tag == "Enemy") {
			DestroyObject(coli.gameObject);
			GameStatics.gameScore -= 100;
			GameStatics.lives -= 1;
		}
	}
}
