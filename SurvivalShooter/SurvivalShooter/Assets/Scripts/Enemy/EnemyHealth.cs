using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100f;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    public Image healthBar;
   

    


    void Awake ()
    {
      
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
       
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        

    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
        
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        healthBar.fillAmount = currentHealth / startingHealth;
        if (healthBar == null);
        {
            Debug.Log("is null");
        }

        if (currentHealth <= 0)
        {
            FindObjectOfType<HitStop>().Stop(1f);
            Death ();
        }
    }
    

    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
