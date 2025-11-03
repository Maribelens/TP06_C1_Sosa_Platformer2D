using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private HealthSystem target;
    [SerializeField] private Image lifeBar;

    private void Awake()
    {
        target.onLifeUpdated += HealthSystem_onLifeUpdated;
        target.onDie += HealthSystem_onDie;
    }

    public void HealthSystem_onLifeUpdated(int current, int max)
    {
        float lerp = current / (float)max;
        lifeBar.fillAmount = lerp;
    }

    private void HealthSystem_onDie()
    {
        lifeBar.fillAmount = 0;
    }

    private void OnDestroy()
    {
        target.onLifeUpdated -= HealthSystem_onLifeUpdated;
        target.onDie -= HealthSystem_onDie;
    }
}
