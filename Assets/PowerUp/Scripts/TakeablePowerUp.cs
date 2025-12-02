using UnityEngine;
using System.Collections;

public class TakeablePowerUp : MonoBehaviour {
	CustomizablePowerUp customPowerUp;

	void Start() {
		customPowerUp = GetComponentInParent<CustomizablePowerUp>();
		if (customPowerUp == null)
		{
			Debug.LogWarning("TakeablePowerUp: no se encontró CustomizablePowerUp en los padres.");
		}
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log($"TakeablePowerUp: OnTriggerEnter con '{other.name}' tag='{other.tag}'");
		if (other == null) return;
		if(other.CompareTag("Player")) {
			Debug.Log("TakeablePowerUp: Colisión con Player detectada.");
			if (customPowerUp != null)
			{
				PowerUpManager.Instance.Add(customPowerUp);
				if(customPowerUp.pickUpSound != null){
					AudioSource.PlayClipAtPoint(customPowerUp.pickUpSound, transform.position);
				}

				PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
				if(playerHealth != null && customPowerUp.isHealing)
				{
					Debug.Log($"TakeablePowerUp: Sanando jugador {customPowerUp.healAmount}");
					playerHealth.Heal(customPowerUp.healAmount);
				}

				PlayerController playerController = other.GetComponentInParent<PlayerController>();
				if(playerController != null && customPowerUp.isFireModePowerUp)
				{
					FireModeManager.FireMode mode = (FireModeManager.FireMode)customPowerUp.fireMode;
					Debug.Log($"TakeablePowerUp: Aplicando modo de disparo {mode} por {customPowerUp.fireModeDuration}s");
					playerController.SetFireMode(mode, customPowerUp.fireModeDuration);
				}
			}
			else
			{
				Debug.LogWarning("TakeablePowerUp: customPowerUp es null, no se aplicarán efectos.");
			}
			Destroy(transform.parent.gameObject);
		}
		else
		{
			Debug.Log("TakeablePowerUp: Colisión ignorada (no es Player).");
		}
	}
}
