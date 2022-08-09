using UnityEngine;
using System.Collections;

public class AlligatorSnappingTurtleUserController : MonoBehaviour {

	AlligatorSnappingTurtleCharacter alligatorSnappingTurtleCharacter;
	
	void Start () {
		alligatorSnappingTurtleCharacter = GetComponent < AlligatorSnappingTurtleCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			alligatorSnappingTurtleCharacter.Attack();
		}

		if (Input.GetButtonDown ("Fire2")) {
			alligatorSnappingTurtleCharacter.Attack2();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			alligatorSnappingTurtleCharacter.Hit();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			alligatorSnappingTurtleCharacter.Down();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			alligatorSnappingTurtleCharacter.Rebirth();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			alligatorSnappingTurtleCharacter.Eat();
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			alligatorSnappingTurtleCharacter.Bite();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			alligatorSnappingTurtleCharacter.CrouchStart();
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			alligatorSnappingTurtleCharacter.CrouchEnd();
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		alligatorSnappingTurtleCharacter.Move (v,h);
	}

}
