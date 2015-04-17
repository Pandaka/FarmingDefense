using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
	[SerializeField]
	float DragSpeed;
	
	private Vector3 _Move;
	
	// Update is called once per frame
	void Update()
	{
		_Move = new Vector3(0, 0, 0);
		
		// Déplacement à gauche
		if(Input.mousePosition.x <= 20 && camera.transform.position.z > -15)
			_Move = new Vector3(0, 0, -DragSpeed);
		
		// Déplacement à droite
		if(Input.mousePosition.x >= Screen.width-20 && camera.transform.position.z < 15)
			_Move = new Vector3(0, 0, DragSpeed);

		// Déplacement en bas
		if(Input.mousePosition.y <= 20 && camera.transform.position.x < 70)
			_Move = new Vector3(DragSpeed, 0, 0);

		// Déplacement en haut
		if(Input.mousePosition.y >= Screen.height-20 && camera.transform.position.x > -40)
			_Move = new Vector3(-DragSpeed, 0, 0);

		// Déplacement en bas à gauche
		if(Input.mousePosition.x <= 25 && Input.mousePosition.y <= 25) {
			if (camera.transform.position.z > -15 && camera.transform.position.x < 70)
				_Move = new Vector3(DragSpeed, 0, -DragSpeed);
			if (camera.transform.position.z > -15 && camera.transform.position.x > 70)
				_Move = new Vector3(0, 0, -DragSpeed);
			if (camera.transform.position.z < -15 && camera.transform.position.x < 70)
				_Move = new Vector3(DragSpeed, 0, 0);
		}

		// Déplacement en haut à gauche
		if(Input.mousePosition.x <= 25 && Input.mousePosition.y >= Screen.height-25) {
			if (camera.transform.position.z > -15 && camera.transform.position.x > -40)
				_Move = new Vector3(-DragSpeed, 0, -DragSpeed);
			if (camera.transform.position.z > -15 && camera.transform.position.x < -40)
				_Move = new Vector3(0, 0, -DragSpeed);
			if (camera.transform.position.z < -15 && camera.transform.position.x > -40)
				_Move = new Vector3(-DragSpeed, 0, 0);
		}

		// Déplacement en bas à droite
		if(Input.mousePosition.x >= Screen.width-25 && Input.mousePosition.y <= 25) {
			if (camera.transform.position.z < 15 && camera.transform.position.x < 70)
				_Move = new Vector3(DragSpeed, 0, DragSpeed);
			if (camera.transform.position.z < 15 && camera.transform.position.x > 70)
				_Move = new Vector3(0, 0, DragSpeed);
			if (camera.transform.position.z > 15 && camera.transform.position.x < 70)
				_Move = new Vector3(DragSpeed, 0, 0);
		}

		// Déplacement en haut à droite
		if(Input.mousePosition.x >= Screen.width-25 && Input.mousePosition.y >= Screen.height-25) {
			if (camera.transform.position.z < 15 && camera.transform.position.x > -40)			
				_Move = new Vector3(-DragSpeed, 0, DragSpeed);
			if (camera.transform.position.z < 15 && camera.transform.position.x < -40)			
				_Move = new Vector3(0, 0, DragSpeed);
			if (camera.transform.position.z > 15 && camera.transform.position.x > -40)			
				_Move = new Vector3(-DragSpeed, 0, 0);
		}

		transform.Translate(_Move, Space.World);
	}
}
