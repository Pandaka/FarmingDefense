using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShootScript : MonoBehaviour {

	[SerializeField]
	List<GameObject> bullet;
	[SerializeField]
	int degat = 1;


	public void OnTriggerEnter(Collider col) {
		//Quand un mob entre dans la zone de détection de la tour
		if(col.CompareTag("mob")) 
		{
			//On cherche un objet Bullet désactivé
			foreach(GameObject go in bullet)
			{
				if(!go.gameObject.activeSelf)
				{
					//Ensuite on le bouge à notre tour puis on lui transmet le transform de l'ennemi et les dégat de la tour
					go.transform.position = this.transform.position;
					go.gameObject.SetActive(true);
					go.GetComponent<BulletShoot>().enemy = col.transform;
					go.GetComponent<BulletShoot>().degat = degat;
					break;
				}
			}
		}
	}
	
	public void OnTriggerExit(Collider col) {
		//si la bullet sort de la zone de détection de la tour on la desactive
		if(col.CompareTag("bullet"))
			col.gameObject.SetActive(false);
	}
}