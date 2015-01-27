using UnityEngine;
using System.Collections;

public class creation_tour : MonoBehaviour {
	Ray ray;
	RaycastHit hit;
	public GameObject tour;
	public Transform map;
	public Vector3 whereToBuild = Vector3.zero;
	GameObject tower = null;
	bool impossibleThere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == map.gameObject) { 
			
				var collidedObjs = Physics.OverlapSphere(hit.point, 1f, 1 << LayerMask.NameToLayer("Trees"));

				if (collidedObjs == null || collidedObjs.Length == 0)
				{
					tower = Instantiate (tour, new Vector3 (hit.point.x, map.position.y, hit.point.z), Quaternion.identity) as GameObject;
					tower.gameObject.SetActive (true);
				}
//
//				if (tower) {
//					tour.transform.position = new Vector3 (hit.point.x, tour.renderer.bounds.size.y / 2, hit.point.z);
//					tour.transform.rotation = Quaternion.identity;
//					
//					tower.transform.position = new Vector3 (hit.point.x, tour.renderer.bounds.size.y / 2, hit.point.z);
//					Debug.DrawLine (ray.origin, hit.point, Color.cyan, 0.1f);
//					
//				}
			}
		}

			


			
//			if (Input.GetMouseButton(0)) {
//				
//				if (tower) {
//					tour.transform.position = new Vector3 (hit.point.x, tour.renderer.bounds.size.y / 2, hit.point.z);
//					tour.transform.rotation = Quaternion.identity;
//					
//					tower.transform.position = new Vector3 (hit.point.x, tour.renderer.bounds.size.y / 2, hit.point.z);
//					Debug.DrawLine (ray.origin, hit.point, Color.cyan, 0.1f);
//
//				}
//				
//			}
//			
//			if (Input.GetMouseButtonUp(0)) {
//				tower = Instantiate(tour, tour.transform.position, tour.transform.rotation) as GameObject;                  
//			}
	

		
	}
}
