using UnityEngine;

public class OrbBar : MonoBehaviour
{
    [SerializeField] RectTransform orbBar;

    public float maxBarCharge;
    public float currentCharge;
    public float width;
    public float height;
    
    public void SetMaxCharge(float maxCharge)
    {
        maxBarCharge = maxCharge;
    }

    public void SetCharge(float charge)
    {
        currentCharge = charge;
        float newCharge = currentCharge / maxBarCharge * width;

        orbBar.sizeDelta = new Vector2(newCharge, height);
    }
}
