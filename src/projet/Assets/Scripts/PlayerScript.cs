using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	int billet = 200;
	[SerializeField]
	int nb_mob_max = 30;
	int nb_mob = 0;
	[SerializeField]
	int nb_tower_max = 10;
	int nb_tower = 0;

	public void modif_Billet(int modif){
		this.billet+=modif;
	}

	public int combien_billet(){
		return this.billet;
	}

	public void modif_nbTour(int modif){
		this.nb_tower+=modif;
	}
	
	public int cmp_tower(){
		return nb_tower_max-nb_tower;
	}

	public void modif_mob(int modif){
		this.nb_mob+=modif;
	}
	
	public int cmp_mob(){
		return nb_mob_max-nb_mob;
	}

}