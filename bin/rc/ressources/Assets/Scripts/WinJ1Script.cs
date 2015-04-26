using UnityEngine;
using System.Collections;

public class WinJ1Script : MonoBehaviour {
	
	[SerializeField]
	int limit_mob;
	
	private int cmp;
	
	// Met le jeu en pause quand le joueur a rempli les conditions de victoire
	void OnTriggerEnter(Collider objet) {
		if (objet.CompareTag("mob_j1")) {
			cmp++;
		}
		if (limit_mob <= cmp) {
			Debug.Log("J1 vainqueur");
			Time.timeScale = 0;
		}
	}
	
}