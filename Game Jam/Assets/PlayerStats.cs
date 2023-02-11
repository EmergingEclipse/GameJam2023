using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hpUpgrade;
    public int damageUpgrade;
    public int mouseUpgrade;

    public GameObject deathPanel;
    public static bool game_paused = false;
    Rigidbody2D m_Rigidbody;
    public float m_Speed = 5f;
    [SerializeField] private float PlayerDamage = 15;
    // Start is called before the first frame update

    private Animator anim;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (game_paused)
        {
            activate_pausemenu();
        }
        if (!game_paused)
        {
            PlayerMovement();
        }



    }

    private void PlayerMovement()
    {

        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        anim.SetFloat("moveX", m_Input.x);
        anim.SetFloat("moveY", m_Input.y);
        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
    }

    public void activate_pausemenu()
    {
        game_paused = true;
    }

    public void DeathTrigger()
    {
        activate_pausemenu();
        //saving the permanent upgrades after death
        PlayerPrefs.SetInt("hpUpgrade", hpUpgrade);
        PlayerPrefs.SetInt("damageUpgrade", hpUpgrade);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);


        deathPanel.SetActive(true);
        GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
        GameObject.Destroy(spawner);

    }

    public void Upgrades()
    {
        //Temporary Upgrades
        int moveSpeedUpgrade = 0;

        //actually upgradeing player stats
        //playerHP *= hpUpgrade;                                ///////////////////////////////////
        PlayerDamage *= damageUpgrade;
        m_Speed = m_Speed + moveSpeedUpgrade;
    }

    //creates all of the base upgrades on initial runtime so upgrades will work
    public void RunTimeData()
    {
        if (PlayerPrefs.GetInt("firstTime") == null)
        {
            PlayerPrefs.SetInt("firstTime", 0);
            PlayerPrefs.SetInt("hpUpgrade", 1);
            PlayerPrefs.SetInt("damageUpgrade", 1);
            PlayerPrefs.SetInt("mouseUpgrade", 0);
        }

        hpUpgrade = PlayerPrefs.GetInt("hpUpgrade");
        damageUpgrade = PlayerPrefs.GetInt("damageUpgrade");
        mouseUpgrade = PlayerPrefs.GetInt("mouseUpgrade");


    }



}