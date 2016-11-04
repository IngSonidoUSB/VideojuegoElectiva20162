using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControlerDaniel : MonoBehaviour {

public float maxSpeed = 1f;
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
public bool isGrounded = false;

	private float positionx;
	private float positiony;

	[FMODUnity.EventRef]
	public string MusicaEvento;
	public float cambio;


	public float cambio1=0.8f;
	public float cambio2=1.3f;
	public float cambio3=2.3f;
	public float cambio4=3.3f;	
	public float cambio5=4.3f;
	public float cambio6=5.3f;
	public float cambio7=6.3f;
	public float cambio8=7.3f;
	public float cambio9=8.3f;
	public float cambio10=9.3f;
	public float cambio11=10.3f;
	public float cambio12=11.3f;
	public int puntaje=0;
	public Text score;
	public int swt;

	FMOD.Studio.EventInstance musica;

	public GameObject check1;									
	public GameObject check2;

// Use this for initialization
void Start () {
	rb2d = GetComponent<Rigidbody2D>();
	anim = GetComponent<Animator>();
	//cloudanim = GetComponent<Animator>();
	Cloud = GameObject.Find("Cloud");
	//cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();

		musica = FMODUnity.RuntimeManager.CreateInstance(MusicaEvento);


	MidiBridge.instance.Warmup (); 
}


void OnCollisionEnter2D(Collision2D collision2D) {

	if (collision2D.relativeVelocity.magnitude > 20){
		Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
		//	cloudanim.Play("cloud");	

	}
}

	public bool estadosalto = false;
	public Vector2 velocity;
	public float posicionYfija = 0.5f;
	public float tamanonuevo = 0.5F;


	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "Intro") {
			musica.start ();
		}
		if (other.tag == "posintro") {
			cambio = cambio1;
			musica.setParameterValue ("control1", cambio);
		}
		if (other.tag == "A1") {
			cambio = cambio2;
			musica.setParameterValue ("control1", cambio);	

		}
		if (other.tag == "A2") {
			cambio = cambio3;
			musica.setParameterValue ("control1", cambio);

		}
		if (other.tag == "A3") {
			cambio = cambio4;
			musica.setParameterValue ("control1", cambio);

		}
		if (other.tag == "A4") {
			cambio = cambio5;
			musica.setParameterValue ("control1", cambio);

		}
		if (other.tag == "coro") {
			cambio = cambio6;
			musica.setParameterValue ("control1", cambio);

		}

		if (other.tag == "B1") {
			cambio = cambio7;
			musica.setParameterValue ("control1", cambio);	

		}
		if (other.tag == "B2") {
			cambio = cambio8;
			musica.setParameterValue ("control1", cambio);

		}
		if (other.tag == "B3") {
			cambio = cambio9;
			musica.setParameterValue ("control1", cambio);

		}
		if (other.tag == "B4") {
			cambio = cambio10;
			musica.setParameterValue ("control1", cambio);
		}
		if (other.tag == "coro2") {
			cambio = cambio11;
			musica.setParameterValue ("control1", cambio);
		}


		if (other.tag == "saltobajo") {
			//rb2d.AddForce(new Vector2(0,jumpForce2));
			swt = 1;

		}


		if (other.tag == "saltoalto") {
			//rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			swt = 1;
		}

		if (other.tag == "reducir") {
			//transform.localScale = new Vector3 (0.3f, 0.3f);
			swt = 1;
		}

		if (other.tag == "reducir2") {
			//transform.localScale = new Vector3 (1f, 1f);
			swt = 1;
		}

		if (other.tag == "mantener") {
			//transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			//rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			swt = 1;
		}

		if (other.tag == "mantener2") {
			//rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			swt = 1;
		}

		if (other.tag == "point50") {
			//rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			swt = 2;
		}
		if (other.tag == "seguimiento") {
			puntaje = 0;
		}

		if (other.tag == "seguimiento") {
			//Destroy(this);
			//Debug.Log("prueba");
			StartCoroutine(restart());
		}

		if (other.tag == "sawone") {
			//Destroy(this);
			//Debug.Log("prueba");
			StartCoroutine(restart());
		}


	}


	void OnTriggerExit2D(Collider2D other) {


		if (other.tag == "point50") {
			//rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			swt = 0;
		}

		if (other.tag == "saltobajo") {
			swt = 0;

		}


		if (other.tag == "saltoalto") {
			//rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			swt = 0;
		}

		if (other.tag == "reducir") {
			//transform.localScale = new Vector3 (0.3f, 0.3f);
			swt = 0;
		}

		if (other.tag == "reducir2") {
			//transform.localScale = new Vector3 (1f, 1f);
			swt = 0;
		}

		if (other.tag == "mantener") {
			//transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			//rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			swt = 0;
		}

		if (other.tag == "mantener2") {
			//rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
			swt = 0;
		}

	}




	private bool estadodeinicio = false;
// Update is called once per frame
void Update () {

	
		if (Input.GetKeyDown(KeyCode.Z) && (isGrounded || !doubleJump) && (!estadosalto))
		{
			//rb2d.AddForce(new Vector2(0,jumpForce));
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			if (swt == 1) {
				puntaje += 100;
			}

			if (swt == 2) {
				puntaje += 50;
			}
			estadosalto = true;
			//jumpForce1 = 0;
			/*if (!doubleJump && !isGrounded)
			{
				doubleJump = false;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}*/
		}

		if (Input.GetKeyUp(KeyCode.Z))
			estadosalto = false;
		//jumpForce1 = 1000f;

		if (Input.GetKeyDown(KeyCode.X) && (isGrounded || !doubleJump) && (!estadosalto))
		{
			//rb2d.AddForce(new Vector2(0,jumpForce));
			rb2d.AddForce(new Vector2(0,jumpForce2));
			if (swt == 1) {
				puntaje += 100;
			}

			if (swt == 2) {
				puntaje += 50;
			}
			estadosalto = true;
			//jumpForce2 = 0;
			/*if (!doubleJump && !isGrounded)
			{
				doubleJump = false;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}*/
		}

		if (Input.GetKeyUp(KeyCode.X))
			estadosalto = false;
		 //jumpForce2 = 500f;

		positionx = rb2d.position.x;
		positiony = rb2d.position.y;

		if (Input.GetKeyDown (KeyCode.C)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if (swt == 1) {
				puntaje += 100;
			}
			if (swt == 2) {
				puntaje += 50;
			}
		
		}
		//rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
		//rb2d.position = new Vector2(positionx,positiony+10);
		if (Input.GetKeyUp (KeyCode.C)) {
			rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;


		}



		if (Input.GetKeyDown (KeyCode.V)){

			transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);

			if (swt == 1) {
				puntaje += 100;
			}
			if (swt == 2) {
				puntaje += 50;
			}
		}
		//rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
		//rb2d.position = new Vector2(positionx,positiony+10);
		if (Input.GetKeyUp (KeyCode.V)) {
			transform.localScale = new Vector3 (1f, 1f);
		}


	if (Input.GetButtonDown("Vertical") && !isGrounded)
	{
		rb2d.AddForce(new Vector2(0,-jumpForce1));
		Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
		//cloudanim.Play("cloud");
	}


		var pingIn1 = MidiInput.GetKey (MidiChannel.Ch1, 60);
		if (((pingIn1*127) > 20) && (isGrounded || !doubleJump) && (!estadosalto)){
			rb2d.AddForce(new Vector2(0,jumpForce1),ForceMode2D.Force);
			estadosalto = true;		
			if (swt == 1) {
				puntaje += 100;
			}
			if (swt == 2) {
				puntaje += 50;
			}	
		}

		var pingIn2 = MidiInput.GetKey (MidiChannel.Ch1, 61);
		if (((pingIn2*127) > 20)  && (isGrounded || !doubleJump) && (!estadosalto))
		{
			//rb2d.AddForce(new Vector2(0,jumpForce));
			rb2d.AddForce(new Vector2(0,jumpForce2));
			if (swt == 1) {
				puntaje += 100;
			}

			if (swt == 2) {
				puntaje += 50;
			}
			estadosalto = true;
			//jumpForce2 = 0;
			/*if (!doubleJump && !isGrounded)
			{
				doubleJump = false;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");		
			}*/
		}

		var pingIn3 = MidiInput.GetKey (MidiChannel.Ch1, 62);
		if  ((pingIn3*127) > 20) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + posicionYfija, 0);
			rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			if (swt == 1) {
				puntaje += 100;
			}
			if (swt == 2) {
				puntaje += 50;
			}

		}

		var pingIn4 = MidiInput.GetKey (MidiChannel.Ch1, 63);
		if ((pingIn4*127) > 20) {

			transform.localScale = new Vector3 (tamanonuevo, tamanonuevo);

			if (swt == 1) {
				puntaje += 100;
			}
			if (swt == 2) {
				puntaje += 50;
			}
		}


		//Debug.Log (pingIn*127);

		score.text = puntaje.ToString();

}
public float Velocidad = 1.0f;



void FixedUpdate()
{
	if (isGrounded) 
		doubleJump = false;


	float hor = Input.GetAxis ("Horizontal");

	anim.SetFloat ("Speed", Mathf.Abs (hor));

		rb2d.velocity = new Vector2 (Velocidad* maxSpeed, rb2d.velocity.y);

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

	IEnumerator restart()
	{
		yield return new WaitForSeconds(0.2f);
		transform.position =  new Vector3(check1.transform.position.x, check1.transform.position.y);
	}

	IEnumerator point()
	{
		yield return new WaitForSeconds(0.2f);
		transform.position =  new Vector3(check1.transform.position.x, check1.transform.position.y);
	}


}

