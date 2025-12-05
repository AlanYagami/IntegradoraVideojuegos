using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
	private PowerUpManager() {}

	private Queue<CustomizablePowerUp> powerUps;
	private Queue<CustomizablePowerUp> powerUpsLogs;
	private ushort powerUpLogLimit = 3;
	
	public int Count {
		get {
			return powerUps.Count;
		}
	}
	
	void Awake() {
		this.powerUps = new Queue<CustomizablePowerUp>();
		this.powerUpsLogs = new Queue<CustomizablePowerUp>();
	}	

	public void Add(CustomizablePowerUp powerUp)
	{
		this.powerUps.Enqueue(powerUp);
		this.powerUpsLogs.Enqueue(powerUp);
		while (this.powerUpsLogs.Count > this.powerUpLogLimit && this.powerUpsLogs.Dequeue()) ;
	}

	private string RGBToHex(Color color)
	{
		return string.Format("#{0}{1}{2}", 
                     ((int)(color.r * 255)).ToString("X2"), 
                     ((int)(color.g * 255)).ToString("X2"), 
                     ((int)(color.b * 255)).ToString("X2"));
	}

	private SimplePowerUpUI ui;
	private FireModeManager fireModeManager;

	void Start()
	{
		// Buscar UI y FireModeManager
		ui = FindObjectOfType<SimplePowerUpUI>();
		fireModeManager = FindObjectOfType<FireModeManager>();
	}

	void Update()
	{
		if (fireModeManager != null && ui != null)
		{
			// Verificar si hay un modo de disparo activo (que no sea Normal)
			if (fireModeManager.IsActiveFireMode())
			{
				// Necesitamos acceder al tiempo restante. 
				// Como FireModeManager tiene 'fireModeDuration' y 'currentFireModeTime' privados,
				// asumiremos que podemos modificarlos o que necesitamos hacerlos públicos.
				// O mejor, usaremos reflexión o añadiremos un getter en FireModeManager si es posible.
				// Dado que la instrucción es "ajustes mínimos", intentaremos usar lo que hay.
				// FireModeManager no expone el tiempo restante.
				// Vamos a modificar FireModeManager para exponerlo.
				
				// Por ahora, asumamos que añadiremos GetTimeRemaining() a FireModeManager.
				float remaining = fireModeManager.GetTimeRemaining();
				ui.Show();
				ui.SetText($"Power-Up activo: {remaining:F1}s");
			}
			else
			{
				ui.Hide();
			}
		}
	}

	void OnGUI() {
		foreach(CustomizablePowerUp pu in powerUpsLogs) {
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			GUILayout.Label("You picked up <color=" + RGBToHex(pu.lightColor) + ">" + pu.powerUpName + "</color>");
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		GUI.Label(new Rect(Screen.width - 180, 0, 180, 20), "PowerUp count: <color=yellow>" + this.powerUps.Count + "</color>");
	}
}

