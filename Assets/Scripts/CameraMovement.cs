using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public static float x=0.0f;
	public static float y=0.0f;
	void Start () {
		x = transform.position.x;
		y = transform.position.y;
	}

	void Update () {
		position();
	}

	void position()
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3(x,y ,-10), 8* Time.deltaTime);
	}

	public void moveRight()
	{
		x = transform.position.x + 480;
	}

	public void moveLeft()
	{
		x = transform.position.x - 480;
	}

	public void moveDown()
	{
		y = transform.position.y - 854;
	}

	public void downTwice()
	{
		y = transform.position.y - 1708;
	}

	public void moveUp()
	{
		y = transform.position.y + 854;
	}
}


