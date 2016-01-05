using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;
    public ParticleSystem m_ExplosionParticles2;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 10000000f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;
    public float m_ExplosionRadius = 10000.0f;             


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        // Find all the tanks in an area around the shell and damage them.
        Debug.Log("Deteced " + colliders.Length + " colliders");
        Debug.Log("The Exploradius is " + m_ExplosionRadius);
        for (int i= 0; i < colliders.Length; i++){
            
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            if (!targetRigidbody)
                continue;
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
            if (!targetHealth)
                continue;
            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage);

        }
        m_ExplosionParticles.transform.parent=null;
        m_ExplosionParticles2.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionParticles2.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject,m_ExplosionParticles.duration);
        Destroy(m_ExplosionParticles2.gameObject, m_ExplosionParticles2.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {

    	Vector3 explosionToTarget = targetPosition - transform.position;

    	float explosionDistance = explosionToTarget.magnitude;

    	float relativeDistance = (m_ExplosionRadius - explosionDistance) /m_ExplosionRadius;
    	float damage = m_MaxDamage;
    	damage = Mathf.Max(0f, damage);

    	return damage;
        // Calculate the amount of damage a target should take based on it's position.
        //return 0f;
    }
}