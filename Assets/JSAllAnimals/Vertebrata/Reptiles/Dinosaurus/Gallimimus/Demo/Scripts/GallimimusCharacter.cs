using UnityEngine;
using System.Collections;

public class GallimimusCharacter : MonoBehaviour {
	public Animator gallimimusAnimator;
	public bool isGrounded=false;
	public bool jumpUp=false;
	public float jumpSpeed=5f;
	public float groundCheckOffset=0.3f;
	public float groundCheckDistance=1f;
	public float runCycleLegOffset=.2f;

	void Start () {
		gallimimusAnimator = GetComponent<Animator> ();
	}
	
	void Update () {
		GroundedCheck ();			
	}
	
	public void Jump(){
		if (isGrounded & gallimimusAnimator.GetFloat ("Forward")>.1f) {
			jumpUp=true;
			isGrounded = false;
			gallimimusAnimator.applyRootMotion = false;
			gallimimusAnimator.SetBool ("Landing",false);

			float runCycle =
				Mathf.Repeat(
					gallimimusAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + runCycleLegOffset, 1);
			if (runCycle < .5){ 
				gallimimusAnimator.SetTrigger ("JumpLeftFoot");
			}else{
				gallimimusAnimator.SetTrigger ("JumpRightFoot");
			}

			GetComponent<Rigidbody> ().AddForce ((transform.up - gallimimusAnimator.GetFloat ("Forward") * transform.right) * jumpSpeed, ForceMode.Impulse);
		}
	}

	public void Attack(){
		gallimimusAnimator.SetTrigger ("Attack");
	}

	public void TailAttack(){
		gallimimusAnimator.SetTrigger ("TailAttack");
	}

	public void SitDown(){
		gallimimusAnimator.SetTrigger ("SitDown");
	}
	
	public void StandUp(){
		gallimimusAnimator.SetTrigger ("StandUp");
	}

	void GroundedCheck(){
		if (jumpUp) {
			if(gallimimusAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "Fly"){
				jumpUp=false;
			}
		}

		if (!jumpUp & Physics.Raycast (transform.position + Vector3.up * groundCheckOffset, Vector3.down, groundCheckDistance)) {
			if (!isGrounded) {
				gallimimusAnimator.SetBool ("Landing", true);
				gallimimusAnimator.applyRootMotion = true;
				isGrounded = true;
			}
		} 
	}

	public void Move(float v,float h){
		gallimimusAnimator.SetFloat ("Forward", v);
		gallimimusAnimator.SetFloat ("Turn", h);
	}

}
