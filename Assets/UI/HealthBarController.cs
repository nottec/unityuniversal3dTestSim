using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public Actor actor;
    HealthBarUI healthBar;
    void OnEnable()
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<HealthBarUI>();
        healthBar.dataSource = actor;
    }

        void Update()
    {
        if (healthBar != null && actor != null)
        {
            healthBar.progress = Mathf.Lerp(healthBar.progress, actor.currentHealth, Time.deltaTime * 10f);
        }
    }
}
