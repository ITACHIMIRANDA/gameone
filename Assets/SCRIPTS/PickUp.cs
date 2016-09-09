using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{

    public int CoinValue;
    public GameObject pickupparticleefect;
    public AudioClip sonidoMoneda;
    private bool _triggered;
    //private AudioSource _audioSource;

    void Awake()
    {
        //_audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    /*void Update () {
		if (_triggered && !_audioSource.isPlaying)
            
        Destroy (gameObject);
       

    }
    */

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player" && !_triggered)
        {
            AudioSource.PlayClipAtPoint(sonidoMoneda, transform.position);
            Destroy(gameObject);
            _triggered = true;
            //_audioSource.enabled = true;

            //gameObject.GetComponent<MeshRenderer> ().enabled = false;

            Instantiate(pickupparticleefect, gameObject.transform.position, Quaternion.LookRotation(Vector3.up));

            GameManager.PickUpCount += CoinValue;
        }
    }
}

