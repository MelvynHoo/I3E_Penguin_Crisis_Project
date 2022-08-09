using UnityEngine;
using System.Collections;

public class FrogCharacter : MonoBehaviour {
	Animator frogAnimator;
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftHand;
	public GameObject rightHand;
	public float groundCheckDistance=.15f;
	public bool isGrounded=false;
	
	void Start () {
		frogAnimator = GetComponent<Animator> ();
	}
	
	void Update () {
			GroundedCheck ();			
	}
	
	public void Jump(){
		if (isGrounded) {
			frogAnimator.SetTrigger ("Jump");
		}
	}

	void GroundedCheck(){
		RaycastHit hitInfo;
		if (Physics.Raycast (leftFoot.transform.position, Vector3.down, out hitInfo, groundCheckDistance) || Physics.Raycast (rightFoot.transform.position, Vector3.down, out hitInfo, groundCheckDistance)|| Physics.Raycast (rightHand.transform.position, Vector3.down, out hitInfo, groundCheckDistance)|| Physics.Raycast (rightHand.transform.position, Vector3.down, out hitInfo,groundCheckDistance)) {
			isGrounded = true;
		}else{
			isGrounded=false;
		}		
	}

	public void Move(float v,float h){
		frogAnimator.SetFloat ("Forward", v);
		frogAnimator.SetFloat ("Turn", h);
	}

}
