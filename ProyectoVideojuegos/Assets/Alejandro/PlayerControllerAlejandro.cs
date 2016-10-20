using UnityEngine;
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
	FMOD.Studio.EventInstance musica;
	FMOD.Studio.EventInstance bombo1;
	FMOD.Studio.EventInstance bombo2;
	FMOD.Studio.EventInstance redo1;
	FMOD.Studio.EventInstance redo2;
	FMOD.Studio.EventInstance melodia1;
	FMOD.Studio.EventInstance melodia2;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//cloudanim = GetComponent<Animator>();

		Cloud = GameObject.Find("Cloud");
		//cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();
		tiempo = Time.deltaTime;

		musica = FMODUnity.RuntimeManager.CreateInstance(MusicaEvento);
		bombo1 = FMODUnity.RuntimeManager.CreateInstance(Bombo1);
		bombo2 = FMODUnity.RuntimeManager.CreateInstance(Bombo2);
		redo1 = FMODUnity.RuntimeManager.CreateInstance(Redoblante1);
		redo2 = FMODUnity.RuntimeManager.CreateInstance(Redoblante2);
		melodia1 = FMODUnity.RuntimeManager.CreateInstance(Melodia1);
		melodia2 = FMODUnity.RuntimeManager.CreateInstance(Melodia2);


	}


	void OnCollisionEnter2D(Collision2D collision2D) {

		if (collision2D.relativeVelocity.magnitude > 20){
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");	



		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Intro") {
			musica.start ();
			rb2d.AddForce(new Vector2(0,jumpForce2),ForceMode2D.Force);
			StartCoroutine (Bombo1c());
		}
		if (other.tag == "Parte1") {
			musica.setParameterValue ("Parte", 1.2f);
		}
		if (other.tag == "Parte2") {
			musica.setParameterValue ("Parte", 3.0f);
		}
		if (other.tag == "Parte3") {
			musica.setParameterValue ("Parte", 4.0f);
		}
		if (other.tag == "Parte4") {
			musica.setParameterValue ("Parte", 5.0f);
		}
		if (other.tag == "Parte5") {
			musica.setParameterValue ("Parte", 6.0f);
		}
		if (other.tag == "Parte6") {
			musica.setParameterValue ("Parte", 7.0f);
		}
		if (other.tag == "Parte7") {
			musica.setParameterValue ("Parte", 8.0f);
		}
		if (other.tag == "Parte8") {
			musica.setParameterValue ("Parte", 9.0f);
		}
		if (other.tag == "Parte9") {
			musica.setParameterValue ("Parte", 10.0f);
		}
		if (other.tag == "Outro") {
			musica.setParameterValue ("Parte", 11.0f);
		}
		if (other.tag == "ColliderBarrilAbajo") {
			Debug.Log ("abajo");
		}
		if (other.tag == "ColliderBarrilArriba") {
			Debug.Log ("arriba");
		}
		if (other.tag == "Bombo") {
			rb2d.AddForce(new Vector2(0,jumpForce2),ForceMode2D.Force);
			StartCoroutine (Bombo1c());
		}
		if (other.tag == "Redoblante") {
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
		}
		if (other.tag == "cp1") {
			StartCoroutine (check ());
		}
	}


	private bool estadodeinicio = false;
	public bool estadosalto = false;
	public Vector2 velocity;
	public float posicionYfija = 5;
	public float tamanonuevo = 0.5F;

	// Update is called once per frame
	void Update () {
		tiempo += Time.deltaTime;
		float multiplicador = tempo / 60;
		int contador = (int) (tiempo * multiplicador) + 1;

		if (contador > 3) {
			Multiplicador2 = 1.0f;
			if (estadodeinicio == false)
				
				estadodeinicio = true;
		}
		if (Input.GetKeyDown(KeyCode.Z) && (isGrounded || !doubleJump) && (!estadosalto))
		{
			//rb2d.AddForce(new Vector2(0,jumpForce));
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);

			estadosalto = true;
			/*if (!doubleJump && !isGrounded)
			{
				doubleJump = false;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}*/
		}

		if (Input.GetKeyUp(KeyCode.Z))
			estadosalto = false;
		
		if (Input.GetKeyDown(KeyCode.X) && (isGrounded || !doubleJump) && (!estadosalto))
		{
			//rb2d.AddForce(new Vector2(0,jumpForce));
			rb2d.AddForce(new Vector2(0,jumpForce2));

			estadosalto = true;
			/*if (!doubleJump && !isGrounded)
			{
				doubleJump = false;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}*/
		}

		if (Input.GetKeyUp(KeyCode.X))
			estadosalto = false;
		
		positionx = rb2d.position.x;
		positiony = rb2d.position.y;

		if (Input.GetKeyDown (KeyCode.C)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}
		//rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
		//rb2d.position = new Vector2(positionx,positiony+10);
		if (Input.GetKeyUp (KeyCode.C)) {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		}


		if (Input.GetKeyDown (KeyCode.V)){

			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}
		//rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
		//rb2d.position = new Vector2(positionx,positiony+10);
		if (Input.GetKeyUp (KeyCode.V)) {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		if (Input.GetKeyDown (KeyCode.B)){

			transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);
		}
		//rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
		//rb2d.position = new Vector2(positionx,positiony+10);
		if (Input.GetKeyUp (KeyCode.B)) {
			transform.localScale = new Vector3 (1f, 1f);
		}


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

	public GameObject checkpoint1;

	IEnumerator check () {
		yield return new WaitForSeconds (2);
		transform.position = new Vector3 (checkpoint1.transform.position.x, checkpoint1.transform.position.y);
	}
}