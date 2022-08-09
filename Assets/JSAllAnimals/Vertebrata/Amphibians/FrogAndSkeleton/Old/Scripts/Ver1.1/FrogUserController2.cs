using UnityEngine;
using System.Collections;

public class FrogUserController2 : MonoBehaviour {
	FrogCharacter2 frogCharacter;

	void Start () {
		frogCharacter = GetComponent < FrogCharacter2> ();
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
