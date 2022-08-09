using UnityEngine;
using System.Collections;

public class BoxTurtleUserController : MonoBehaviour {	
	BoxTurtleCharacter boxTurtleCharacter;
	
	void Start () {
		boxTurtleCharacter = GetComponent < BoxTurtleCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			boxTurtleCharacter.Attack ();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			boxTurtleCharacter.Hit();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			boxTurtleCharacter.Eat();
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			boxTurtleCharacter.Close();
		}
		
		if (Input.GetKeyUp (KeyCode.B)) {
			boxTurtleCharacter.Open();
		}

		if (Input.GetKeyUp (KeyCode.K)) {
			boxTurtleCharacter.Death();
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			boxTurtleCharacter.Rebirth();
		}


		if (Input.GetKeyDown (KeyCode.C)) {
			boxTurtleCharacter.CrouchStart();
		}
		
		if (Input.GetKeyUp (KeyCode.C)) {
			boxTurtleCharacter.CrouchEnd();
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		boxTurtleCharacter.Move (v,h);
	}
}
