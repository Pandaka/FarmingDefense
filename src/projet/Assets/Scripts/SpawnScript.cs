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
	public Transform destination = null;
	[SerializeField]
	GameObject player;


	void Update (){
		if (player.GetComponent<PlayerScript>().cmp_mob() > 0) {
			timeLeft -= Time.deltaTime;		
			if (timeLeft <= 0.0f) {
				foreach(NavMeshAgent go in mob)
				{
					if(!go.gameObject.activeSelf)
					{
						go.transform.position = self.position;
						go.gameObject.SetActive(true);
						go.destination = destination.position;
						player.GetComponent<PlayerScript>().modif_mob(1);
						break;
					}
				}
				timeLeft = interval;
			}
		}
	}
	
}