using Unity.VisualScripting;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.hasKey = true;
            Debug.Log("Key has been added");
             if(pickUpEffect != null)
                            Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
           
        }
    }
}
