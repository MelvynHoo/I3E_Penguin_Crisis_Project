using UnityEngine;
using System.Collections;

public class CassowaryCharacter : MonoBehaviour {
	Animator cassowaryAnimator;
	public bool jumpUp=false;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset=0.01f;
	public GameObject leftFoot;
	public bool leftFootIsGrounded;
	public GameObject rightFoot;
	public bool rightFootIsGrounded;
	public bool IsGrounded;

	public float jumpSpeed=1f;
	Rigidbody cassowaryRigid;
	
	void Start () {
		cassowaryAnimator = GetComponent<Animator> ();
		cassowaryRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
	}
	
	public void Attack(){
		cassowaryAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		cassowaryAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		cassowaryAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		cassowaryAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		cassowaryAnimator.SetTrigger("Rebirth");
	}
	
	
	public void SitDown(){
		cassowaryAnimator.SetTrigger("SitDown");
	}
	

	public void StandUp(){
		cassowaryAnimator.SetTrigger("StandUp");
	}
	

	
	public void Jump(){
		if (IsGrounded) {
			cassowaryAnimator.SetTrigger ("Jump");
			jumpUp = true;
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		leftFootIsGrounded=Physics.Raycast(leftFoot.transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		rightFootIsGrounded=Physics.Raycast(rightFoot.transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		cassowaryAnimator.SetBool("LeftFootIsGrounded",leftFootIsGrounded);		
		cassowaryAnimator.SetBool("RightFootIsGrounded",rightFootIsGrounded);	
		IsGrounded=leftFootIsGrounded||rightFootIsGrounded;

		if(IsGrounded){
			if(!jumpUp){
				cassowaryAnimator.applyRootMotion = true;
				cassowaryAnimator.SetBool("IsGrounded",true);
			}
		}
		else
		{
			cassowaryAnimator.applyRootMotion = false;
			cassowaryAnimator.SetBool("IsGrounded",false);
		}

		if (jumpUp) {
			if(!IsGrounded){
				jumpUp=false;
				cassowaryAnimator.applyRootMotion = false;
				cassowaryRigid.AddForce((transform.up+transform.forward*cassowaryRigid.velocity.sqrMagnitude*.1f)*jumpSpeed,ForceMode.Impulse);
			}
		}
	}
	
	public void Move(float v,float h){
		cassowaryAnimator.SetFloat ("Forward", v);
		cassowaryAnimator.SetFloat ("Turn", h);
	}
}
