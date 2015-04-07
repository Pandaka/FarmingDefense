using UnityEngine;using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour {
	

	public string levelSuivant;
	
	void  OnMouseUp (){ 
		
		switch(levelSuivant) {
		case "Deconnecter" :
			Application.Quit();
			break;
			
		case "Lancer" :
			Application.LoadLevel("Lancer");
			break;
			
		case "Charger" :
			Application.LoadLevel("Charger");
			break;
			
		case "Options" :
			Application.LoadLevel("Options");
			break;

		case "Menu" :
			Application.LoadLevel("Menu");
			break;

		case "Resolution" :
			Application.LoadLevel("Resolution");
			break;

		case "Commandes" :
			Application.LoadLevel("Commandes");
			break;

		case "Main_scene" :
			Application.LoadLevel("Main_scene");
			break;

			
		}
		
	}
		
}