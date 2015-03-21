using UnityEngine;
using System.Collections;
public class BulletShoot : MonoBehaviour {
	
	public Transform enemy;
	public int degat;

	void Update () {
		if (enemy.gameObject.activeSelf == false)
			this.gameObject.SetActive(false);
		else 
			transform.position = Vector3.Lerp(transform.position,enemy.transform.position,0.07f);

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.CompareTag("mob")) {
			col.GetComponent<StatMob>().hit(degat);
			this.gameObject.SetActive(false);
		}
		else if (!col.gameObject.CompareTag("tower"))
		{
			this.gameObject.SetActive(false);
		}
	}
}
