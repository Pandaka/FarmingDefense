using UnityEngine;
using System.Collections;
public class BulletShoot : MonoBehaviour {
	
	public Transform enemy;
	public int degat;

	void Update () {
		//Si l'ennemi est désactivé on suprime la Bullet sinon on la bouge jusqu'à sa cible
		if (enemy.gameObject.activeSelf == false)
			this.gameObject.SetActive(false);
		else 
			transform.position = Vector3.Lerp(transform.position,enemy.transform.position,0.07f);

	}

	void OnTriggerEnter(Collider col) {
		//si la bullet entre en collision avec un mob
		if (col.gameObject.CompareTag("mob")) {
			//on appel Hit et on désactive la bullet
			col.GetComponent<StatMob>().hit(degat);
			this.gameObject.SetActive(false);
		}
		else if (!col.gameObject.CompareTag("tower"))
		{
			this.gameObject.SetActive(false);
		}
	}
}
