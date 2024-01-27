using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


enum States { 
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

    private States state; 
    // Start is called before the first frame update
    void Start()
    {
        state = States.stalking;    
        agent.enabled = true;   
        agent.destination = getRandomWorldPoint();
    }

     Vector3 getRandomWorldPoint()
    {
        
        Vector3 RandWorldPoint = new Vector3(Random.Range( -15, 15), transform.position.y, Random.Range(-15, 15));

        NavMeshHit hit; 

        NavMesh.SamplePosition(RandWorldPoint, out hit, Mathf.Infinity, NavMesh.AllAreas);
        Debug.Log(hit.position);
        return hit.position;
    }
    // Update is called once per frame
    void Update()
    {

        if(agent.velocity == Vector3.zero)
        {
            agent.destination = getRandomWorldPoint();
        }
        //agent.destination = Player.position;
        
        //Search();

        animator.SetInteger("State", (int)state);
        //switch (state)
        //{
        //    case States.stalking:   
        //        break; 
            
        //    case States.chasing:
        //        break; 

        //    case States.attacking:
        //        break;
        //    default:
        //        break;

        //}

    }


    private void Search()
    {
        if(CanSeePlayer())
        {
            state = States.chasing;
            return; 
        }
    }

    private bool CanSeePlayer()
    {
        Collider[] hitColliders = Physics.OverlapBox(EyeLinePos.position, new Vector3((visionWidth / 2), (10 / 2), (visionDepth / 2)), transform.rotation, PlayerLayerMask);
        int i = 0;
        Debug.Log(hitColliders.Length);
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


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawCube(EyeLinePos.position, new Vector3(visionWidth, 10, visionDepth));
    //}

}
