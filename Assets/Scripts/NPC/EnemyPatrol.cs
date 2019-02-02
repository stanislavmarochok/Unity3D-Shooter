using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour {

	[SerializeField] WaypointController waypointController;
	[SerializeField] float waitTimeMin;
	[SerializeField] float waitTimeMax;

	PathFinder pathFinder;
	private EnemyPlayer m_EnemyPlayer;
	public EnemyPlayer EnemyPlayer 
	{
		get
		{ 
			if (m_EnemyPlayer == null)
				m_EnemyPlayer = GetComponent<EnemyPlayer> ();
			return m_EnemyPlayer;
		}
	}

	void Start () {
		waypointController.SetNextWaypoint ();
	}

	void Awake () {
		pathFinder = GetComponent<PathFinder> ();
		pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
		waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;

		EnemyPlayer.EnemyHealth.OnDeath += EnemyPlayer_EnemyHealth_OnDeath;
		EnemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
	}

	private void EnemyPlayer_OnTargetSelected (Player obj)
	{
		pathFinder.Agent.Stop ();
	}

	private void EnemyPlayer_EnemyHealth_OnDeath ()
	{
		pathFinder.Agent.Stop ();
	}

	void WaypointController_OnWaypointChanged (Waypoint waypoint)
	{
		pathFinder.SetTarget (waypoint.transform.position);
	}

	private void PathFinder_OnDestinationReached ()
	{
		// assume we are patrolling
		GameManager.Instance.Timer.Add(waypointController.SetNextWaypoint, Random.Range(waitTimeMin, waitTimeMax));
	}
}
