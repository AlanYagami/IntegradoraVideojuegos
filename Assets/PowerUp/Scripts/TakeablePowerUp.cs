using UnityEngine;
using System.Collections;

public class TakeablePowerUp : MonoBehaviour {
	CustomizablePowerUp customPowerUp;

	void Start() {
		customPowerUp = (CustomizablePowerUp)transform.parent.gameObject.GetComponent<CustomizablePowerUp>();
		//this.audio.clip = customPowerUp.pickUpSound;
	}

	void OnTriggerEnter (Collider collider) {
		if(collider.CompareTag("Player")) {
			PowerUpManager.Instance.Add(customPowerUp);
			if(customPowerUp.pickUpSound != null){
				AudioSource.PlayClipAtPoint(customPowerUp.pickUpSound, transform.position);
			}

			// Curar al jugador si este power up está configurado como curativo
			PlayerHealth playerHealth = collider.GetComponentInParent<PlayerHealth>();
			if(playerHealth != null && customPowerUp.isHealing)
			{
				playerHealth.Heal(customPowerUp.healAmount);
			}

			Destroy(transform.parent.gameObject);
		}
	}
}
