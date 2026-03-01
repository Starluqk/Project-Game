using Unity.VisualScripting;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public GameObject pickUpEffect;
public GameObject murADetruire;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (murADetruire != null)
            {
                Destroy(murADetruire);
            }
            GameManager.hasKey = true;
            Debug.Log("Key has been added");
            if (pickUpEffect != null)
                Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}

