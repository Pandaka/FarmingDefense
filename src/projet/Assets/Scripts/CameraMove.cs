using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
	[SerializeField]
	float DragSpeed = 2f;
	[SerializeField]
	bool ReverseDrag = true;
	
	private Vector3 _DragOrigin;
	private Vector3 _Move;
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(2))
		{
			_DragOrigin = Input.mousePosition;
			//return;
		}

		if(!Input.GetMouseButton(2)) return;
		
		if(ReverseDrag)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _DragOrigin);
			_Move = new Vector3(pos.y * DragSpeed, 0, pos.x * DragSpeed);
		}
		else
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition + _DragOrigin);
			_Move = new Vector3(pos.y * -DragSpeed, 0, pos.x * -DragSpeed);
		}
		
		transform.Translate(_Move, Space.World);
	}
}
