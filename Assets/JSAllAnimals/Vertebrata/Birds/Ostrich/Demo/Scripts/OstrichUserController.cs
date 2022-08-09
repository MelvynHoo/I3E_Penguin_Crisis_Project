using UnityEngine;
using System.Collections;

public class OstrichUserController : MonoBehaviour {
	OstrichCharacter ostrichCharacter;
	
	void Start () {
		ostrichCharacter = GetComponent < OstrichCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			ostrichCharacter.Attack();
		}
		
		if (Input.GetButtonDown ("Jump")) {
			ostrichCharacter.Jump();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			ostrichCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			ostrichCharacter.Eat();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			ostrichCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			ostrichCharacter.Rebirth();
		}		
		if (Input.GetKeyDown (KeyCode.U)) {
			ostrichCharacter.StandUp();
		}
		
		if (Input.GetKeyDown (KeyCode.N)) {
			ostrichCharacter.SitDown();
		}				
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if (Input.GetKey(KeyCode.LeftShift)) v *= 0.5f;
		if (Input.GetKey(KeyCode.R)) v *= 2f;

		ostrichCharacter.Move (v,h);
	}
}
