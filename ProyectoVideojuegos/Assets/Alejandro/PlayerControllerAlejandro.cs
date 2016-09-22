using UnityEngine;
using System.Collections;

public class PlayerControllerAlejandro : MonoBehaviour {

	public float maxSpeed = 6f;
	public float jumpForce = 1000f;
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

	private float tiempo;
	public int tempo = 120;
	[FMODUnity.EventRef]
	public string MusicaEvento;

	FMOD.Studio.EventInstance musica;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//cloudanim = GetComponent<Animator>();

		Cloud = GameObject.Find("Cloud");
		//cloudanim = GameObject.Find("Cloud(Clone)").GetComponent<Animator>();
		tiempo = Time.deltaTime;

		musica = FMODUnity.RuntimeManager.CreateInstance(MusicaEvento);

	}


	void OnCollisionEnter2D(Collision2D collision2D) {

		if (collision2D.relativeVelocity.magnitude > 20){
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//	cloudanim.Play("cloud");	



		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Intro")
		musica.start ();
	}

	private bool estadodeinicio = false;


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
		if (Input.GetButtonDown("Jump") && (isGrounded || !doubleJump))
		{
			rb2d.AddForce(new Vector2(0,jumpForce));

			if (!doubleJump && !isGrounded)
			{
				doubleJump = true;
				Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
				//	cloudanim.Play("cloud");		
			}
		}


		if (Input.GetButtonDown("Vertical") && !isGrounded)
		{
			rb2d.AddForce(new Vector2(0,-jumpForce));
			Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
			//cloudanim.Play("cloud");
		}

		if (Input.GetKeyDown(KeyCode.A)) 
			musica.setParameterValue("Parte", 1.1f);

		if (Input.GetKeyDown(KeyCode.S)) 
			musica.setParameterValue("Parte", 2.1f);

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




}