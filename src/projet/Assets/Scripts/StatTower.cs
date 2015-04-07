using UnityEngine;
using System.Collections;

public class StatTower : MonoBehaviour {
	
	[SerializeField]
	int degat = 1;
	[SerializeField]
	int vieMax =10;
	[SerializeField]
	float portee = 1;
	[SerializeField]
	int billetDeath = 200;
	[SerializeField]
	PlayerScript player_ennemi;
	[SerializeField]
	PlayerScript joueur;
	[SerializeField]
	SphereCollider range;
	[SerializeField]
	Renderer lifebar;
	
	private int vie;

	
	public SphereCollider Range {
		get {
			return range;
		}
		set {
			range = value;
		}
	}


	void Start(){
		range.radius*=portee;
	}

	//fonction à la mort de la tour
	void onDeath(){
		//L'ennemi gagne de l'argent et la tour est désactiver
		player_ennemi.modif_Billet(billetDeath);
		vie=vieMax;
		joueur.modif_nbTour(-1);
		this.gameObject.SetActive(false);
	}

	public void lifeBarInitiate(){
		vie = vieMax;
		lifebar.material.SetInt("_MaxHps", vieMax);
		lifebar.material.SetInt("_Hps", vie);
	}

	//Fonction quand la tour prend des dommages
	public void hit(int degat){
		//réduction point de vie
		this.vie-=degat;
		lifebar.material.SetInt("_Hps", vie);
		if(this.vie<=0)
			onDeath();
	}

	//foncion pour récupérer les dégats de la tour
	public int damage(){
		return this.degat;
	}

	//fonction pour modifier la portée
	public void modif_portee(float modif){
		portee=modif;
		range.radius=2*portee;
	}
}
