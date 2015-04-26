using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobAttack : MonoBehaviour {

	[SerializeField]
	GameObject mob;
	[SerializeField]
	List<Collider> collider_tower;

	private NavMeshAgent nav_mob;
	private StatMob stat_mob;
	private float saveSpeed;
	private float timeLeft = 0.0f;
	private float interval = 2.0f;
	private GameObject enemi;
	private StatTower tower;

	protected bool paused;
	
	void OnPauseGame () 
	{ 
		paused = true; 
	}
	
	void OnResumeGame () 
	{ 
		paused = false; 
	}

	void Start(){
		stat_mob=mob.GetComponent<StatMob>();
		nav_mob=mob.GetComponent<NavMeshAgent>();
		saveSpeed=nav_mob.speed;
	}

	//Si une tour est dans le champs de vision du mob
	public void OnTriggerEnter(Collider col){
		foreach (Collider collid in collider_tower){
		if(col==collid)
		{
			//on stop le sbire et le sbire attaque la tour
			nav_mob.speed=0;
			enemi=col.gameObject;
			tower = enemi.GetComponent<StatTower>();
			}
		}
	}

	//remplacer par Update()
	void Update(){
		if(!paused)
		{	if(enemi!= null)
			{
				timeLeft -= Time.deltaTime;		
				if (timeLeft <= 0.0f) {
					tower.hit(stat_mob.getAttack());
					timeLeft = interval;
				}
				if(!enemi.activeSelf)
				{
					//si la tour est détruite alors le sbire reprend sa route
					nav_mob.speed=saveSpeed;
				}
			}
		}
	}
}