using UnityEngine;
using System.Collections;

public class PoisonDartFrogUserController : MonoBehaviour {
	PoisonDartFrogCharacter poisonDartFrogCharacter;
	
	void Start () {
		poisonDartFrogCharacter = GetComponent < PoisonDartFrogCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			poisonDartFrogCharacter.Attack();
		}
		
		if (Input.GetButtonDown ("Jump")) {
			poisonDartFrogCharacter.Jump();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			poisonDartFrogCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			poisonDartFrogCharacter.Eat();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			poisonDartFrogCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			poisonDartFrogCharacter.Rebirth();
		}	

		if (Input.GetKeyDown (KeyCode.W)) {
			poisonDartFrogCharacter.HopForward();
		}
		
		if (Input.GetKeyDown (KeyCode.A)) {
			poisonDartFrogCharacter.HopLeft();
		}		

		if (Input.GetKeyDown (KeyCode.D)) {
			poisonDartFrogCharacter.HopRight();
		}			

	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		poisonDartFrogCharacter.Move (v,h);
	}
}
