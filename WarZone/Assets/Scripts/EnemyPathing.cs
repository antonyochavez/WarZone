using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfiguration waveConfig;
    List<Transform> waypoints; // list of points (Transform) enemy travels to
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position; /* sets position
            of enemy to 1st waypoint */
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfiguration(WaveConfiguration waveConfig) /* each 
        enemy will be linked to a wave configuration in Unity, so this method 
        will allow us to refer to the linked wave configuration in this class */
    {
        this.waveConfig = waveConfig;
    }

    private void Move() // method that moves enemy to next waypoint until final endpoint
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementInFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, movementInFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
