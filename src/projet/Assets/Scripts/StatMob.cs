using UnityEngine;
using System.Collections;

public class StatMob : MonoBehaviour {

	[SerializeField]
	int attaque = 1;
	[SerializeField]
	int defense = 1;
	[SerializeField]
	int vitesse = 1;
	[SerializeField]
	int cout = 10;
	[SerializeField]
	int moneydrop= 50;
	[SerializeField]
	GameObject player_enemi;
	[SerializeField]
	GameObject player;


	//Fonction quand le sbire meurt
	public void onDeath() {
		//on augmente les billets du joueur adverse et on enlève un sbire de notre joueur.
		player_enemi.GetComponent<PlayerScript>().modif_Billet(moneydrop);
		player.GetComponent<PlayerScript>().modif_mob(-1);
		this.gameObject.SetActive(false);
	}

	//Fonction quand le sbire subit un dégat
	public void hit(int degat) {
		defense-=degat;
		if(defense<=0)
			onDeath();
	}

}