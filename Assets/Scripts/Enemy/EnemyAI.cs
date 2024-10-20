using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _pathfinding;
    
    private void Awake()
    {
        _pathfinding = GetComponent<EnemyPathfinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }
    
    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            _pathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }
    
    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
    }
}
