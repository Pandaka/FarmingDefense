using UnityEngine;
using System.Collections;


public class tower_shot_script : MonoBehaviour {

	[SerializeField]
	GameObject bullet;
	[SerializeField]
	int degat=1;

	bool inArea= false;


	void Update() {

	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Molecule"))
		{
			inArea=true;
			GameObject tir = GameObject.Instantiate(bullet,this.transform.position,Quaternion.identity)as GameObject;
			tir.GetComponent<bullet_goes_to_creep>().posEnnemy=col.transform;
			tir.GetComponent<bullet_goes_to_creep>().degat=degat;

		}
	}
	public void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Molecule"))
			inArea=false;
	}
}