using UnityEngine;
using System.Collections;

public class ShrimpCharacterScript : MonoBehaviour {
	public Animator ShrimpAnimator;
	public float ShrimpSpeed=1f;
	Rigidbody ShrimpRigid;
	public bool isSwimming=false;
	public float forwardSpeed=0f;
	public float turnSpeed=0f;
	public float upDownSpeed=0f;
	public float maxTurnSpeed=.001f;
	public float maxForwardSpeed=.1f;
	public float backSpeed=-.2f;
	public bool isLiving=true;

	void Start () {
		ShrimpAnimator = GetComponent<Animator> ();
		ShrimpAnimator.speed = ShrimpSpeed;
		ShrimpRigid = GetComponent<Rigidbody> ();
		if (isSwimming) {
			ShrimpAnimator.SetTrigger ("Swim");
			ShrimpAnimator.applyRootMotion = false;
			isSwimming = true;
		}
	}	

	void FixedUpdate(){
		Move ();
	}

	public void Landing(){
		if (isSwimming) {
			ShrimpAnimator.SetTrigger ("Landing");
			ShrimpAnimator.applyRootMotion = true;
			isSwimming = false;
			ShrimpRigid.useGravity=true;
		}
	}

	public void Hit(){
		ShrimpAnimator.SetTrigger ("Hit");
	}

	public void Attack(){
		ShrimpAnimator.SetTrigger ("Attack");
	}

	public void SwimBack(){
		ShrimpAnimator.SetTrigger ("SwimBack");
		ShrimpRigid.AddForce (transform.forward*backSpeed,ForceMode.Impulse);
	}
	
	public void Die(){
		ShrimpAnimator.SetTrigger ("Die");
		ShrimpAnimator.applyRootMotion = true;
		isSwimming = false;
		ShrimpRigid.useGravity=true;
		isLiving = false;
	}

	public void Rebirth(){
		ShrimpAnimator.SetTrigger ("Rebirth");
		isLiving = true;
	}

	public void Swim(){
		if (!isSwimming && isLiving) {
			ShrimpAnimator.SetTrigger ("Swim");
			ShrimpAnimator.applyRootMotion = false;
			forwardSpeed=maxForwardSpeed;
			isSwimming = true;
			ShrimpRigid.useGravity=false;
		}
	}
	
	public void Move(){
		ShrimpAnimator.SetFloat ("Forward",forwardSpeed);
		ShrimpAnimator.SetFloat ("Turn",turnSpeed);
		ShrimpAnimator.SetFloat ("UpDown",upDownSpeed);

		if(isSwimming) {
			ShrimpRigid.AddTorque (transform.up*maxTurnSpeed*turnSpeed);
			ShrimpRigid.AddForce (transform.up*upDownSpeed*.1f);
			ShrimpRigid.AddForce (transform.forward*forwardSpeed);
		}
	}
}
