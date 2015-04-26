using UnityEngine;
using System.Collections;

public class StatTower : MonoBehaviour {
	
	[SerializeField]
	int degat = 1;
	[SerializeField]
	int vieMax =10;
	[SerializeField]
	float portee = 2;
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
	[SerializeField]
	TowerUpgrade ownUpgrade;
	[SerializeField]
	GameObject ownZoneDetection;

	private int vie;
	private int save_VieMax;
	private float save_Portee;
	private int save_Degat;

	public SphereCollider Range {
		get {
			return range;
		}
		set {
			range = value;
		}
	}
	
	public int VieMax {
		get {
			return vieMax;
		}
		set {
			vieMax = value;
		}
	}
	
	public int Vie {
		get {
			return vie;
		}
		set {
			vie = value;
		}
	}


	void Start(){
		save_Degat=degat;
		save_Portee=portee;
		save_VieMax=vieMax;
		range.radius*=portee;
	}

	//fonction à la mort de la tour
	void onDeath(){
		//L'ennemi gagne de l'argent et la tour est remise à 0 et est désactivée
		player_ennemi.modif_Billet(billetDeath);
		vie=save_VieMax;
		portee=save_Portee;
		degat=save_Degat;
		ownUpgrade.Reset();
		range.radius=2;
		ownZoneDetection.transform.localScale=new Vector3(4f,0.1f,4f);
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
		portee+=modif;
		range.radius=portee;
	}
	//fonction pour modifier la défense
	public void modif_def(int modif){
		this.vieMax+=modif;
	}
	//fonction pour modifier l'attaque
	public void modif_attack(int modif){
		this.degat+=modif;
	}
}
