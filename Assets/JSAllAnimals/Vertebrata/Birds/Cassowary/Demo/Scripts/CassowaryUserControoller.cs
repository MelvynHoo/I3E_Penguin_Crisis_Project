using UnityEngine;
using System.Collections;

public class CassowaryUserControoller : MonoBehaviour {
	CassowaryCharacter cassowaryCharacter;
	
	void Start () {
		cassowaryCharacter = GetComponent < CassowaryCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			cassowaryCharacter.Attack();
		}

		if (Input.GetButtonDown ("Jump")) {
			cassowaryCharacter.Jump();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			cassowaryCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			cassowaryCharacter.Eat();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			cassowaryCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			cassowaryCharacter.Rebirth();
		}		
		if (Input.GetKeyDown (KeyCode.U)) {
			cassowaryCharacter.StandUp();
		}
		
		if (Input.GetKeyDown (KeyCode.N)) {
			cassowaryCharacter.SitDown();
		}				
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if (Input.GetKey(KeyCode.LeftShift)) v *= 0.5f;
		cassowaryCharacter.Move (v,h);
	}
}
