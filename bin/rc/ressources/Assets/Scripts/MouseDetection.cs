using UnityEngine;
using System.Collections;

public class MouseDetection : MonoBehaviour {

	[SerializeField]
	int tailleEntrer = 27;
	[SerializeField]
	int tailleSortie = 25;


	void OnMouseEnter() {
		guiText.fontSize = tailleEntrer;
	}
	
	void OnMouseExit() {
		guiText.fontSize = tailleSortie;
	}


}
