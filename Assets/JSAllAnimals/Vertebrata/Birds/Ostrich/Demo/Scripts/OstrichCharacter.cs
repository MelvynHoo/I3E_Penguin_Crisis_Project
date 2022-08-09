using UnityEngine;
using System.Collections;

public class OstrichCharacter : MonoBehaviour {
	Animator ostrichAnimator;
	public bool jumpUp=false;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset=0.01f;
	public GameObject leftFoot;
	public bool leftFootIsGrounded;
	public GameObject rightFoot;
	public bool rightFootIsGrounded;
	public bool IsGrounded;
	
	public float jumpSpeed=1f;
	Rigidbody ostrichRigid;
	
	void Start () {
		ostrichAnimator = GetComponent<Animator> ();
		ostrichRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
	}
	
	public void Attack(){
		ostrichAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		ostrichAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		ostrichAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		ostrichAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		ostrichAnimator.SetTrigger("Rebirth");
	}
	
	
	public void SitDown(){
		ostrichAnimator.SetTrigger("SitDown");
	}
	
	
	public void StandUp(){
		ostrichAnimator.SetTrigger("StandUp");
	}
	
	
	
	public void Jump(){
		if (IsGrounded) {
			ostrichAnimator.SetTrigger ("Jump");
			jumpUp = true;
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		leftFootIsGrounded=Physics.Raycast(leftFoot.transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		rightFootIsGrounded=Physics.Raycast(rightFoot.transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		ostrichAnimator.SetBool("LeftFootIsGrounded",leftFootIsGrounded);		
		ostrichAnimator.SetBool("RightFootIsGrounded",rightFootIsGrounded);	
		IsGrounded=leftFootIsGrounded||rightFootIsGrounded;
		
		if(IsGrounded){
			if(!jumpUp){
				ostrichAnimator.applyRootMotion = true;
				ostrichAnimator.SetBool("IsGrounded",true);
			}
		}
		else
		{
			ostrichAnimator.applyRootMotion = false;
			ostrichAnimator.SetBool("IsGrounded",false);
		}
		
		if (jumpUp) {
			if(!IsGrounded){
				jumpUp=false;
				ostrichAnimator.applyRootMotion = false;
				ostrichRigid.AddForce(transform.up*jumpSpeed,ForceMode.Impulse);
			}
		}
	}
	
	public void Move(float v,float h){
		ostrichAnimator.SetFloat ("Forward", v);
		ostrichAnimator.SetFloat ("Turn", h);
	}
}
