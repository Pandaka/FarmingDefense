using UnityEngine;
using System.Collections;

public class StatMob : MonoBehaviour {

	[SerializeField]
	int attaque = 1;
	[SerializeField]
	int defense = 1;
	[SerializeField]
	float vitesse = 1;
	[SerializeField]
	int cout = 10;
	[SerializeField]
	int moneydrop= 50;
	[SerializeField]
	GameObject player_enemi;
	[SerializeField]
	GameObject player;
	[SerializeField]
	Renderer lifebar;
	[SerializeField]
	NavMeshAgent nav_mob;

	private int lifemax;
	private PlayerScript player_script;
	private PlayerScript enemi_script;
	private float speed;


	void Start(){
		lifemax=defense;
		player_script = player.GetComponent<PlayerScript>();
		enemi_script = player_enemi.GetComponent<PlayerScript>();
		speed = nav_mob.speed;
		this.GetComponent<NavMeshAgent>().speed*=vitesse;
	}
	
	//fonction pour avoir la barre de vie avec les bonnes valeurs
	public void lifeBarInitiate(){
		lifebar.material.SetInt("_MaxHps", defense);
		lifebar.material.SetInt("_Hps", defense);
	}

	public int getCout(){
		return this.cout;
	}

	public int getAttack(){
		return this.attaque;
	}
	
	//Fonction quand le sbire subit un dégat
	
	public void hit(int degat) {
		if(Network.isServer)
			networkView.RPC("hit_phase2",RPCMode.All,degat);
	}

	[RPC]
	void hit_phase2(int degat) {
		defense-=degat;
		lifebar.material.SetInt("_Hps", defense);
		if(defense<=0)
		{
			//on augmente les billets du joueur adverse et on enlève un sbire de notre joueur.
			enemi_script.modif_Billet(moneydrop);
			player_script.modif_mob(-1);
			defense=lifemax;
			nav_mob.speed=speed;
			this.gameObject.SetActive(false);
		}
	}
}