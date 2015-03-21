using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerShootScript : MonoBehaviour {

	[SerializeField]
	List<GameObject> bullet;
	[SerializeField]
	int degat = 1;

	private Transform enemy;

	public void OnTriggerEnter(Collider col) {
		if(col.CompareTag("mob")) {
			enemy = col.transform;
			networkView.RPC("CreateBullet", RPCMode.All,enemy.position);
		}
	}
	
	public void OnTriggerExit(Collider col) {
		if(col.CompareTag("bullet"))
			col.gameObject.SetActive(false);
	}

	[RPC]
	void CreateBullet (Vector3 pos_enemy) {

		foreach(GameObject go in bullet)
		{
			if(!go.gameObject.activeSelf)
			{
				go.transform.position = this.transform.position;
				go.gameObject.SetActive(true);
				go.GetComponent<BulletShoot>().enemy = enemy;
				go.GetComponent<BulletShoot>().degat = degat;
				break;
			}
		}
	}	

}