using UnityEngine;
using System.Collections;

public class SimpleFrogUserController : MonoBehaviour {
	SimpleFrogCharacter frogCharacter;

	void Start () {
		frogCharacter = GetComponent < SimpleFrogCharacter> ();
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
