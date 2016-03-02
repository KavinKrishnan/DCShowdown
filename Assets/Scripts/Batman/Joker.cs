using UnityEngine;
using System.Collections;

public class Joker : MonoBehaviour {


	public int JHealth = 10;
	public float moveSpeed = 2f;
	public bool grounded = true;
	public bool rightDir = true;
	public bool canDJump = false;
	private bool canMove= false;
	private float hVal = -1.0f;
	
	// Use this for initialization
	void OnGUI () {

		string livess = "Joker Health: " + JHealth;
		GUI.Label (new Rect (25,20, 200, 50), livess);
		
	}

	
	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = false;
		}


		if (col.gameObject.tag == "Batman") {
			if (Input.GetKeyDown (KeyCode.T)) {
				JHealth--;
			}
		}

	}
	
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = true;
		}

		if (col.gameObject.tag == "Batman") {
			if (Input.GetKeyDown (KeyCode.T)) {
				JHealth--;
			}
		}

	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "ground")
		{
			grounded = true;
		}

		if (col.gameObject.tag == "Batman") {
			if (Input.GetKeyDown (KeyCode.T)) {
				JHealth--;
			}
		}

	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Right",false);
		anim.SetBool("Idle",false);
		anim.SetBool("Punch",false);
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			
			hVal = -1.0f;
			
			canMove = true;
			
			anim.SetBool("Right",true);
			MoveRight();
			
			
			if(!rightDir)
			{
				transform.Rotate (Vector3.up * 180);
				moveSpeed*=-1;
			}
			
			
			rightDir = true;
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			
			
			hVal = 1.0f;
			
			canMove = true;
			
			anim.SetBool("Right",true);
			MoveRight();
			
			if(rightDir)
			{
				transform.Rotate (Vector3.up * 180);
				moveSpeed*=-1;
			}
			
			rightDir = false;
		}
		else if (Input.GetMouseButtonDown(0)) {
			
			anim.SetBool("Punch",true);
			PunchRight();
			
		}
		else if ((Input.GetKeyUp (KeyCode.RightArrow))||(Input.GetKeyUp (KeyCode.LeftArrow))||(Input.GetMouseButtonUp(0))) {
			
			canMove = false;
			
			anim.SetBool("Idle",true);
			anim.SetBool("Right",false);
			IdleRight();
		}
		
		
		if (!grounded) {	
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, -1), ForceMode2D.Impulse);
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
		anim.SetBool("Right",true);
	}
	
	void IdleRight(){
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Idle",true);
	}
	
	void PunchRight(){
		Animator anim = GetComponent <Animator>();
		anim.SetBool("Punch",true);
	}
	
	
}
