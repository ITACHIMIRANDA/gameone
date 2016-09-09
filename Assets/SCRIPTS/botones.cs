using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class botones : MonoBehaviour {
    //public GameObject espada;
    //Animator anim;

    /*public void OnGUI()
     {
         if (GUI.Button(new Rect( 1745 / 3, 4085 / 20, 430, 110), ""))
         {
             SceneManager.LoadScene(1);
         }
     }
     // Use this for initialization*/


    public void BotonStart (){
        SceneManager.LoadScene(1);
        //Application.LoadLevel(1);
    }

    public void BotonInicio ()
    {
        SceneManager.LoadScene(0);
        //Application.LoadLevel(1);
    }

     public void Atacar()
     {
         //anim.SetTrigger("AtaqueEspada");
         //enemyAudio.clip = ataqueClip;
         //enemyAudio.Play();

     }

    /* private void GetComponent<T>(string v)
     {
         throw new NotImplementedException();
     }*/

    public void BotonCreditos()
    {
        SceneManager.LoadScene(3);
        //Application.LoadLevel(1);
    }

    public void BotonExit (){
        Application.Quit();
    }

    
}
