using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private Animator anim;
	private Rigidbody rb;

	private GameObject[] all_monsters_alive;
	private float currentJumpCooldown = -1.0f;

	public float cooldownAfterEachJump = 5.0f;

	public float speed = 75.0f;
	public float turnSpeed = 400.0f;
	public float gravity = 1.0f;

	public float currentHealth = 100.0f;
	public float maxHealth = 100.0f;
	public bool isDead = false;
    public float canMove = 0;

	public int score = 0;
	public int currentCombo = 0;
	public int highestCombo = 0;

	public HealthBar healthBar;

	private float lastHit = 0.0f;


	void Start ()
	{
		anim = gameObject.GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
		currentHealth = 100.0f;
		score = 0;
		highestCombo = 0;
	}

	void checkIsDead()
	{
		if(currentHealth <= 0.0f)
		{
			isDead = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
		}
	}

	void Update ()
	{
		checkIsDead();
		if(!isDead && canMove < Time.time)
		{
			all_monsters_alive = GameObject.FindGameObjectsWithTag("Enemy");

			if (Input.GetKey ("space")) {
				anim.SetInteger ("AnimationPar", 2);
			}else if (Input.GetKey("w")){
				anim.SetInteger ("AnimationPar", 1);
			}else{
				anim.SetInteger ("AnimationPar", 0);
			}

			PlayerMovement();
			isAboveMonster();
		}
	}

	void FixedUpdate ()
	{
		if(!isDead && canMove < Time.time)
		{
			if (Input.GetKeyDown("x"))
			{
				savePlayer();
				saveEnemies();
			}
			else if (Input.GetKeyDown("c"))
			{
				loadPlayer();
				loadEnemies();
			}
		}

	}

	void PlayerMovement()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		if(isGrounded() && Input.GetKeyDown("space") && Time.time > currentJumpCooldown){
			rb.AddForce(0, 10, 0, ForceMode.Impulse);
			currentJumpCooldown = Time.time + cooldownAfterEachJump;
		} else if (!isGrounded()) {
			if (currentCombo > highestCombo) {
				highestCombo = currentCombo;
			}
		} else if (isGrounded()){
			currentCombo = 0;
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical) * speed * Time.deltaTime;

		transform.Translate(movement,Space.Self);
	}

    private bool isGrounded()
    {
		RaycastHit rh;
		bool hasHit = Physics.Raycast(transform.position, -Vector3.up, out rh, 0.2f);
		if (hasHit && !rh.collider.gameObject.CompareTag("Enemy")) {
			return true;
		}
		return false;
    }

	private bool checkPlayerOnTop(float playerX, float playerY, float playerZ) {
        if (Mathf.Abs(playerX - transform.position.x) < 1.75f && Mathf.Abs(playerZ - transform.position.z) < 1.75f) {
            if (Mathf.Abs(playerY - transform.position.y) < 3f && Mathf.Abs(playerY - transform.position.y) > 0.5f) {
                return true;
            }
        }
        return false;
    }


	private void isAboveMonster()
	{
		RaycastHit rh;
		bool hasHit = Physics.Raycast(transform.position, -Vector3.up, out rh, 0.25f);

		if(hasHit && rh.collider.gameObject.CompareTag("Enemy") && Time.time > lastHit){
			currentCombo += 1;
			lastHit = Time.time + 1.0f;
			EnemyController monster = rh.collider.GetComponent<EnemyController>();
			monster.currentHealth -= 33.5f;
			if(Time.time > currentJumpCooldown)
				rb.AddForce(0, 15, 0, ForceMode.Impulse);
		}
	}

	public void takeDamage(float damage)
    {
		currentHealth -= damage;
    }

	public void savePlayer ()
	{
		SaveSystem.SavePlayer(this);
	}

	public void loadPlayer ()
	{
		PlayerData data = SaveSystem.LoadPlayer();

		currentHealth = data.health;

		Vector3 position;
		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		transform.position = position;
	}

	public void saveEnemies ()
	{
		SaveSystem.SaveEnemies(all_monsters_alive);
	}

	public void loadEnemies ()
	{
		EnemyData data = SaveSystem.LoadEnemies();

		for(int i = 0; i < data.nrOfEnemies; i++)
		{
			if (all_monsters_alive[i].GetComponent<EnemyController>().currentHealth <= 0.0f)
			{
				continue;
			}
			all_monsters_alive[i].GetComponent<EnemyController>().currentHealth = data.health[i];

			Vector3 position;
			position.x = data.position[3 * i];
			position.y = data.position[3 * i + 1];
			position.z = data.position[3 * i + 2];

			all_monsters_alive[i].transform.position = position;
			all_monsters_alive[i].GetComponent<EnemyController>().healthBar.setHealth(data.health[i]);
		}
	}
}
