using UnityEngine;

public class pipes_movement : MonoBehaviour
{
    void Start()
    {
        
    }

    public int pipe_speed;

    void Update()
    {
        if (!bird_movement.game_over && bird_movement.game_started)
            this.transform.position -= new Vector3(pipe_speed * Time.deltaTime, 0, 0);

        if (this.transform.position.x <= -7.8) {
            float random_y_position = Random.Range(-3.75f, 0.52f);
            this.transform.position = new Vector3(-1.13f, random_y_position, 89);
        }
    }
}
