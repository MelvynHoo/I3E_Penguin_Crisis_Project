using UnityEngine;
using System.Collections;

public class GiantTortoiseRandomWalk : MonoBehaviour {
	float aimForwardSpeed=0f;
	float aimTurnSpeed=0f;
	float initialForwardSpeed=0f;
	float initialTurnSpeed=0f;
	float forwardSpeed=0f;
	float turnSpeed=0f;

	float speedChangeTime=0f;
	float nextSpeedChangeTime=0f;
	GiantTortoiseCharacter giantTortoiseCharacter;


	void Start () {
		nextSpeedChangeTime=Random.Range(3f,6f);	
		giantTortoiseCharacter = GetComponent < GiantTortoiseCharacter> ();
	}

	void FixedUpdate () {
		speedChangeTime += Time.deltaTime;
		if (nextSpeedChangeTime < speedChangeTime) {
			nextSpeedChangeTime=Random.Range(3f,6f);
			speedChangeTime=0f;
			giantTortoiseCharacter.DefenseEnd();
			giantTortoiseCharacter.EatEnd();

			float rnd=Random.Range(0f,3f);
			if(rnd<1f){
				initialForwardSpeed=forwardSpeed;
				initialTurnSpeed=turnSpeed;
				aimForwardSpeed=Random.Range(-1f,1f);
				aimTurnSpeed=Random.Range(-1f,1f);
			}else if(rnd<2f){
				giantTortoiseCharacter.DefenseStart();
				forwardSpeed=0f;
				turnSpeed=0f;
				initialForwardSpeed=0f;
				initialTurnSpeed=0f;
				aimForwardSpeed=0f;
				aimTurnSpeed=0f;
			}else{
				giantTortoiseCharacter.EatStart();
				forwardSpeed=0f;
				turnSpeed=0f;
				initialForwardSpeed=0f;
				initialTurnSpeed=0f;
				aimForwardSpeed=0f;
				aimTurnSpeed=0f;
			}
		}

		forwardSpeed = Mathf.Lerp (initialForwardSpeed, aimForwardSpeed, speedChangeTime);
		turnSpeed = Mathf.Lerp (initialTurnSpeed, aimTurnSpeed, speedChangeTime);

		giantTortoiseCharacter.Move (forwardSpeed,turnSpeed);
	}
}
