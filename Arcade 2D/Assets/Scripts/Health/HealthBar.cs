using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _totalHealth;
    [SerializeField] private Image _currentHealth;


    void Start()
    {
        FillTotalHealth();
    }

    void Update()
    {
        FillCurrentHealth();
    }

    private void FillTotalHealth()
    {
        _totalHealth.fillAmount = _health.GetCurrentHealth() / 10;
    }

    private void FillCurrentHealth()
    {
        _currentHealth.fillAmount = _health.GetCurrentHealth() / 10;
    }
}
