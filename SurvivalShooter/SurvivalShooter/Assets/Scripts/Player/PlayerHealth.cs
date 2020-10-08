using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public AudioClip lowHealthClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public TimeManager timeManager;

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    public bool damaged;
    public CameraShake cameraShake;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;

        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        StartCoroutine(cameraShake.Shake(.15f, .4f));

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if (currentHealth <= 25)
        {
            playerAudio.loop = true;
            playerAudio.clip = lowHealthClip;
            playerAudio.Play();
        }
        
        if (currentHealth <= 0 && !isDead)
        {
            timeManager.BulletTime();
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");
        playerAudio.loop = false;
        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
