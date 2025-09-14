using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class bird_movement : MonoBehaviour
{
    Animator anim;
    public AudioClip wing_sound, score_sound, hit_sound;
    public TextMeshProUGUI score_text, game_over_scene_score, highest_score_text;
    public GameObject game_over_panel;
    public GameObject opening_scene;
    public GameObject game_panel;
    public int jump_speed;
    int score = 0;
    int highest_score = 0;
    int hit_score = 0;
    public static bool game_over = false;
    public static bool game_started = false;
    Rigidbody2D rb;
    void Start()
    {
        anim = GetComponent<Animator>();

        highest_score = PlayerPrefs.GetInt("HighestScore", 0);
        highest_score_text.text = "Highest Score: " + highest_score.ToString();

        anim.SetBool("bird_can_fly", false);
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;

    }

    void Update()
    {
        if (!game_started && Input.GetKeyDown(KeyCode.Space))
        {
            game_started = true;
            rb.simulated = true; 
            opening_scene.SetActive(false);
            game_panel.SetActive(true);
            anim.SetBool("bird_can_fly", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !game_over && game_started)
        {
            this.GetComponent<AudioSource>().PlayOneShot(wing_sound);
            this.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero; // same with new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, jump_speed, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        score++;
        score_text.text = score.ToString();
        game_over_scene_score.text = "Score: " + score.ToString();

        this.GetComponent<AudioSource>().PlayOneShot(score_sound);

        if (score > highest_score)
        {
            highest_score = score;
            highest_score_text.text = "Highest Score: " + highest_score.ToString();

            PlayerPrefs.SetInt("HighestScore", highest_score);
            PlayerPrefs.Save();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hit_score == 0)
        {
            this.GetComponent<AudioSource>().PlayOneShot(hit_sound);
            hit_score++;
        }
        game_over = true;
        game_over_panel.SetActive(true);

        anim.SetBool("bird_can_fly", false);

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        game_over = false;
        game_started = false;
    }
}
