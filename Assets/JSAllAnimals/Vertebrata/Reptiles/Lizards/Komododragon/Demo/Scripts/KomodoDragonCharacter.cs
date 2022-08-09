using UnityEngine;
using System.Collections;

public class KomodoDragonCharacter : MonoBehaviour {
	Animator komodoDragonAnimator;
	
	void Start () {
		komodoDragonAnimator = GetComponent<Animator> ();
	}
	
	public void Attack(){
		komodoDragonAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		komodoDragonAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		komodoDragonAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		komodoDragonAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		komodoDragonAnimator.SetTrigger("Rebirth");
	}
	
	
	
	public void Move(float v,float h){
		komodoDragonAnimator.SetFloat ("Forward", v);
		komodoDragonAnimator.SetFloat ("Turn", h);
	}
}
