using UnityEngine;
using System.Collections;

public class WinJ2Script : MonoBehaviour {
	
	[SerializeField]
	int limit_mob;
	
	private int cmp;
	
	// Met le jeu en pause quand le joueur a rempli les conditions de victoire
	void OnTriggerEnter(Collider objet) {
		if (objet.CompareTag("mob_j2")) {
			cmp++;
		}
		if (limit_mob <= cmp) {
			Debug.Log("J2 vainqueur");
			Time.timeScale = 0;
		}
	}
	
}