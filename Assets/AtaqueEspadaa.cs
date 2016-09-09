using UnityEngine;
using System.Collections;

public class AtaqueEspadaa : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("AtaqueEspada")) ;
	
	}
    public void Golpear()
    {
        animator.SetTrigger("AtaqueEspada");
    }
}
