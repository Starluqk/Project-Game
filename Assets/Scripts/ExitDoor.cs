using System;
using System.Collections;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private Animator anim;
    private bool aEteTouchee = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // On vérifie si c'est le joueur qui touche la porte
        if (collision.CompareTag("Player"))
        {
            aEteTouchee = true;
            StartCoroutine(SeqenceSortie());
            Debug.Log("Niveau terminé ! Passage au suivant...");
           
        }
    }

    IEnumerator SeqenceSortie()
    {
        if (anim != null)
        {
            anim.SetTrigger("FERMETURE");
        }
        yield return new WaitForSeconds(0.8f);
             GameManager.NextLevel();
        }
    }

