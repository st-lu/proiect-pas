using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;
    private Animator anim;

    System.Random rnd = new System.Random();
    private bool goingRandom = false;
    private float randomDirectionX;
    private float randomDirectionY;
    private float doNotChangeDir = 0.0f;
    private float doNotChangeTime = 0.5f;
    private int direction = 0;
    
	public float currentHealth = 100.0f;
    public float maxHealth = 100.0f;
    public EnemyHealthBar healthBar;
    public bool isDead = false;
    public float speed = 0;
    private EnemyAttack enemyAttack;
    public GameObject healthBarCanvas;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Walk Forward", true);
        healthBar.setMaxHealth(maxHealth);
        enemyAttack = GetComponent<EnemyAttack>();
        speed = rnd.Next() % 3 + 4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            // roata towards target
            //transform.LookAt(new Vector3(Target.position.x,0,Target.position.z));

            //Moves towards the target while it is not above it.
            //If it is above it, the monster will try to dodge it and go in a the oposite direction.
            //TO DO: speed parameter does not work.
            if(Vector3.Distance(Target.position, transform.position) > 0.2f && !checkPlayerOnAbove(Target.position.x, Target.position.y, Target.position.z, true)) {
                if (Time.time > doNotChangeDir){
                    direction = 0;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x,0,Target.position.z), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(Target.position.x,0,Target.position.z));
                    goingRandom = false;
                    doNotChangeDir = Time.time + doNotChangeTime;
                }
                else if (direction == 0) {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x,0,Target.position.z), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(Target.position.x,0,Target.position.z));
                    goingRandom = false;
                } else {
                    if (!goingRandom) {
                                randomDirectionX = rnd.Next() % 720 - 360;
                                randomDirectionY = rnd.Next() % 720 - 360;
                                goingRandom = true;
                            }
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomDirectionX, 0, randomDirectionY), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(randomDirectionX, 0, randomDirectionY));
                }
            }
            else if (checkPlayerOnAbove(Target.position.x, Target.position.y, Target.position.z, true)) {
                if (Time.time > doNotChangeDir){ 
                    direction = 1;
                    if (!goingRandom) {
                        randomDirectionX = rnd.Next() % 720 - 360;
                        randomDirectionY = rnd.Next() % 720 - 360;
                        goingRandom = true;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomDirectionX, 0, randomDirectionY), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(randomDirectionX, 0, randomDirectionY));
                    doNotChangeDir = Time.time + doNotChangeTime;
                } 
                else if (direction == 0) {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x,0,Target.position.z), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(Target.position.x,0,Target.position.z));
                    goingRandom = false;
                } else {
                    if (!goingRandom) {
                                randomDirectionX = rnd.Next() % 720 - 360;
                                randomDirectionY = rnd.Next() % 720 - 360;
                                goingRandom = true;
                            }
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomDirectionX, 0, randomDirectionY), Time.fixedDeltaTime * speed);
                    transform.LookAt(new Vector3(randomDirectionX, 0, randomDirectionY));
                }
            }
        
            if (verifyDead()) {
                var player = GameObject.FindWithTag("Player").GetComponent<Player>();
                player.score += 1;
            }
        }
    }
    bool verifyDead()
    {
        if (currentHealth <= 0)  // Monster is dead :(
        {
            anim.SetTrigger("Die");
            enemyAttack.setCanAttack(false);
            enemyAttack.setPlayerInRange(false);
            anim.SetBool("Walk Forward", false);
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(killMonster());
            
            healthBarCanvas.SetActive(false);

            return true;
        }
        return false;
    }

    // Kill the monster :(
    IEnumerator killMonster()
    {
        isDead = true;
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().enabled = false;
        // Destroy(gameObject);
    }

    // This should check if the player is right above the monster.
    // If monster_call == true --> We check if monster should run from the player because player tries to jump on it.
    // Else --> We check if the player stepped on the monster.
    private bool checkPlayerOnAbove(float playerX, float playerY, float playerZ, bool monster_call = false) {
        var comparation_size_xz = 1.75f;
        if (monster_call) {
            comparation_size_xz = 8.0f;
        }
        if (Mathf.Abs(playerX - transform.position.x) < comparation_size_xz && Mathf.Abs(playerZ - transform.position.z) < comparation_size_xz) {
            if (monster_call) {
                if (Mathf.Abs(playerY - transform.position.y) > 1f) {
                    return true;
                }
            }
            else {
                if (Mathf.Abs(playerY - transform.position.y) < 3f && Mathf.Abs(playerY - transform.position.y) > 0.5f) {
                    return true;
                }
            }
        }
        return false;
    }
}
