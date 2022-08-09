using UnityEngine;
using System.Collections;

public class FrogUserController3 : MonoBehaviour {
	FrogCharacter3 frogCharacter;
	
	void Start () {
		frogCharacter = GetComponent < FrogCharacter3> ();
	}
	
	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			frogCharacter.Jump();
		}
		
		if (Input.GetButtonDown ("Fire1")) {
			frogCharacter.Eat();
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		frogCharacter.Move (v,h);
	}
}
