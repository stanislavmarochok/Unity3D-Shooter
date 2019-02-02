using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour {

	[System.Serializable]
	public class MouseInput
	{
		public Vector2 Damping;
		public Vector2 Sensitivity;
		public bool LockMouse;
	}

	[SerializeField] Aj settings;
	[SerializeField] MouseInput MouseControl;
	[SerializeField] AudioController footSteps;
	[SerializeField] float minimalMoveTreshold;

	public PlayerAim playerAim;

	public PlayerShoot PlayerShoot;

	Vector3 previousPosition;

	private CharacterController m_MoveController;
	public CharacterController MoveController {
		get {
			if (m_MoveController == null)
				m_MoveController = GetComponent<CharacterController> ();
			return m_MoveController;
		}
	}

	private PlayerState m_PlayerState;
	public PlayerState PlayerState {
		get { 
			if (m_PlayerState == null) {
				m_PlayerState = GetComponentInChildren <PlayerState> ();
			}
			return m_PlayerState;
		}
	}

	private PlayerHealth m_PlayerHealth;
	public PlayerHealth PlayerHealth {
		get { 
			if (m_PlayerHealth == null) {
				m_PlayerHealth = GetComponentInChildren <PlayerHealth> ();
			}
			return m_PlayerHealth;
		}
	}

	InputController playerInput;
	Vector2 mouseInput;

	// Use this for initialization
	void Awake () {
		playerInput = GameManager.Instance.InputController;
		PlayerShoot = gameObject.GetComponent<PlayerShoot> ();
		GameManager.Instance.LocalPlayer = this;

		if (MouseControl.LockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!PlayerHealth.IsAlive)
			return;

		Move ();
		LookAround ();

	}

	void Move () {

		float moveSpeed = settings.RunSpeed;

		if (playerInput.IsWalking)
			moveSpeed = settings.WalkSpeed;

		if (playerInput.IsSprinting)
			moveSpeed = settings.SprintSpeed;

		if (playerInput.IsCrouching)
			moveSpeed = settings.CrouchSpeed;

		Vector2 direction = new Vector2 (playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
		MoveController.SimpleMove (transform.forward * direction.x + transform.right * direction.y);

		if (Vector3.Distance (transform.position, previousPosition) > minimalMoveTreshold)
			footSteps.Play ();

		previousPosition = transform.position;
	}

	void LookAround ()
	{
		
		mouseInput.x = Mathf.Lerp (mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
		mouseInput.y = Mathf.Lerp (mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
		transform.Rotate (Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
		playerAim.SetRotation (mouseInput.y * MouseControl.Sensitivity.y);
	}

}
