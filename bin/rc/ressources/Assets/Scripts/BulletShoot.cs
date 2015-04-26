using UnityEngine;
using System.Collections;
public class BulletShoot : MonoBehaviour {
	
	private Transform enemy;
	private int degat;
	private Transform pos_tower;
	private string tag_enemy;

	
	protected bool paused;
	
	void OnPauseGame () 
	{ 
		paused = true; 
	}
	
	void OnResumeGame () 
	{ 
		paused = false; 
	}


	public void Tag_enemy(string cible)
	{
		this.tag_enemy= cible;
	}
	public void takeTarget(Transform target){
		this.enemy=target;
	}
	
	public void takeDamage(int damage){
		this.degat=damage;
	}
	public void takePos(Transform bullet){
		this.pos_tower=bullet;
	}
	
	void FixedUpdate () {
		if(!paused)
		{
			//Si l'ennemi est désactivé on suprime la Bullet sinon on la bouge jusqu'à sa cible
			if (enemy==null || enemy.gameObject.activeSelf == false)
			{
				this.transform.position=pos_tower.position;
				enemy=null;
				this.gameObject.SetActive(false);
			}
			else {
				this.transform.position = Vector3.Lerp(transform.position,enemy.transform.position,0.07f);
				//this.transform.Translate(Vector3.forward*Time.deltaTime);
				this.transform.LookAt(enemy.transform);
			}
		}
	}
	
	void OnTriggerEnter(Collider col) {
		//si la bullet entre en collision avec un mob
		if (col.gameObject.CompareTag(tag_enemy)) {
			//on appel Hit et on désactive la bullet
			col.GetComponent<StatMob>().hit(degat);
			this.gameObject.SetActive(false);
		}
		else if (!col.gameObject.CompareTag("tower_j1") && !col.gameObject.CompareTag("tower_j2"))
		{
			this.gameObject.SetActive(false);
		}
	}
	
}
