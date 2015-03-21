using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerCreation : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	[SerializeField]
	List<Transform> tour;
	[SerializeField]
	GameObject player;


	public Transform map;
	public Vector3 whereToBuild = Vector3.zero;
	bool impossibleThere;

	void Start () 
	{
	}
	
	void Update () {
		//si on fait clic gauche
		if (Input.GetMouseButtonDown(0)) {
			//si le joueur n'a pas atteind le nombre de tour max
			if(player.GetComponent<PlayerScript>().cmp_tower()>0)
			{
				//on capture la position du clic sur la map
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				//vérification qu'on touche la map
				if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == map.gameObject) {		

					var collidedObjs = Physics.OverlapSphere(hit.point, 1f, 1 << LayerMask.NameToLayer("obstacle"));

					//si la tour est dans la bonne partie du terrain et qu'on a assez de billet pour la créer
					if(hit.point.x > 0 && player.GetComponent<PlayerScript>().combien_billet()>=100){
						//on arrange x et z pour une postion plus propre des tours
						float x = Mathf.Round(hit.point.x);
						float z = Mathf.Round(hit.point.z);

						if (collidedObjs == null || collidedObjs.Length == 0) {
							//on met les coordées dans un vecteur 3
							Vector3 tmp = new Vector3 (x, 1.4f, z);
							//envoie du rpc pour créer la tour
							networkView.RPC("CreateTower", RPCMode.All, tmp);

						}
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
				
				//ajout d'un tour au compteur et on enlève des billets
				player.GetComponent<PlayerScript>().modif_nbTour(1);
				player.GetComponent<PlayerScript>().modif_Billet(-100);
				break;
			}
		}
	}	

}