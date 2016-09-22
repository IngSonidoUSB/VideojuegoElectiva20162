using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {
	 
	public Text timertext;
	private float tiempo;
	public int tempo = 90;
	public int compas = 4;
	// Use this for initialization
	void Start () {
		tiempo = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		tiempo += Time.deltaTime;
		/*float multiplicador = tempo / 60;
		int contador = (int) (tiempo * multiplicador) + 1;
		timertext.text = contador.ToString ();
		if (contador == (compas+1)) {
			
			tiempo = 0;
		}*/

	
	}
}
