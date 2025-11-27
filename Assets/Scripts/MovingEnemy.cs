using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MovingEnemy : MonoBehaviour
{
    public NavMeshAgent miGo;
    public float walkingSpeed = 5.5f; // Caminar Animación
    public float runningSpeed = 9.0f; // Correr Animación
    public Transform[] patrolAreas; // Lista de Areas a patrullar
    public float distanceDestinies; // Distancia del enemigo y el primer Area
    private int patrols; // Contador
    private Animator animator;
    public float idleTime = 2.0f;
    public float idleTimer;

    public AudioSource imMoving;


    public enum ENEMY_STATE
    {
        Idle,
        Walking,
        Running
    }
    public ENEMY_STATE currentState;

    void Start()
    {

        currentState = ENEMY_STATE.Idle;
        animator = GetComponent<Animator>();

        if (miGo == null || animator == null)
        {
            enabled = false;
        }

        miGo.speed = walkingSpeed;
        distanceDestinies = Vector3.Distance(transform.position, patrolAreas[patrols].position);
        miGo.destination = patrolAreas[patrols].position;
        imMoving = GetComponent<AudioSource>();
        WalkingState();
    }


    public void Update()
    {
        float speed = miGo.velocity.magnitude;
        animator.SetFloat("Speed", speed);
        switch (currentState)
        {
            case ENEMY_STATE.Idle:

                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    imMoving.Play();
                    patrols = (patrols + 1) % patrolAreas.Length;
                    WalkingState();
                }
                break;

            case ENEMY_STATE.Walking:
                if (!miGo.pathPending && miGo.remainingDistance < 0.5f)
                {
                    IdleState();
                    imMoving.Stop();
                }
                break;
        }
    }
    void IdleState()
    {
        currentState = ENEMY_STATE.Idle;
        idleTimer = idleTime;
        miGo.isStopped = true;
    }

    void WalkingState()
    {
        currentState = ENEMY_STATE.Walking;
        miGo.isStopped = false;
        miGo.speed = walkingSpeed;
        miGo.destination = patrolAreas[patrols].position;
    }
}
