using UnityEngine;
using System.Collections;

public class FrogUserController : MonoBehaviour {

	FrogCharacter frogCharacter;
	
	void Start () {
		frogCharacter = GetComponent<FrogCharacter> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			frogCharacter.Jump();
		}
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		frogCharacter.Move (v,h);
	}
}
