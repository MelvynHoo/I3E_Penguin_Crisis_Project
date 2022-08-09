using UnityEngine;
using System.Collections;

public class FrogCharacter3 : MonoBehaviour {
	
	Animator frogAnimator;
	public float groundCheckDistance=.5f;
	public bool isGrounded=false;
	
	Rigidbody frogRigid;
	public bool jumpUp=false;
	public float jumpSpeed=.5f;
	public bool overturned=false;
	public GameObject frogGyro;
	
	void Start () {
		frogAnimator = GetComponent<Animator> ();
		frogRigid=GetComponent<Rigidbody>();
		frogGyro = new GameObject ("FrogGyro");
	}
	
	void Update () {
		
		GroundedCheck ();	
		frogGyro.transform.position = transform.position;
		if (Vector3.Dot (frogGyro.transform.forward, transform.right) > 0f) {
			frogGyro.transform.RotateAround (frogGyro.transform.position, Vector3.up, -Time.deltaTime*100f);
		} else {
			frogGyro.transform.RotateAround (frogGyro.transform.position, Vector3.up, Time.deltaTime*100f);
		}
	}
	
	public void Jump(){
		if (isGrounded && transform.up.y>0f) {
			frogAnimator.applyRootMotion=false;
			frogRigid.AddForce((transform.forward+transform.up)*jumpSpeed,ForceMode.Impulse);
			jumpUp=true;
			isGrounded=false;
			frogAnimator.SetTrigger("Jump");
			frogRigid.constraints=RigidbodyConstraints.None;
			overturned=true;
		}
	}
	
	public void Eat(){
		frogAnimator.SetTrigger("Eat");
	}
	
	public void Swim(){
		frogAnimator.SetTrigger("Swim");
	}
	
	void GroundedCheck(){
		
		if (frogAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "Fall") {
			jumpUp=false;
		}
		RaycastHit hit;
		if (!jumpUp) {
			if (Physics.Raycast (transform.position, Vector3.down,out hit,groundCheckDistance)) {
				isGrounded = true;
				frogAnimator.SetBool ("IsGrounded", true);
				float mag=Vector3.Cross(transform.up,hit.normal).sqrMagnitude;
				
				if(overturned){
					if(transform.up.y>0f){
						transform.rotation=Quaternion.Lerp(transform.rotation,frogGyro.transform.rotation,10f*Time.deltaTime);
						if(mag<.01f){
							overturned=false;
							frogRigid.constraints=RigidbodyConstraints.FreezeRotation;
							frogAnimator.applyRootMotion = true;
						}
					}else{
						transform.rotation=Quaternion.Lerp(transform.rotation,frogGyro.transform.rotation,10f*Time.deltaTime);
					}
				}else if(mag>.1f || transform.up.y<0f){
					overturned=true;
				}
			} else {
				isGrounded = false;
				frogAnimator.SetBool ("IsGrounded", false);
				frogAnimator.applyRootMotion=false;
				frogRigid.constraints=RigidbodyConstraints.None;
				overturned=true;
			}		
		}
		
	}
	
	public void Move(float v,float h){
		frogAnimator.SetFloat ("Forward", v);
		frogAnimator.SetFloat ("Turn", h);
	}

}
