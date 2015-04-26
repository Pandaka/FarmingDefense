using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	[SerializeField]
	int billet = 1000;
	[SerializeField]
	int nb_mob_max = 30;

	int nb_mob = 0;

	[SerializeField]
	int nb_tower_max = 10;

	int nb_tower = 0;

	[SerializeField]
	SpawnScript[] spawns;
	[SerializeField]
	int numero_joueur;
	[SerializeField]
	Camera cam_joueur;
	[SerializeField]
	TowerCreation tc;

	[SerializeField]
	Text nbBillet;

	[SerializeField]
	Text nbUnits;

	[SerializeField]
	Text nbTours;


	private NetworkPlayer player;
	private NetworkView playerNetwork;
	private bool activ;

	public NetworkPlayer Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}
	public NetworkView PlayerNetwork {
		get {
			return playerNetwork;
		}
		set {
			playerNetwork = value;
		}
	}


	public void isPlayer(int nb)
	{
		if(Network.player == player && nb==numero_joueur)
			cam_joueur.gameObject.SetActive(true);
	}
	
	public float timer = 10.0f;

	public int getNb()
	{
		return this.numero_joueur;
	}

	public void modif_Billet(int modif){
		this.billet+=modif;
	}

	public int combien_billet(){
		return this.billet;
	}

	public int combien_monstre(){
		return this.nb_mob;
	}

	public int combien_tour(){
		return this.nb_tower;
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

	public void disableCreation()
	{
		activ=false;
		tc.enabled=false;
	}

	void Start(){
		activ=false;
		tc.enabled=false;
	}

	void Update (){
		//Pour l'affichage
		nbBillet.text=""+this.billet;

		nbUnits.text=""+this.nb_mob+"/"+this.nb_mob_max;

		nbTours.text=""+this.nb_tower+"/"+this.nb_tower_max;

		//Pour le Spawn
		timer -= Time.deltaTime;

		if (timer <= 0) {
			timer = 0;
		}

		//1 = SpeedMob, 2 = TankMob, 3 = AttackMob
		if(Input.GetKeyDown(KeyCode.A) && Network.player==this.Player)
		{
			foreach(SpawnScript spawn in spawns)
				spawn.networkView.RPC("superSpawn",RPCMode.Server,1);
		}
		if(Input.GetKey(KeyCode.Z) && Network.player==this.Player)
		{
			foreach(SpawnScript spawn in spawns)
				spawn.networkView.RPC("superSpawn",RPCMode.Server,2);
		}
		if(Input.GetKey(KeyCode.E) && Network.player==this.Player)
		{
			foreach(SpawnScript spawn in spawns)
				spawn.networkView.RPC("superSpawn",RPCMode.Server,3);
		}
		if(Input.GetKeyDown(KeyCode.T) && Network.player==this.Player)
		{
			Debug.Log("T");
			if(!activ && Network.isClient ){
				tc.enabled=true;
				activ=true;
			}
			else{
				tc.enabled=false;
				activ=false;
			}
		}
	}		
}

