using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerAlejandro : MonoBehaviour {

	public float maxSpeed = 6f;
	public float jumpForce1 = 1000f;
	public float jumpForce2 = 500f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float verticalSpeed = 20;
	[HideInInspector]
	public bool lookingRight = true;
	bool doubleJump = false;
	public GameObject Boost;
	private Animator cloudanim;
	public GameObject Cloud;
	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isGrounded = false;
	private float positionx;
	private float positiony;
	private float tiempo;
	public int tempo = 120;
	[FMODUnity.EventRef]
	public string MusicaEvento;
	[FMODUnity.EventRef]
	public string Bombo1;
	[FMODUnity.EventRef]
	public string Bombo2;
	[FMODUnity.EventRef]
	public string Redoblante1;
	[FMODUnity.EventRef]
	public string Redoblante2;
	[FMODUnity.EventRef]
	public string Melodia1;
	[FMODUnity.EventRef]
	public string Melodia2;
	[FMODUnity.EventRef]
	public string Melodia1ConFinal;
	[FMODUnity.EventRef]
	public string Melodia2UnaVez;
	FMOD.Studio.EventInstance musica;
	FMOD.Studio.EventInstance bombo1;
	FMOD.Studio.EventInstance bombo2;
	FMOD.Studio.EventInstance redo1;
	FMOD.Studio.EventInstance redo2;
	FMOD.Studio.EventInstance melodia1;
	FMOD.Studio.EventInstance melodia2;
	FMOD.Studio.EventInstance melodia1confinal;
	FMOD.Studio.EventInstance melodia2unavez;
	private float ccactivo = 0;
	private float bbactivo = 0;
	public int puntaje = 0;
	public Text score;
	private int estadopuntaje = 0;
	private int tipobombo = 1;
	private int tiporedo = 1;
	private int tipomel1 = 1;
	private int tipomel2 = 1;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		Cloud = GameObject.Find("Cloud");
		tiempo = Time.deltaTime;
		musica = FMODUnity.RuntimeManager.CreateInstance(MusicaEvento);
		bombo1 = FMODUnity.RuntimeManager.CreateInstance(Bombo1);
		bombo2 = FMODUnity.RuntimeManager.CreateInstance(Bombo2);
		redo1 = FMODUnity.RuntimeManager.CreateInstance(Redoblante1);
		redo2 = FMODUnity.RuntimeManager.CreateInstance(Redoblante2);
		melodia1 = FMODUnity.RuntimeManager.CreateInstance(Melodia1);
		melodia2 = FMODUnity.RuntimeManager.CreateInstance(Melodia2);
		melodia1confinal = FMODUnity.RuntimeManager.CreateInstance(Melodia1ConFinal);
		melodia2unavez = FMODUnity.RuntimeManager.CreateInstance(Melodia2UnaVez);

		MidiBridge.instance.Warmup ();
	}
	void OnCollisionEnter2D(Collision2D collision2D) {
		if (collision2D.relativeVelocity.magnitude > 20){
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Intro") {
			musica.start ();
			musica.setParameterValue ("Parte", 0.2f);
		}
		if (other.tag == "Parte1") {
			musica.setParameterValue ("Parte", 1.2f);
		}
		if (other.tag == "Parte2") {
			musica.setParameterValue ("Parte", 2.2f);
		}
		if (other.tag == "Parte3") {
			musica.setParameterValue ("Parte", 4.2f);
		}
		if (other.tag == "Parte4") {
			musica.setParameterValue ("Parte", 5.2f);
		}
		if (other.tag == "Parte5") {
			musica.setParameterValue ("Parte", 7.2f);
			tipobombo = 2;
			tiporedo = 2;
		}
		if (other.tag == "Parte6") {
			musica.setParameterValue ("Parte", 8.2f);
			tipomel1 = 2;
			tipomel2 = 2;
		}
		if (other.tag == "Parte7") {
			musica.setParameterValue ("Parte", 9.2f);
		}
		if (other.tag == "Parte8") {
			musica.setParameterValue ("Parte", 10.2f);
		}
		if (other.tag == "ColliderBarrilAbajo") {
			puntaje -= 50;
		}
		if (other.tag == "ColliderBarrilArriba") {
			
		}
		if (other.tag == "Bombo") {
			//rb2d.AddForce(new Vector2(0,jumpForce2),ForceMode2D.Force);
			//StartCoroutine (Bombo1c());
			estadopuntaje = 1;
		}
		if (other.tag == "Redoblante") {
			//rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			//StartCoroutine (Redo1c ());
			estadopuntaje = 1;
		}
		if (other.tag == "Melodia1") {
			//transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);
			//StartCoroutine (Melodia1c ());
			estadopuntaje = 1;
		}
		if (other.tag == "Muerte") {
			puntaje = 0;
			musica.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			melodia1.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			melodia2.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			melodia1confinal.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			melodia2unavez.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			transform.localScale = new Vector3 (1f, 1f);
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			StartCoroutine (check());
			tipobombo = 1;
			tiporedo = 1;
			tipomel1 = 1;
			tipomel2 = 1;
		}
		if (other.tag == "Melodia1Fin") {
			transform.localScale = new Vector3 (1f, 1f);
		}
		if (other.tag == "Melodia2") {
			//transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			//rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			//StartCoroutine ( Melodia2c ());
			estadopuntaje = 1;
		}
		if (other.tag == "Melodia2Fin") {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		}	
		if (other.tag == "Bombo2") {
			//rb2d.AddForce(new Vector2(0,jumpForce2),ForceMode2D.Force);
			//StartCoroutine (Bombo2c ());
			estadopuntaje = 1;
		}
		if (other.tag == "Redoblante2") {
			//rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			//StartCoroutine (Redo2c ());
			estadopuntaje = 1;
		}
		if (other.tag == "Melodia1f") {
			//transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);
			//StartCoroutine (Melodia1finalc ());
			estadopuntaje = 1;
		}
		if (other.tag == "Melodia2u") {
			//transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			//rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			StartCoroutine ( Melodia2vezc ());
			estadopuntaje = 1;
		}
		if (other.tag == "Parar") {
			rb2d.velocity = new Vector2 (0, 0);
		}
		if (other.tag == "Medio") {
			estadopuntaje = 2;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Bombo") {
			estadopuntaje = 0;
		}
		if (other.tag == "Redoblante") {
			estadopuntaje = 0;
		}
		if (other.tag == "Melodia1") {
			estadopuntaje = 0;
		}
		if (other.tag == "Melodia2") {
			estadopuntaje = 0;
		}
		if (other.tag == "Bombo2") {
			estadopuntaje = 0;
		}
		if (other.tag == "Redoblante2") {
			estadopuntaje = 0;
		}
		if (other.tag == "Melodia1f") {
			estadopuntaje = 0;
		}
		if (other.tag == "Melodia2u") {
			estadopuntaje = 0;
		}
	}
	private bool estadodeinicio = false;
	public bool estadosalto = false;
	public Vector2 velocity;
	public float posicionYfija = 5;
	public float tamanonuevo = 0.5F;
	// Update is called once per frame
	void Update () {
		var zz = MidiInput.GetKey (MidiChannel.Ch1, 60);
		var xx = MidiInput.GetKey (MidiChannel.Ch1, 61);
		var cc = MidiInput.GetKey (MidiChannel.Ch1, 62);
		var bb = MidiInput.GetKey (MidiChannel.Ch1, 64);
		tiempo += Time.deltaTime;
		float multiplicador = tempo / 60;
		int contador = (int) (tiempo * multiplicador) + 1;
		if (contador > 3) {
			Multiplicador2 = 1.0f;
			if (estadodeinicio == false)
				
				estadodeinicio = true;
		}
		if (((zz*127)>20)  && (isGrounded || !doubleJump) && (!estadosalto))
		{
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			estadosalto = true;
			if (estadopuntaje == 1) {
				puntaje += 100;
			}
			if (estadopuntaje == 2) {
				puntaje += 50;
			}
			if (estadopuntaje == 0) {
				puntaje -= 100;
			}
			if (tiporedo == 1) {
				StartCoroutine (Redo1c());
			}
			if (tiporedo == 2) {
				StartCoroutine (Redo2c());
			}
		}
		if (((zz*127)<20))
			estadosalto = false;
		if (((xx*127)>20) && (isGrounded || !doubleJump) && (!estadosalto))
		{
			rb2d.AddForce(new Vector2(0,jumpForce2));
			estadosalto = true;
			if (estadopuntaje == 1) {
				puntaje += 100;
			}
			if (estadopuntaje == 2) {
				puntaje += 50;
			}
			if (estadopuntaje == 0) {
				puntaje -= 100;
			}
			if (tiporedo == 1) {
				StartCoroutine (Bombo1c());
			}
			if (tiporedo == 2) {
				StartCoroutine (Bombo2c());
			}
		}
		if (((xx*127)<20))
			estadosalto = false;
		positionx = rb2d.position.x;
		positiony = rb2d.position.y;
		if (((cc*127)>20)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if (estadopuntaje == 1) {
				puntaje += 100;
			}
			if (estadopuntaje == 2) {
				puntaje += 50;
			}
			if (estadopuntaje == 0) {
				puntaje -= 100;
			}
			if (tipomel2 == 1) {
				StartCoroutine (Melodia2c());
			}
			if (tipomel2 == 2) {
				StartCoroutine (Melodia2vezc());
			}
		}
		if ((bb*127)>20){
			transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);
			if (estadopuntaje == 1) {
				puntaje += 100;
			}
			if (estadopuntaje == 2) {
				puntaje += 50;
			}
			if (estadopuntaje == 0) {
				puntaje -= 100;
			}
			if (tipomel1 == 1) {
				StartCoroutine (Melodia1c());
			}
			if (tipomel1 == 2) {
				StartCoroutine (Melodia1finalc());
			}
		}
		float ultimoz = 0;
		float ultimox = 0;
		/*if ((zz*127) > 20 && zz > ultimoz){
			StartCoroutine (saltar ());
			ultimoz = zz;
		}
		if ((xx*127) > 20 && xx > ultimox)
		{
			StartCoroutine (saltar2 ());
			//ultimox = xx;
		}
		/*if (((127*cc) > 20) && ccactivo == 0) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			ccactivo = 1;
		}
		if (((127*cc) > 20) && ccactivo == 1) {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		if (((127*bb) > 20) && bbactivo == 0){
			transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);
		}
		if (((127*bb) > 20) && bbactivo == 1) {
			transform.localScale = new Vector3 (1f, 1f);
		}*/

		score.text = puntaje.ToString ();
	}
	public float Velocidad = 1.0f;
	public float Multiplicador2 = 0f;
	void FixedUpdate()
	{
		if (isGrounded) 
			doubleJump = false;

		float hor = 0.5f;
		anim.SetFloat ("Speed", Mathf.Abs (hor));
		rb2d.velocity = new Vector2 (Multiplicador2 * Velocidad * maxSpeed, rb2d.velocity.y);
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15F, whatIsGround);
		anim.SetBool ("IsGrounded", isGrounded);
		if ((hor > 0 && !lookingRight)||(hor < 0 && lookingRight))
			Flip ();

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}
	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
	IEnumerator Bombo1c() {
		bombo1.start ();
		yield return new WaitForSeconds(1);
		bombo1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Bombo2c() {
		bombo2.start ();
		yield return new WaitForSeconds(1);
		bombo2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Redo1c() {
		redo1.start ();
		yield return new WaitForSeconds(1);
		redo1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Redo2c() {
		redo2.start ();
		yield return new WaitForSeconds (1);
		redo2.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Melodia1c() {
		melodia1.start ();
		yield return new WaitForSeconds(20);
		melodia1.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Melodia2c() {
		melodia2.start ();
		yield return new WaitForSeconds (20);
		melodia2.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Melodia1finalc() {
		melodia1confinal.start ();
		yield return new WaitForSeconds(20);
		melodia1confinal.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	IEnumerator Melodia2vezc() {
		melodia2unavez.start ();
		yield return new WaitForSeconds (20);
		melodia2unavez.stop (FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}
	public GameObject checkpoint1;
	IEnumerator check () {
		yield return new WaitForSeconds (1);
		transform.position = new Vector3 (checkpoint1.transform.position.x, checkpoint1.transform.position.y);
	}
	IEnumerator esperar() {
		yield return new WaitForSeconds (0.3f);
	}
	IEnumerator saltar(){
		if (true && (isGrounded || !doubleJump) && (!estadosalto))
		{
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			estadosalto = true;
		}
		yield return new WaitForSeconds (0.1f);
		if (true)
			estadosalto = false;
		}
	IEnumerator saltar2(){
		if (true && (isGrounded || !doubleJump) && (!estadosalto))
		{
			rb2d.AddForce(new Vector2(0,jumpForce2),ForceMode2D.Force);
			estadosalto = true;
		}
		yield return new WaitForSeconds (1);
		if (true)
			estadosalto = false;
		}

}