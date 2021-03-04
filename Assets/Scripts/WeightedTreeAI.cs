using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeightedTreeAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public bool inSight;
    public LayerMask lineMask;
    public float distance;
    public Vector3 directionToPlayer;
    public Vector3 playerMoveDirection;
    public Vector3 prevPlayerPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public bool LineOfSightCheck()
    {
        directionToPlayer = Vector3.Normalize(player.position - transform.position);

        float dot = Vector3.Dot(directionToPlayer, transform.forward);

        if (dot > 0.7 && !Physics.Linecast(transform.position, player.position, lineMask))
        {
            Debug.DrawLine(transform.position, player.position, Color.green);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public interface IDecision
{
    IDecision MakeDecision();
}

public class SeesPlayer : IDecision
{
    public IDecision MakeDecision()
    {
        // Check LoS then move to next node.
        return null;
    }
}

public class CheckDistance : IDecision
{
    public IDecision MakeDecision()
    {
        // Check distance then move to next node.
        return null;
    }
}

public class HasSeenPlayer : IDecision
{
    public IDecision MakeDecision()
    {
        // Check if prevPlayerPoint has been set and not reached then move to next node.
        return null;
    }
}
