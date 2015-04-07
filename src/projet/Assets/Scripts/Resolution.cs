using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {

	public int resolution;

	void ChangeResolution () {

		switch(resolution){
		
		case 480 :
			Screen.SetResolution(640, 480, true);
			break;

		case 720 :
			Screen.SetResolution(1280, 720, true);
			break;

		case 1080 :
		Screen.SetResolution(1920, 1080, true);
			break;
		
		}
	}
}
