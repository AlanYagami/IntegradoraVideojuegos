using UnityEngine;
using System.Collections;

public class TakeablePowerUp : MonoBehaviour {
	CustomizablePowerUp customPowerUp;

	void Start() {
		customPowerUp = (CustomizablePowerUp)transform.parent.gameObject.GetComponent<CustomizablePowerUp>();
		//this.audio.clip = customPowerUp.pickUpSound;
	}

	void OnTriggerEnter (Collider collider) {
		if(collider.tag == "Player") {
			PowerUpManager.Instance.Add(customPowerUp);
			if(customPowerUp.pickUpSound != null){
				AudioSource.PlayClipAtPoint(customPowerUp.pickUpSound, transform.position);
			}
			
			// Si es un power up de curación, sanar al jugador
			PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
			if(playerHealth != null && customPowerUp.powerUpName.Contains("green"))
			{
				playerHealth.HealOne();
			}
			
			Destroy(transform.parent.gameObject);
		}
	}
}
