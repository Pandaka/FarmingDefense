using UnityEngine;
using System.Collections;

public class WinLoseScript : MonoBehaviour {

	[SerializeField]
	int limit_mob;

	private int cmp;

	// Met le jeu en pause quand le joueur a rempli les conditions de victoire
	void OnTriggerEnter(Collider objet) {
		if (objet.CompareTag("mob_j1"))
			cmp++;
		if (limit_mob*2 <= cmp) {
			Debug.Log("J1 vainqueur");
			Time.timeScale = 0;
		}
	}

}
