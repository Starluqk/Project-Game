using TMPro;
using UnityEngine;

public class ShowLevels : MonoBehaviour
{
    [SerializeField] private GameObject buttonLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < GameManager.NbLevels; i++)
        {
            GameObject labell = Instantiate(buttonLevel, transform);
            TextMeshProUGUI levelLabell = labell.GetComponentInChildren<TextMeshProUGUI>();
            levelLabell.text = (i+1).ToString();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
