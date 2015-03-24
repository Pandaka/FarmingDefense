using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	
	float timeLeft = 0.0f;
	[SerializeField]
	float interval = 5.0f;
	[SerializeField]
	NavMeshAgent[] mob;
	[SerializeField]
	Transform self;
	[SerializeField]
	Transform destination = null;
	[SerializeField]
	GameObject player;


	void Update (){
		//Si le joueur n'a pas atteind la limite de sbires
		if (player.GetComponent<PlayerScript>().cmp_mob() > 0) {
			//décompte
			timeLeft -= Time.deltaTime;		
			if (timeLeft <= 0.0f) {
				//On cherche des mob désactivé
				foreach(NavMeshAgent go in mob)
				{
					if(!go.gameObject.activeSelf)
					{
						//on les placent au point de spwan on les actives et on leur donne la position de la base ennemi
						go.transform.position = self.position;
						go.gameObject.SetActive(true);
						go.destination = destination.position;
						break;
					}
				}
				//On augmente le nombre de sbires du joueur à qui appartient le spawn
				player.GetComponent<PlayerScript>().modif_mob(1);
				timeLeft = interval;
			}
		}
	}

}