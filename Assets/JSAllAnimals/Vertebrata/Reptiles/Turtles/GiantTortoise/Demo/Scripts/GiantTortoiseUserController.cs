using UnityEngine;
using System.Collections;

public class GiantTortoiseUserController : MonoBehaviour {
	GiantTortoiseCharacter giantTortoiseCharacter;
	
	void Start () {
		giantTortoiseCharacter = GetComponent < GiantTortoiseCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			giantTortoiseCharacter.Attack();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			giantTortoiseCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			giantTortoiseCharacter.EatStart();
		}

		if (Input.GetKeyUp (KeyCode.E)) {
			giantTortoiseCharacter.EatEnd();
		}

		if (Input.GetKeyDown (KeyCode.I)) {
			giantTortoiseCharacter.DrinkStart();
		}
		
		if (Input.GetKeyUp (KeyCode.I)) {
			giantTortoiseCharacter.DrinkEnd();
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			giantTortoiseCharacter.DefenseStart();
		}
		
		if (Input.GetKeyUp (KeyCode.Z)) {
			giantTortoiseCharacter.DefenseEnd();
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			giantTortoiseCharacter.SwimStart();
		}

		if (Input.GetKeyDown (KeyCode.N)) {
			giantTortoiseCharacter.SwimEnd();
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			giantTortoiseCharacter.Death1();
		}
		
		if (Input.GetKeyDown (KeyCode.L)) {
			giantTortoiseCharacter.Death2();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			giantTortoiseCharacter.Rebirth();
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		giantTortoiseCharacter.Move (v,h);
	}
}
