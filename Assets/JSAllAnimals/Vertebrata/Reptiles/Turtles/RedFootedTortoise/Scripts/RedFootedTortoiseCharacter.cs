using UnityEngine;
using System.Collections;

public class RedFootedTortoiseCharacter : MonoBehaviour {
	Animator redFootedTortoiseAnimator;
	
	void Start () {
		redFootedTortoiseAnimator = GetComponent<Animator> ();
	}
	
	public void Attack(){
		redFootedTortoiseAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		redFootedTortoiseAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		redFootedTortoiseAnimator.SetTrigger("Eat");
	}
	
	public void SitDown(){
		redFootedTortoiseAnimator.SetBool("SitDown",true);
	}
	
	public void StandUp(){
		redFootedTortoiseAnimator.SetBool("SitDown",false);
	}
	
	public void Death1(){
		redFootedTortoiseAnimator.SetTrigger("Death1");
	}
	
	public void Death2(){
		redFootedTortoiseAnimator.SetTrigger("Death2");
	}
	
	public void Death3(){
		redFootedTortoiseAnimator.SetTrigger("Death3");
	}
	
	public void Rebirth(){
		redFootedTortoiseAnimator.SetTrigger("Rebirth");
	}
	
	public void Move(float v,float h){
		redFootedTortoiseAnimator.SetFloat ("Forward", v);
		redFootedTortoiseAnimator.SetFloat ("Turn", h);
	}
}
