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

	void Update (){
		if (PlayerScript.nb_mob_max > PlayerScript.nb_mob) {
			timeLeft -= Time.deltaTime;		
			if (timeLeft <= 0.0f) {
				foreach(NavMeshAgent go in mob)
				{
					if(!go.gameObject.activeSelf)
					{
						go.transform.position = self.position;
						go.gameObject.SetActive(true);
						go.destination = destination.position;
						break;
					}
				}
				PlayerScript.nb_mob += 1;
				timeLeft = interval;
			}
		}
	}
	
}