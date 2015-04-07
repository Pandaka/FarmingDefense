using UnityEngine;
using System.Collections;

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


	public float timer = 10.0f;


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


	void  Update (){
		timer -= Time.deltaTime;

		if (timer <= 0) {
			timer = 0;
		}

		if(Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log("s");
			foreach(SpawnScript spawn in spawns)
				spawn.networkView.RPC("superSpawn",RPCMode.Server,1);
		}
		if(Input.GetKey(KeyCode.T))
		{
			foreach(SpawnScript spawn in spawns)
				spawn.superSpawn(2);
		}
		if(Input.GetKey(KeyCode.A))
		{
			foreach(SpawnScript spawn in spawns)
				spawn.superSpawn(3);
		}
	}

		





	void OnGUI () {
			
			GUI.Button (new Rect (0, 0, 100, 20), "Billet = " + billet);
			
			GUI.Button (new Rect (0,30, 100, 20), "Tours " + nb_tower + "/" + nb_tower_max);
			
			GUI.Button (new Rect (0,60, 100, 20), "Unités " + nb_mob + "/" + nb_mob_max);
			
			GUI.Box(new Rect(10,710,50,20), ""+timer.ToString("0"));
			
		}
	}

