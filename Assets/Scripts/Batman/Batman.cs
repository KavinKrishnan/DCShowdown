using UnityEngine;
using System.Collections;

public class Batman : MonoBehaviour {


	public float moveSpeed = 2f;
	public bool grounded = true;
	public bool rightDir = true;
	public bool canDJump = false;
	private bool canMove= false;
	private float hVal = 1.0f;

	// Use this for initialization

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = true;
		}


	}



	// Update is called once per frame
	void Update () {

		Animator anim = GetComponent <Animator>();
		anim.SetBool("Rightw",false);
		anim.SetBool("Idler",false);
		anim.SetBool("Punchr",false);

		if (Input.GetKeyDown (KeyCode.D)) {

			hVal = 1.0f;

			canMove = true;

			anim.SetBool("Rightw",true);
			MoveRight();


			if(!rightDir)
			{
				transform.Rotate (Vector3.up * 180);
				moveSpeed*=-1;
			}


			rightDir = true;
		}
		else if (Input.GetKeyDown (KeyCode.A)) {


			hVal = -1.0f;

			canMove = true;

			anim.SetBool("Rightw",true);
			MoveRight();

			if(rightDir)
			{
				transform.Rotate (Vector3.up * 180);
				moveSpeed*=-1;
			}

			rightDir = false;
		}
		else if (Input.GetKeyDown (KeyCode.T)) {
			
			anim.SetBool("Punchr",true);
			PunchRight();
			
		}
		else if ((Input.GetKeyUp (KeyCode.A))||(Input.GetKeyUp (KeyCode.D))||(Input.GetKeyUp (KeyCode.T))) {

			canMove = false;

			anim.SetBool("Idler",true);
			anim.SetBool("Rightw",false);
			IdleRight();
		}


		if (!grounded) {	
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, -2), ForceMode2D.Impulse);
			if (Input.GetKeyUp ("space") && canDJump) {
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0,13), ForceMode2D.Impulse);
				canDJump = false;
			} 
		} else {   
			canDJump = true;  
			if (Input.GetKeyUp ("space")) {
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, 7), ForceMode2D.Impulse);
			} 
		}


		if (canMove) {
				
			transform.Translate (hVal * moveSpeed * Time.deltaTime, 0, 0);
		}

	}

    void MoveRight(){
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Rightw",true);
	}

	void IdleRight(){
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Idler",true);
	}

	void PunchRight(){
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Punchr",true);
	}


}
