using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float enemyCooldown = 1;
    public float damage = 10;

    private bool playerInRange = false;
    private bool canAttack = true;
    private Animator anim;

    // Player variables
    public Rigidbody playerRigidBody;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canAttack  && ((playerRigidBody.position.y)<1.5))
        {
            // Debug.Log("Attacking");
            anim.SetTrigger("Stab Attack");
            player.takeDamage(damage);
            StartCoroutine(AttackCooldown());

            var playerMove = GameObject.FindWithTag("Player").GetComponent<Player>();
            playerRigidBody.AddForce(transform.forward * 10, ForceMode.Impulse);
            playerMove.canMove += 2.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            anim.SetBool("Walk Forward", true);
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCooldown);
        canAttack = true;
    }

    public void setCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    public void setPlayerInRange(bool playerInRange)
    {
        this.playerInRange = playerInRange;
    }
}
