using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowLevels : MonoBehaviour
{
    [SerializeField] private GameObject buttonLevel;

    private float[] minXAnchor = new float[] { 0.307f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    private float[] maxXAnchor = new float[] { 0.347f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    private float[] minYAnchor = new float[] { 0.639f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    private float[] maxYAnchor = new float[] { 0.718f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < GameManager.NbLevels; i++)
        {
            GameObject labell = Instantiate(buttonLevel, transform);

            RectTransform newDoor = labell.GetComponent<RectTransform>();

            newDoor.anchorMin = new Vector2(minXAnchor[i % 10], minYAnchor[i % 10]);
            newDoor.anchorMax = new Vector2(maxXAnchor[i % 10], maxYAnchor[i % 10]);

            TextMeshProUGUI levelLabell = labell.GetComponentInChildren<TextMeshProUGUI>();
            levelLabell.text = (i + 1).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
