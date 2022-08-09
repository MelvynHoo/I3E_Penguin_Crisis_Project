using UnityEngine;
using System.Collections;

public class BoxTurtleCharacter : MonoBehaviour {
	Animator boxTurtleAnimator;
	
	void Start () {
		boxTurtleAnimator = GetComponent<Animator> ();
	}
	
	public void Attack(){
		boxTurtleAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		boxTurtleAnimator.SetTrigger("Hit");
	}

	public void Eat(){
		boxTurtleAnimator.SetTrigger("Eat");
	}

	public void Open(){
		boxTurtleAnimator.SetBool("Boxed",false);
	}
	
	public void Close(){
		boxTurtleAnimator.SetBool("Boxed",true);
	}

	public void Death(){
		boxTurtleAnimator.SetTrigger("Death");
	}

	public void Rebirth(){
		boxTurtleAnimator.SetTrigger("Rebirth");
	}


	public void CrouchStart(){
		boxTurtleAnimator.SetBool("Crouching",true);
	}
	
	public void CrouchEnd(){
		boxTurtleAnimator.SetBool("Crouching",false);
	}
	
	public void Move(float v,float h){
		boxTurtleAnimator.SetFloat ("Forward", v);
		boxTurtleAnimator.SetFloat ("Turn", h);
	}
}
