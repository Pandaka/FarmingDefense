﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShootScript : MonoBehaviour {

	[SerializeField]
	GameObject[] bullets;
	[SerializeField]
	StatTower tower_stat;
	[SerializeField]
	float timer = 2.0f;
	[SerializeField]
	GameObject self;
		
	private float interval = 0.0f;
	private List<GameObject> cibles;
	
	//on initialise
	void Start(){
		cibles = new List<GameObject>();
	}


	public void OnTriggerEnter(Collider col) {
		if(this.CompareTag("tower_j1") && col.gameObject.CompareTag("mob_j2") && !cibles.Contains(col.gameObject)) 
		{
			cibles.Add(col.gameObject);
		}
		else if(this.CompareTag("tower_j2") && col.gameObject.CompareTag("mob_j1") && !cibles.Contains(col.gameObject)) 
		{
			cibles.Add(col.gameObject);
		}
	}
	
	public void OnTriggerExit(Collider col) {
		//si un sbire sort de la zone de détection on ne le prend plus pour cible
		if(cibles.Contains(col.gameObject)){
			cibles.Remove(col.gameObject);
		}
	}

	void Update () {
		//si on est le serveur on fait le timer d'attaque
		if (Network.isServer)
		{
			if(cibles.Count>0){
				foreach(GameObject c in cibles){
					if(!c.activeSelf)
					{
						cibles.Remove(c);
					}
				}
				interval -= Time.deltaTime;
				if(interval < 0.0f && cibles.Count>0)
				{
					if(cibles[0].transform.position.x-self.transform.position.x<tower_stat.Range.radius || 
					   cibles[0].transform.position.z-self.transform.position.z<tower_stat.Range.radius)
					{
						//on active la bullet.
						int bullet = HandleBullet();
						if(bullet >=0 && cibles.Count>0)
						{
							networkView.RPC("feu", RPCMode.All, bullet);
							interval = timer;
						}
					}
				}	
			}
		}
	}

	private int HandleBullet(){
		int i=0;
		foreach(GameObject go in bullets)
		{
			if(!go.activeSelf)
			{
				return i;
			}
			i++;
		}
		return -1;
	}
	

	[RPC]
	void feu(int idBullet){
		//on choisi la bullet puis on l'active et on la met sur la position de la tour
		GameObject bullet = bullets[idBullet];
		bullet.SetActive(true);
		bullet.transform.position = this.transform.position;
		//On lui donne les damage de la tour et l'emplacement de l'ennemi
		BulletShoot bs = bullet.GetComponent<BulletShoot>(); 
		bs.takeTarget(cibles[0].transform);
		bs.takeDamage(tower_stat.damage());
		bs.Tag_enemy(cibles[0].tag);
		bs.takePos(bullet.transform);
	}
}
