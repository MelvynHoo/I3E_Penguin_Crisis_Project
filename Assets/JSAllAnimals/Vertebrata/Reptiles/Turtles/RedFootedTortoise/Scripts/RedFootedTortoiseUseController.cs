using UnityEngine;
using System.Collections;

public class RedFootedTortoiseUseController : MonoBehaviour {
	RedFootedTortoiseCharacter redFootedTortoiseCharacter;
	
	void Start () {
		redFootedTortoiseCharacter = GetComponent <RedFootedTortoiseCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			redFootedTortoiseCharacter.Attack();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			redFootedTortoiseCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			redFootedTortoiseCharacter.Eat();
		}
		
		if (Input.GetKeyDown (KeyCode.Z)) {
			redFootedTortoiseCharacter.SitDown();
		}
		
		if (Input.GetKeyUp (KeyCode.Z)) {
			redFootedTortoiseCharacter.StandUp();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			redFootedTortoiseCharacter.Death1();
		}
		
		if (Input.GetKeyDown (KeyCode.L)) {
			redFootedTortoiseCharacter.Death2();
		}
		
		if (Input.GetKeyDown (KeyCode.M)) {
			redFootedTortoiseCharacter.Death3();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			redFootedTortoiseCharacter.Rebirth();
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		redFootedTortoiseCharacter.Move (v,h);
	}
}
