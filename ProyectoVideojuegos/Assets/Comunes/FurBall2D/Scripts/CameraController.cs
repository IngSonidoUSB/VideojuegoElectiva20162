using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform Player;
	public float m_speed = 0.1f;
	public GameObject background;
	public GameObject plataforma;
	Camera mycam;

	public void Start()
	{
		mycam = GetComponent<Camera> ();
	}

	public void Update()
	{

		mycam.orthographicSize = (Screen.height / 100f) / 0.7f;

		if (Player) 
		{
		
			transform.position = Vector3.Lerp(transform.position, Player.position, m_speed) + new Vector3(0, 0.03f, -12);
			background.transform.position =  new Vector3(transform.position.x, transform.position.y+5,5);
			plataforma.transform.position =  new Vector3(transform.position.x-40, -20,0);
		}
		 

	}
}
