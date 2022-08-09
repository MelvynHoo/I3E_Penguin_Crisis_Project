using UnityEngine;
using System.Collections;

public class ShrimpUserControllerScript : MonoBehaviour {
	public ShrimpCharacterScript ShrimpCharacter;
	
	void Start () {
		ShrimpCharacter = GetComponent<ShrimpCharacterScript> ();	
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.L)) {
			ShrimpCharacter.Landing();
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			ShrimpCharacter.Hit();
		}
		if (Input.GetKeyDown(KeyCode.B)) {
			ShrimpCharacter.SwimBack();
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			ShrimpCharacter.Die();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			ShrimpCharacter.Rebirth();
		}
		if (Input.GetButtonDown ("Fire1")) {
			ShrimpCharacter.Attack();
		}
		if (Input.GetButtonDown ("Jump")) {
			ShrimpCharacter.Swim();
		}
	}
	
	void FixedUpdate(){
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		if (ShrimpCharacter.isSwimming) {	
			ShrimpCharacter.turnSpeed = h;
			ShrimpCharacter.upDownSpeed=v;
		}else{
			ShrimpCharacter.forwardSpeed = v;
			ShrimpCharacter.turnSpeed = h;
		}
	}
}
