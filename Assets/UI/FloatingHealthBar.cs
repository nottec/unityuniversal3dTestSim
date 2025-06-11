using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    Actor actor;

    [SerializeField] private Image _healthbarSprite;

    private float targetFillAmount;
    private float smoothSpeed = 5f; // higher = faster smoothing

    void Update()
    {
        if (_healthbarSprite != null)
        {
            _healthbarSprite.fillAmount = Mathf.Lerp(_healthbarSprite.fillAmount, targetFillAmount, Time.deltaTime * smoothSpeed);
        }
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        float fill = currentHealth / maxHealth;
        fill = Mathf.Clamp(fill, 0.01f, 1f);
        targetFillAmount = fill; // <-- set the target, not instantly change it
    } 
}
