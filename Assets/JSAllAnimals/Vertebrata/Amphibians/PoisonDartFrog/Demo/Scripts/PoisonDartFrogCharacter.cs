using UnityEngine;
using System.Collections;

public class PoisonDartFrogCharacter : MonoBehaviour {
	Animator poisonDartFrogAnimator;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset=0.01f;
	public bool IsGrounded;
	Rigidbody poisonDartFrogRigid;
	
	void Start () {
		poisonDartFrogAnimator = GetComponent<Animator> ();
		poisonDartFrogRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
	}
	
	public void Attack(){
		poisonDartFrogAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		poisonDartFrogAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		poisonDartFrogAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		poisonDartFrogAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		poisonDartFrogAnimator.SetTrigger("Rebirth");
	}
	
	
	public void HopForward(){
		poisonDartFrogAnimator.SetTrigger("HopForward");
	}
	
	
	public void HopRight(){
		poisonDartFrogAnimator.SetTrigger("HopRight");
	}
	
	public void HopLeft(){
		poisonDartFrogAnimator.SetTrigger("HopLeft");
	}	
	
	public void Jump(){
		if (IsGrounded) {
			poisonDartFrogAnimator.SetTrigger ("Jump");
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		IsGrounded=Physics.Raycast(transform.position + (Vector3.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

	}
	
	public void Move(float v,float h){
		poisonDartFrogAnimator.SetFloat ("Forward", v);
		poisonDartFrogAnimator.SetFloat ("Turn", h);
	}
}
