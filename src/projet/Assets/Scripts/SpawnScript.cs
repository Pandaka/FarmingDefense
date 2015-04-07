using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	
	float timeLeft = 0.0f;
	[SerializeField]
	float interval = 5.0f;
	[SerializeField]
	NavMeshAgent[] mob;
	[SerializeField]
	NavMeshAgent[] speedMob;
	[SerializeField]
	NavMeshAgent[] tankMob;
	[SerializeField]
	NavMeshAgent[] attackMob;

	[SerializeField]
	Transform self;
	[SerializeField]
	Transform destination = null;
	[SerializeField]
	GameObject player;

	private PlayerScript player_script;

	void Start(){
		player_script = player.GetComponent<PlayerScript>();
	}

	void Update (){
		//Si le joueur n'a pas atteind la limite de sbires
		if (player_script.cmp_mob() > 0) {
			//décompte
			timeLeft -= Time.deltaTime;		
			if (timeLeft <= 0.0f) {
				if(Network.isServer)
					networkView.RPC("Spawn",RPCMode.All);
				timeLeft = interval;
			}
		}
	}

	[RPC]
	public void superSpawn(int i)
	{
		Debug.Log(i);
		if(player_script.combien_billet() >=30 && player_script.cmp_mob()>0)
			networkView.RPC("SuperSpawn",RPCMode.All,i);
	}

	[RPC]
	void Spawn(){
		//On cherche des mob désactivé
		foreach(NavMeshAgent go in mob)
		{
			if(!go.gameObject.activeSelf)
			{
				//on les placent au point de spwan on les actives et on leur donne la position de la base ennemi
				go.transform.position = self.position;
				StatMob sbire = go.gameObject.GetComponent<StatMob>();
				sbire.lifeBarInitiate();
				go.gameObject.SetActive(true);
				go.destination = destination.position;
				//On augmente le nombre de sbires du joueur à qui appartient le spawn
				player_script.modif_mob(1);
				break;
			}
		}
	}

	[RPC]
	void SuperSpawn(int style){
		//1 = SpeedMob, 2 = TankMob, 3 = AttackMob
		Debug.Log("SuperSpawn");
		switch(style)
		{
		case 1 :
			foreach(NavMeshAgent go in speedMob)
			{
				if(!go.gameObject.activeSelf)
				{
					//on les placent au point de spwan on les actives et on leur donne la position de la base ennemi
					go.transform.position = self.position;
					StatMob sbire = go.gameObject.GetComponent<StatMob>();
					sbire.lifeBarInitiate();
					go.gameObject.SetActive(true);
					go.destination = destination.position;
					//On augmente le nombre de sbires du joueur à qui appartient le spawn
					player_script.modif_mob(1);
					player_script.modif_Billet(-sbire.getCout());
					break;
				}
			}
			break;
		case 2 :
			foreach(NavMeshAgent go in tankMob)
			{
				if(!go.gameObject.activeSelf)
				{
					//on les placent au point de spwan on les actives et on leur donne la position de la base ennemi
					go.transform.position = self.position;
					StatMob sbire = go.gameObject.GetComponent<StatMob>();
					sbire.lifeBarInitiate();
					go.gameObject.SetActive(true);
					go.destination = destination.position;
					//On augmente le nombre de sbires du joueur à qui appartient le spawn
					player_script.modif_mob(1);
					player_script.modif_Billet(-sbire.getCout());
					break;
				}
			}
			break;
		case 3 :
			foreach(NavMeshAgent go in attackMob)
			{
				if(!go.gameObject.activeSelf)
				{
					//on les placent au point de spwan on les actives et on leur donne la position de la base ennemi
					go.transform.position = self.position;
					StatMob sbire = go.gameObject.GetComponent<StatMob>();
					sbire.lifeBarInitiate();
					go.gameObject.SetActive(true);
					go.destination = destination.position;
					//On augmente le nombre de sbires du joueur à qui appartient le spawn
					player_script.modif_mob(1);
					player_script.modif_Billet(-sbire.getCout());
					break;
				}
			}
			break;



		}
	}

}