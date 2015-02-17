using UnityEngine;
using System.Collections;

public class bullet_goes_to_creep : MonoBehaviour {

	public Transform posEnnemy;
	public int degat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position,posEnnemy.position,0.05f);
	}
	void OnTriggerEnter(Collider col)
	{
		col.GetComponent<stat_mob_script>().hit(degat);
		Destroy(gameObject);
	}
}
