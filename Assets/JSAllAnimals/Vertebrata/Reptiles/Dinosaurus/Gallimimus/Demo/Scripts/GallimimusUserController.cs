using UnityEngine;
using System.Collections;

public class GallimimusUserController : MonoBehaviour {
	GallimimusCharacter gallimimusCharacter;

	void Start () {
		gallimimusCharacter = GetComponent<GallimimusCharacter> ();
	}
	
	private void FixedUpdate()
	{
		if (Input.GetButtonDown ("Jump")) {
			gallimimusCharacter.Jump();
		}
		
		if (Input.GetButtonDown ("Fire1")) {
			gallimimusCharacter.Attack();
		}

		if (Input.GetKeyDown(KeyCode.T)) {
			gallimimusCharacter.TailAttack();
		}

		if (Input.GetKeyDown(KeyCode.U)) {
			gallimimusCharacter.StandUp();
		}

		if (Input.GetKeyDown(KeyCode.N)) {
			gallimimusCharacter.SitDown();
		}

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		gallimimusCharacter.Move (v,h);
	}
}
