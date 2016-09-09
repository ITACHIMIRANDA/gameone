using UnityEngine;
using System.Collections;

public class Mirar : MonoBehaviour {
    private GameObject Player;
    private NavMeshAgent nav;

	// Para mirar y seguir al personaje
	void Start () {
        Player = GameObject.FindWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
	}
	
	
	void Update () {
        transform.LookAt(Player.transform);
        if (nav.isActiveAndEnabled) {
            nav.SetDestination(Player.transform.position); }
        
	}
}
