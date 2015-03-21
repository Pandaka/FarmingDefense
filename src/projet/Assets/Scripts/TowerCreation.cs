using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerCreation : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	[SerializeField]
	List<Transform> tour;
	public Transform map;
	public Vector3 whereToBuild = Vector3.zero;
	bool impossibleThere;

	void Start () {}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if(PlayerScript.nb_tower<PlayerScript.nb_tower_max)
			{
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == map.gameObject) {		
					var collidedObjs = Physics.OverlapSphere(hit.point, 1f, 1 << LayerMask.NameToLayer("obstacle"));
					if (collidedObjs == null || collidedObjs.Length == 0) {
						Vector3 tmp = new Vector3 (hit.point.x, 1.400007f, hit.point.z);
						networkView.RPC("CreateTower", RPCMode.All, tmp);
						PlayerScript.nb_tower++;
					}
				}
			}
		}			
	}

	[RPC]
	void CreateTower (Vector3 pos) {
		foreach(Transform go in tour)
		{
			if(!go.gameObject.activeSelf)
			{
				go.position = pos;
				go.gameObject.SetActive(true);
				break;
			}
		}
	}	

}