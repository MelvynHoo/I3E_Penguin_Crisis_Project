using UnityEngine;
using System.Collections;

public class AlligatorSnappingTurtleCharacter : MonoBehaviour {
	Animator alligatorSnappingTurtleAnimator;
	
	void Start () {
		alligatorSnappingTurtleAnimator = GetComponent<Animator> ();
	}

	public void Attack(){
		alligatorSnappingTurtleAnimator.SetTrigger("Attack");
	}

	public void Attack2(){
		alligatorSnappingTurtleAnimator.SetTrigger("Attack2");
	}

	public void Hit(){
		alligatorSnappingTurtleAnimator.SetTrigger("Hit");
	}

	public void Down(){
		alligatorSnappingTurtleAnimator.SetTrigger("Down");
	}

	public void Rebirth(){
		alligatorSnappingTurtleAnimator.SetTrigger("Rebirth");
	}

	public void Eat(){
		alligatorSnappingTurtleAnimator.SetTrigger("Eat");
	}

	public void Bite(){
		alligatorSnappingTurtleAnimator.SetTrigger("Bite");
	}

	public void CrouchStart(){
		alligatorSnappingTurtleAnimator.SetBool("Crouching",true);
	}

	public void CrouchEnd(){
		alligatorSnappingTurtleAnimator.SetBool("Crouching",false);
	}
	
	public void Move(float v,float h){
		alligatorSnappingTurtleAnimator.SetFloat ("Forward", v);
		alligatorSnappingTurtleAnimator.SetFloat ("Turn", h);
	}
}
