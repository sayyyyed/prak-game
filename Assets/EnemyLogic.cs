using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLogic : MonoBehaviour
{
    [Header("Enemy Setting")]
    public float hitPoints = 100f;
    public float turnSpeed = 15f;
    public Transform target;
    public float ChaseRange;
    private UnityEngine.AI.NavMeshAgent agent;
    private float DistanceToTarget;
    private float DistanceToDefault;
    private Animator anim;
    Vector3 DefaultPosition;

    [Header("Enemy SFX")]
    public AudioClip GethitAudio;
    public AudioClip StepAudio;
    public AudioClip AttackSwingAudio;
    public AudioClip AttackConnectAudio;
    public AudioClip DeathAudio;
    AudioSource EnemyAudio;

    [Header("Enemy VFX")]
    public ParticleSystem SlashEffect;
    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {
        EnemyAudio.clip = GethitAudio;
        EnemyAudio.Play();
        hitPoints -= damage;
        anim.SetTrigger("GetHit");
        anim.SetFloat("Hitpoint", hitPoints);
        if (hitPoints <= 0)
        {
            EnemyAudio.clip = DeathAudio;
            EnemyAudio.Play();
            Destroy(gameObject, 3f);
        }
    }

    private void Start()
    {
        target = FindAnyObjectByType<PlayerLogic>().transform;
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        anim.SetFloat("Hitpoint", hitPoints);
        EnemyAudio = this.GetComponent<AudioSource>();
        DefaultPosition = this.transform.position;
    }

    public void step()
    {
        EnemyAudio.clip = StepAudio;
        EnemyAudio.Play();
    }

    private void Update()
    {
        DistanceToTarget = Vector3.Distance(target.position, transform.position);
        DistanceToDefault = Vector3.Distance(DefaultPosition, transform.position);
        if (DistanceToTarget <= ChaseRange && hitPoints != 0)
        {
            FaceTarget(target.position);
            if (DistanceToTarget > agent.stoppingDistance + 2f)
            {
                ChaseTarget();
                SlashEffect.Stop();

            }
            else if (DistanceToTarget <= agent.stoppingDistance)
            {
                Attack();

            }
        }

        else if (DistanceToTarget >= ChaseRange * 2)
        {
            agent.SetDestination(DefaultPosition);
            FaceTarget(DefaultPosition);
            if (DistanceToDefault <= agent.stoppingDistance)
            {
                Debug.Log("Time to stop");
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
            }
        }
    }

    public void SlashEffectToggleOn()
    {
        SlashEffect.Play();
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void Attack()
    {
        Debug.Log("Attack");
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);

    }
    public void HitConnect()
    {
        EnemyAudio.clip = AttackSwingAudio;
        EnemyAudio.Play();
        if (DistanceToTarget <= agent.stoppingDistance)
        {
            EnemyAudio.clip = AttackConnectAudio;
            EnemyAudio.Play();
            target.GetComponent<PlayerLogic>().PlayerGetHit(50f);

        }
    }
    public void ChaseTarget()
    {
        agent.SetDestination(target.position);
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);
    }

    void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);
    }


}
