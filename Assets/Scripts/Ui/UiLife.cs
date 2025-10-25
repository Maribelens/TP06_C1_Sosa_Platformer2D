using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{

    [SerializeField] private HealthSystem target;
    [SerializeField] private Image lifeBar;

    private void Awake()
    {
        //target.onReceiveDamage.AddListener(UpdateLife);
        target.onLifeUpdated += HealthSystem_onLifeUpdated;
        target.onDie += HealthSystem_onDie;
    }

    private void OnDestroy()
    {
        //target.onReceiveDamage.RemoveListener(UpdateLife);
        target.onLifeUpdated -= HealthSystem_onLifeUpdated;
        target.onDie -= HealthSystem_onDie;
    }

    public void HealthSystem_onLifeUpdated(int current, int max)
    {
        //target.onReceiveDamage2.Invoke(0, 0);// Error: Nunca invocar eventos de otro script!

        float lerp = current / (float)max;
        lifeBar.fillAmount = lerp;
    }

    private void HealthSystem_onDie()
    {
        lifeBar.fillAmount = 0;
    }
}
