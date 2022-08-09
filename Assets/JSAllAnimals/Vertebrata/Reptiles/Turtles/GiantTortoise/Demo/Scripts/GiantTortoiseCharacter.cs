using UnityEngine;
using System.Collections;

public class GiantTortoiseCharacter : MonoBehaviour {
	Animator giantTortoiseAnimator;
	Rigidbody giantTortoiseRigid;
	public bool isSwimming=false;
	public bool isLived=true;
	public float swimSpeed=.5f;

	void Start () {
		giantTortoiseAnimator = GetComponent<Animator> ();
		giantTortoiseRigid = GetComponent<Rigidbody> ();
	}
	
	public void Attack(){
		giantTortoiseAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		giantTortoiseAnimator.SetTrigger("Hit");
	}
	
	public void EatStart(){
		giantTortoiseAnimator.SetBool("IsEating",true);
	}

	public void EatEnd(){
		giantTortoiseAnimator.SetBool("IsEating",false);
	}

	public void DrinkStart(){
		giantTortoiseAnimator.SetBool("IsDrinking",true);
	}
	
	public void DrinkEnd(){
		giantTortoiseAnimator.SetBool("IsDrinking",false);
	}

	public void DefenseStart(){
		giantTortoiseAnimator.SetBool("Defense",true);
	}
	
	public void DefenseEnd(){
		giantTortoiseAnimator.SetBool("Defense",false);
	}
	
	public void Death1(){
		giantTortoiseAnimator.SetTrigger("Death1");
		giantTortoiseAnimator.SetBool("IsLived",false);
		isLived = false;
	}
	
	public void Death2(){
		giantTortoiseAnimator.SetTrigger("Death2");
		giantTortoiseAnimator.SetBool("IsLived",false);
		isLived = false;
	}

	
	public void Rebirth(){
		giantTortoiseAnimator.SetBool("IsLived",true);
		isLived = true;
	}
	
	public void Move(float v,float h){
		giantTortoiseAnimator.SetFloat ("Forward", v);
		giantTortoiseAnimator.SetFloat ("Turn", h);


		if (isSwimming && isLived){
			transform.RotateAround(transform.position,transform.up,Time.deltaTime*h*50f);
			giantTortoiseRigid.velocity=transform.forward*v*swimSpeed;	
		}


	}

	public void SwimStart(){
		giantTortoiseAnimator.SetBool("IsSwimming",true);
		isSwimming = true;
		giantTortoiseRigid.useGravity = false;
		giantTortoiseRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		giantTortoiseAnimator.applyRootMotion = false;

	}
	
	public void SwimEnd(){
		giantTortoiseAnimator.SetBool("IsSwimming",false);
		isSwimming = false;
		giantTortoiseRigid.useGravity = true;
		giantTortoiseAnimator.applyRootMotion = true;
		giantTortoiseRigid.constraints = RigidbodyConstraints.FreezeRotation;
	}
}
