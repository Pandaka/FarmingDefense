using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour {

	public string resolution;
	private bool stateScreen;

	void OnMouseUp () {

		if (Screen.fullScreen)
			stateScreen = true;
		else stateScreen = false;

		switch(resolution){		
			case "480" :
				Debug.Log(stateScreen);
				Screen.SetResolution(640, 480, stateScreen);
				break;	
			case "720" :
				Screen.SetResolution(1280, 720, stateScreen);
				break;
			case "1080" :
				Screen.SetResolution(1920, 1080, stateScreen);
				break;
			case "full" :
				Screen.fullScreen = !Screen.fullScreen;
				break;	
		}
	}
}
