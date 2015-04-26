using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AffichageMenu : MonoBehaviour {

	[SerializeField]
	bool isBuildingServer;
	[SerializeField]
	GameObject creer;
	[SerializeField]
	GameObject rejoindre;

	// Use this for initialization
	void Start () {
		if(isBuildingServer)
			rejoindre.gameObject.SetActive(false);
		else
			creer.gameObject.SetActive(false);
	}

}
