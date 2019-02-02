using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour {

	PathFinder pathFinder;
	[SerializeField] Scanner playerScanner;

	[SerializeField] Aj settings;

	Player priorityTarget;
	List<Player> myTargets;

	public event System.Action<Player> OnTargetSelected;

	private EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth 
	{
		get
		{ 
			if (m_EnemyHealth == null)
				m_EnemyHealth = GetComponent<EnemyHealth> ();
			return m_EnemyHealth;
		}
	}

	private EnemyState m_EnemyState;
	public EnemyState EnemyState 
	{
		get
		{ 
			if (m_EnemyState == null)
				m_EnemyState = GetComponent<EnemyState> ();
			return m_EnemyState;
		}
	}

	void Start () {
		pathFinder = GetComponent<PathFinder> ();
		pathFinder.Agent.speed = settings.WalkSpeed;

		playerScanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady ();

		EnemyHealth.OnDeath += EnemyHealth_OnDeath;
		EnemyState.OnModeChanged += EnemyState_OnModeChanged;
	}

	private void EnemyState_OnModeChanged (EnemyState.EMode state)
	{
		pathFinder.Agent.speed = settings.WalkSpeed;
		
		if (state == EnemyState.EMode.AWARE)
			pathFinder.Agent.speed = settings.RunSpeed;
	}

	private void EnemyHealth_OnDeath ()
	{
		
	}

	private void Scanner_OnScanReady ()
	{
		if (priorityTarget != null)
			return;

		myTargets = playerScanner.ScanForTargets<Player> ();

		if (myTargets.Count == 1)
			priorityTarget = myTargets [0];
		else
			SelectClosestTarget ();

		if (priorityTarget != null) 
		{
			if (OnTargetSelected != null)
				OnTargetSelected (priorityTarget);
		}
	}

	private void SelectClosestTarget () {
		float closestTarget = playerScanner.ScanRange;
		foreach (var possibleTarget in myTargets) 
		{
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}
	}

	void Update () 
	{
		if (priorityTarget == null)
			return;

		transform.LookAt (priorityTarget.transform.position);
	}
}
