﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Bird))]
public class BirdPlayer : MonoBehaviour
{
    public static BirdPlayer singelton = null;
    private Bird bird;
    public Text scoreText;
    // public BirdAI birdThatLearnsFromYou;
    public bool isJumping1;

    // Use this for initialization
    void Start()
    {
        if (singelton)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singelton = this;
            bird = GetComponent<Bird>();
            // GameManager.instance.AddBird();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // updating the score of the player
        scoreText.text = "Fitness: " + Mathf.Round(this.bird.Score());

        // checking whether we pressed jump, cross-platform input
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    bird.Jump();
                    isJumping1 = true;
                }
                else
                {
                    isJumping1 = false;
                }
            }
        }
        else
        {
            if (Input.GetButton("Jump"))
            {
                bird.Jump();
                isJumping1 = true;
            }
            else
            {
                isJumping1 = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // basically losing
        //bird.Kill();
        // birdThatLearnsFromYou.KillAIBird();
        singelton = null;
        Destroy(this.gameObject);
        GameManager.instance.RemovePlayerBird();
    }

}
