using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crawlerNavTest : MonoBehaviour
{
    NavMeshAgent nm; //set monster(s) navmeshagent here
    public Transform target; //set player here

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nm.SetDestination(target.position);
    }
}
