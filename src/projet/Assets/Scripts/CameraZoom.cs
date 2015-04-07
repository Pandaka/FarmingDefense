using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
	[SerializeField]
	float ZoomSpeed = 1f;
	[SerializeField]
	int ScrollWheelLimit = 20;
	
	// Private
	private int _ScrollWheelminPush;
	private int _ScrollCount;

	void Start(){
		_ScrollWheelminPush = 0;
		_ScrollCount = 10;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if(_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
			{
				camera.transform.position += new Vector3(0, ZoomSpeed, 0);
				_ScrollCount++;
			}
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if(_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
			{
				camera.transform.position -= new Vector3(0, ZoomSpeed, 0);
				_ScrollCount--;
			}
		}
	}
}