using UnityEngine;
using System.Collections;

public class victory_script : MonoBehaviour {

	[SerializeField]
	Collider col;

	bool paused;

	// Update is called once per frame
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("Victory");
		OnApplicationPause(true);
	}

	void OnApplicationPause(bool pauseStatus)
	{
		paused = pauseStatus;
	}
}
