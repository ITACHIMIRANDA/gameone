using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    GameObject espada;
    //salud inicial
    public int startingHealth = 100;

    //salud actual
    public int currentHealth;

    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip DeadClip;
    public Slider zombiesDamageText;
    public int DanoEnemy = 10;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;


    bool isDead;
    bool isSinking;
    bool DamageEnemy = false;


    void Awake()
    {
        espada = GameObject.FindGameObjectWithTag ("Espada");
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;
        //zombieSlider.value = currentHealth;

    }



    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Espada" && espada.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AtaqueEspada") && !DamageEnemy)
        {

            DamageEnemy = true;
            currentHealth -= DanoEnemy;
            zombiesDamageText.value = currentHealth;

            Debug.Log("DAÑO");
        }
        if (currentHealth <= 0 && !isDead)
        {
            Dead();
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")


            // ESTO LO PONGO PARA QUE CUANDO TE GOLPEE EL BICHO (UYYY EL BICHOOO),
            // HASTA QUE NO SALGAS DE SU COLLIDER Y PASEN 3 SEGUNDOS NO TE PUEDA GOLPEAR OTRA VEZ
            StartCoroutine(EsperaTrasGolpe());

    }

    IEnumerator EsperaTrasGolpe()
    {

        yield return new WaitForSeconds(1);
        DamageEnemy = false;
    }
    public void Takedamage(int amount) //añadir particulas
    {

        if (isDead)
            return;
        enemyAudio.Play();
        currentHealth -= amount;

    }
    void Dead()
    {
        isDead = true;

        //CapsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = DeadClip;
        enemyAudio.Play();

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 3.9f);

        StartSinking();
    }
    public void StartSinking()
    {
        // GetComponent<NavMeshAgent>().enabled = false;
        // GetComponent<Rigidbody>().isKinematic = true;

        //esto es el nombre para que comienze el tiempo
        StartCoroutine(EsperaMuerte());

        
        //score manager
        // Destroy(gameObject, 3f);
    }

    //yield da el tiempo para que actue lo siguiente
    IEnumerator EsperaMuerte()
    {

        yield return new WaitForSeconds(3.5f);
        DamageEnemy = false;
        isSinking = true;
    }
}