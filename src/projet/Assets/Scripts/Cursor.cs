using UnityEngine;
using System.Collections;


public class Cursor : MonoBehaviour {

	[SerializeField]
	Texture2D curseur;

	void Start() {
		Screen.showCursor = false; 
	}
	
	void OnGUI() {
		Vector3 positionSouris = Input.mousePosition;
		Rect positionCurseur = new Rect(positionSouris.x,Screen.height - positionSouris.y,curseur.width,curseur.height);
		GUI.Label(positionCurseur,curseur);
	}
	
}
