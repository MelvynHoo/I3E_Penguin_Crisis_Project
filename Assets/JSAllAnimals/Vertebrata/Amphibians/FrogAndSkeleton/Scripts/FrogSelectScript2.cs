using UnityEngine;
using System.Collections;

public class FrogSelectScript2 : MonoBehaviour {
	public GameObject frogCamera;
	public GameObject[] frogs;
	int currentFrogNum=1;
	
	public void FrogSelect(int frogNum){
		FrogCameraScript frogcamerascript=frogCamera.GetComponent<FrogCameraScript>();
		frogcamerascript.target = frogs [frogNum];
		currentFrogNum=frogNum;
	}
	
	public void SetJumpSpeed(float sp){
		frogs [currentFrogNum].GetComponentInParent<FrogCharacter3> ().jumpSpeed = sp;
	}
}
