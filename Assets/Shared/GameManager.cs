using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

	public event System.Action<Player> OnLocalPlayerJoined;
	private GameObject gameObject;

	private static GameManager m_Instance;
	public static GameManager Instance {
		get { 
			if (m_Instance == null) {
				m_Instance = new GameManager ();
				m_Instance.gameObject = new GameObject ("_gameManager");
				m_Instance.gameObject.AddComponent<InputController> ();
				m_Instance.gameObject.AddComponent <Timer> ();
				m_Instance.gameObject.AddComponent <Respawner> ();
			}
			return m_Instance;
		}
	}

	private InputController m_InputController;
	public InputController InputController {
		get  { 
			if (m_InputController == null) {
				m_InputController = gameObject.GetComponent <InputController> ();
			}
			return m_InputController;
		}
	}

	private Timer m_Timer;
	public Timer Timer {
		get {
			if (m_Timer == null)
				m_Timer = gameObject.GetComponent<Timer> ();
			return m_Timer;
		}
	}

	private EventBus m_EventBus;
	public EventBus EventBus {
		get {
			if (m_EventBus == null)
				m_EventBus = new EventBus();
			return m_EventBus;
		}
	}

	private Respawner m_Respawner;
	public Respawner Respawner {
		get {
			if (m_Respawner	== null)
				m_Respawner = gameObject.GetComponent <Respawner> ();
			return m_Respawner;
		}
	}

	private Player m_LocalPlayer;
	public Player LocalPlayer {
		get { 
			return m_LocalPlayer;
		}
		set { 
			m_LocalPlayer = value;
			if (OnLocalPlayerJoined != null) {
				OnLocalPlayerJoined (m_LocalPlayer);
			}
		}
	}

}
