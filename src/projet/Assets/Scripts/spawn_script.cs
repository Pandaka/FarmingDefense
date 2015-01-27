using UnityEngine;
using System.Collections;

public class spawn_script : MonoBehaviour {

	public float interval = 5.0f;
	
	float timeLeft = 0.0f;
	
	public GameObject mob = null;
	
	public Transform destination = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(player_script.nb_mob_max>player_script.nb_mob)
		{
			timeLeft -= Time.deltaTime;
		
			if (timeLeft <= 0.0f) {

				GameObject g = (GameObject)Instantiate(mob, transform.position, Quaternion.identity);
				NavMeshAgent n= g.GetComponent<NavMeshAgent>();
				n.destination = destination.position;
				player_script.nb_mob+=1;
				timeLeft = interval;
			}
		}
	}
}
