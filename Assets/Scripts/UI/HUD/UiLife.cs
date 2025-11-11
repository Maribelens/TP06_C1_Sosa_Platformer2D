using UnityEngine;
using UnityEngine.UI;

public class UiLife : MonoBehaviour
{
    [Header("LifeBar Panel")]
    [SerializeField] private HealthSystem target;
    [SerializeField] private Image lifeBar;

    private void Awake()
    {
        // Suscripción a eventos del sistema de salud
        target.onLifeUpdated += HealthSystem_onLifeUpdated;
        target.onDie += HealthSystem_onDie;
    }

    public void HealthSystem_onLifeUpdated(int current, int max)
    {
        // Actualiza la barra de vida según el porcentaje restante
        float lerp = current / (float)max;
        lifeBar.fillAmount = lerp;
    }

    private void HealthSystem_onDie()
    {
        // Vacía la barra al morir el jugador
        lifeBar.fillAmount = 0;
    }

    private void OnDestroy()
    {
        // Evita referencias colgantes al destruir el objeto
        target.onLifeUpdated -= HealthSystem_onLifeUpdated;
        target.onDie -= HealthSystem_onDie;
    }
}
