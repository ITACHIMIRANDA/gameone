using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    //salud inicial
    public int startingHealth = 100;

    //salud actual
    public int currentHealth;

    //para relacionar la barra de salud
    public Slider healthSlider;

    //texto de vida
    public Text Saludtext;

    //public AudioClip deathclip;

    Animator anim;
    // AudioSource playerAudio;
    // PlayerMovement playerMovement;

    //PlayerShooting playerShooting;
    public Image damageImage;
    public float flashSpeed = 10f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    bool isDead;
    bool damaged = false;

    public int dano = 10;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent <AudioSource> ();
        // playerMovement = GetComponent<PlayerMovement>();

        //playerShooting = GetComponentInChildren<PlayerShooting> ();

        //Saludtext.text = startingHealth.ToString();
        healthSlider.value = startingHealth;
        currentHealth = startingHealth;  //al iniciar ponemos la salud actual.
    }



    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed);
        }
        damaged = false;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
            Death();

        }
    }


    // ESTO PARA CUANDO GOLPEA EL BICHO

    void OnCollisionEnter(Collision col)
    {


        if (col.gameObject.tag == "Enemigo" && !damaged)
        {

            damaged = true;
            currentHealth -= dano;
            healthSlider.value = currentHealth;

            Debug.Log("DAÑO");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Enemigo")


            // ESTO LO PONGO PARA QUE CUANDO TE GOLPEE EL BICHO (UYYY EL BICHOOO),
            // HASTA QUE NO SALGAS DE SU COLLIDER Y PASEN 3 SEGUNDOS NO TE PUEDA GOLPEAR OTRA VEZ
            StartCoroutine(EsperaTrasGolpe());

    }

    IEnumerator EsperaTrasGolpe()
    {

        yield return new WaitForSeconds(1);
        damaged = false;
    }


    /*public void Takedamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
       // Saludtext.text = currentHealth.ToString();
        healthSlider.value = currentHealth; //al iniciar ponemos la salud actual
    }
	public void Takedamage (int amount) //el daño que va a recibir del enemigo
    {
        damaged = true;
        currentHealth -= amount;
        Saludtext.text = currentHealth.ToString();
        healthSlider.value = currentHealth;

        //playerAudio.Play (); PLAYER HURT (cuando hacen daño al personaje)

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }*/
    void Death()
    {
        isDead = true;
        //playerShooting.disableeffects ();
        // anim.SetTrigger("Dead");
        //playerMovement.enabled = false;
        //player.shooting.enabled=false;

    }

    void OnTriggerEnter(Collider vida)
    {
        if (vida.gameObject.tag == "MedipackRed")
        {
            currentHealth += 30;
            if (currentHealth >= 100) { 
                currentHealth = 100;
            }
            healthSlider.value = currentHealth;
            Destroy(vida.gameObject);
        }
    }
}
