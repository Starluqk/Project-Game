using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowLevels : MonoBehaviour
{
    [SerializeField] private GameObject buttonLevel;

    private float[] minXAnchor = new float[] { 0.307f, 0.422f, 0.486f, 0.561f, 0.658f, 0.292f, 0.394f, 0.504f, 0.588f, 0.663f};
    private float[] maxXAnchor = new float[] { 0.347f, 0.464f, 0.527f, 0.603f, 0.699f, 0.332f, 0.437f, 0.544f, 0.629f, 0.707f};
    private float[] minYAnchor = new float[] { 0.639f, 0.585f, 0.578f, 0.570f, 0.618f, 0.433f, 0.430f, 0.389f, 0.387f, 0.390f};
    private float[] maxYAnchor = new float[] { 0.718f, 0.666f, 0.661f, 0.653f, 0.701f, 0.518f, 0.512f, 0.474f, 0.468f, 0.474f};
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
