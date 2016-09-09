using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerShooting : MonoBehaviour
    {
        public int damagePerShot = 20;                  // El daño que hace con cada disparo
        public float timeBetweenBullets = 0.15f;        // El tiempo que tiene que pasar entre disparo y disparo
        public float range = 100f;                      // La distancia que hara la bala


        float timer;                                    // El tiempo que ha pasado desde el ultimo disparo
        Ray shootRay;                                   // Punto inicial y direccion del rayo(disparo)
        RaycastHit shootHit;                            // El resultado del objeto que haya colisionado
        int shootableMask;                              // Las capas con las que queremos que el disparo colisione
        ParticleSystem gunParticles;                    // Reference to the particle system.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
		//public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                //El tiempo que va a estar activo la luz y el rayo


        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem> ();
            gunLine = GetComponent <LineRenderer> ();
            gunAudio = GetComponent<AudioSource> ();
            gunLight = GetComponent<Light> ();
			//faceLight = GetComponentInChildren<Light> ();
        }


        void Update()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // si pulsamos el boton de disparar y si ha pasado el tiempo de disparar y si el juego no esta pausado.
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot();
            }
            // si ha pasado suficiente tiempo como para parar la luz y el rayo
            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects();
            }
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			//faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play ();

            // Enable the lights.
            gunLight.enabled = true;
			//faceLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop ();
            gunParticles.Play ();

            // ponemos donde se situa el rayo
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            //si colisiona o no con algun objeto
            if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

                // If the EnemyHealth component exist...
                if(enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage (damagePerShot, shootHit.point);
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}