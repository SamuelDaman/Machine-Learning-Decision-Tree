using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeightedTreeAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    //public bool inSight;
    public LayerMask lineMask;
    //public float distance;
    public float meleeRange = 3f;
    public Vector3 directionToPlayer;
    public Vector3 playerMoveDirection;
    public Vector3 prevPlayerPoint;

    public IDecision seesPlayer;
    public IDecision isInMeleeRange;
    public IDecision hasSeenPlayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        seesPlayer = new SeesPlayer(this);
        isInMeleeRange = new IsInMeleeRange(this);
        hasSeenPlayer = new HasSeenPlayer(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seesPlayer.MakeDecision();
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

    public bool DistanceCheck()
    {
        return Vector3.Distance(transform.position, player.position) <= meleeRange;
    }

    public void Melee()
    {
        // Initiate a melee attack.
    }

    public void Shoot()
    {
        // Shoot at the player.
    }
}

public interface IDecision
{
    IDecision MakeDecision();
}

public class SeesPlayer : IDecision
{
    public WeightedTreeAI parentScript;
    public SeesPlayer() { }

    public SeesPlayer(WeightedTreeAI script)
    {
        parentScript = script;
    }

    public IDecision MakeDecision()
    {
        // Check LoS then move to next node.
        return parentScript.LineOfSightCheck() ? parentScript.isInMeleeRange.MakeDecision() : parentScript.hasSeenPlayer.MakeDecision();
    }
}

public class IsInMeleeRange : IDecision
{
    WeightedTreeAI parentScript;
    public IsInMeleeRange() { }

    public IsInMeleeRange(WeightedTreeAI script)
    {
        parentScript = script;
    }

    public IDecision MakeDecision()
    {
        // Check distance then move to next node.
        if (parentScript.DistanceCheck())
        {
            parentScript.Melee();
        }
        else
        {
            parentScript.Shoot();
        }
        return null;
    }
}

public class HasSeenPlayer : IDecision
{
    WeightedTreeAI parentScipt;
    public HasSeenPlayer() { }

    public HasSeenPlayer(WeightedTreeAI script)
    {
        parentScipt = script;
    }

    public IDecision MakeDecision()
    {
        // Check if prevPlayerPoint has been set and not reached then move to next node.
        return null;
    }
}
