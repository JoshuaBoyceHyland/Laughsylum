
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
enum EnemyStates { 
stalking = 0,
chasing = 1, 
attacking = 2
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;    
    [SerializeField] Animator animator;
    [SerializeField] Transform Player;
    [SerializeField] LayerMask PlayerLayerMask;
    [SerializeField] Transform EyeLinePos;
    [SerializeField] Transform worldSize; 
    [SerializeField] float visionWidth;
    [SerializeField] float visionDepth;

    GameObject righthand;

    private EnemyStates state; 
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.stalking;    
        agent.enabled = true;   
        agent.destination = getRandomWorldPoint();

        AttackRange attackRange = GetComponentInChildren<AttackRange>();

        attackRange.playerAttackAble += playerReadyToBeAttacked;

        righthand = GameObject.Find("mixamorig:RightHand");

        righthand.GetComponent<HandHitBox>().playerHit += playerDamage;



        
    }

     Vector3 getRandomWorldPoint()
    {
        
        Vector3 RandWorldPoint = new Vector3(Random.Range( -15, 15), transform.position.y, Random.Range(-15, 15));

        NavMeshHit hit; 

        NavMesh.SamplePosition(RandWorldPoint, out hit, Mathf.Infinity, NavMesh.AllAreas);
      
        return hit.position;
    }
    // Update is called once per frame
    void Update()
    {

        
        
        //Search();

        animator.SetInteger("State", (int)state);
        switch (state)
        {
            case EnemyStates.stalking:
                if (agent.velocity == Vector3.zero)
                {
                    agent.destination = getRandomWorldPoint();
                }

                if (CanSeePlayer())
                {
                    state = EnemyStates.chasing;
                    agent.destination = Player.position;
                    agent.speed = 3.5f; 
                }

                break;

            case EnemyStates.chasing:
             
                agent.destination = Player.position;
                if (!CanSeePlayer())
                {
                    StartCoroutine(LosePlayer());
                }
                break;

            case EnemyStates.attacking:
                FacePlayer();
                break;
            default:
                break;

        }

    }


    private void playerDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private IEnumerator LosePlayer()
    {


        yield return new WaitForSeconds(1.5f);

        if (!CanSeePlayer())
        {
            state = EnemyStates.stalking;
            agent.speed = 2; 
        }
    }
    private bool CanSeePlayer()
    {
        Collider[] hitColliders = Physics.OverlapBox(EyeLinePos.position, new Vector3((visionWidth / 2), (10 / 2), (visionDepth / 2)), transform.rotation, PlayerLayerMask);
        int i = 0;
        
        while (i < hitColliders.Length)
        {


            if (hitColliders[i].tag == "Player")
            {
                return true;
            }
            i++;

        }

        return false; 
    }

    private void playerReadyToBeAttacked()
    {
        state = EnemyStates.attacking;
        agent.enabled = false; 
    }


    private void FacePlayer()
    {
        Vector3 directionToTarget = Player.position - transform.position;

        directionToTarget.y = 0; 

        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    public void AttackFinished()
    {
        state = EnemyStates.chasing;
        agent.speed = 3.5f;
        agent.enabled = true;
        righthand.active = true;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawCube(EyeLinePos.position, new Vector3(visionWidth, 10, visionDepth));
    //}

}
