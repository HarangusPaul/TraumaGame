using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance

    public int playerScore = 0;  // Player's score

    // Reference to TMP Text component
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI objective;

    public GameObject doorSpiders;

    public GameObject outSideSpiders;

    public GameObject bedroom;
    
    public GameObject roomSpider;
    
    public GameObject apartment;
    
    public GameObject more;
    
    public GameObject walls;
    
    public GameObject downStairs;


    public GameObject paint;
    public GameObject paint2;
    
    
    // public GameObject hoop;
    // public GameObject hoop2;


    private int webs = 3;
    

    private String[] objectives = new[]
    {
        "Move around!", //0
        "Enter the building(press click on the door) and find your apartment up stairs", //1
        "Find your bedroom",//2
        "Find your bedroom closet",//3
        "Go outside the building and search for the box",//4
        "Pick up the box outside and move it on to your bedroom closet",//5
        "Put the painting back",//6
        "Find the main closet and the brow",//7
        "Pick the brow up and clean spider web's 0/3",//8
        "Pick the brow up and clean spider web's 1/3",//9
        "Pick the brow up and clean spider web's 2/3",//10
        "Put the rook back on the chess bored",//11
        "Clean up the table from dining room",//12
        "Move the stuff from downstairs in your closet next to the bedroom 0/3",//13
        "Move the stuff from downstairs in your closet next to the bedroom 1/3",//14
        "Move the stuff from downstairs in your closet next to the bedroom 2/3",//15
        "Repair the damages cosed by the moving team downstairs and put the painting back",//16
        "Put the bench's back 0/2", //17
        "Put the bench's back 1/2" //18
    };

    private bool[] objectivesCompleted = new[] { true, false, false, false, false, false, false, false,
        false, false, false,false,false, false, false, false, false,false,false};
    // private bool[] objectivesCompleted = new[] { true, true, true, true, true, true, true, true,
    //     true, true, true, true,true, true, true, true, true,false,false};
    private int objs = 3;
    // private bool[] objectivesCompleted = new[] { true, true, true, true, true, true, true, true, true, true, true,true,false};


    private void Awake()
    {
        // Set up the singleton instance
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        doorSpiders.active = false;
        roomSpider.active = false;
        outSideSpiders.active = false;
        bedroom.active = false;
        apartment.active = false;
        paint.active = false;
        more.active = false;
        walls.active = false;
        downStairs.active = false;
        
        
        // Update the score UI at the start
        UpdateTaskUI();
        setTaskSpiders();
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        // Add points to the player's score
        playerScore += points;
        
        // Update the score UI
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        // Reset the player's score to 0
        playerScore = 0;

        // Update the score UI
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update the TMP Text component to display the current score
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();
        }
        Debug.Log("Score: " + playerScore);
    }

    void UpdateTaskUI()
    {
        int currentTask = -1;
        for (int i = 0; i < objectivesCompleted.Length; i++)
        {
            if (!objectivesCompleted[i])
            {
                currentTask = i;
                break;
            }
        }
        setTaskSpiders();
        if (currentTask == -1)
        {
            objective.text = "Done";
            return;
        }

        if (currentTask == 7)
        {
            paint.active = true;
            paint2.active = false;
        }
        objective.text = objectives[currentTask];
    }

    public void updateTask(int i)
    {
        objectivesCompleted[i] = true;
        AddScore(10);
        UpdateTaskUI();
    }

    public void webGotBunkedInBack()
    {
        webs--;
        updateTask(webs!=2?webs==1?9:10:8);
    }
    private int currentTask()
    {
        int j = 0;
        foreach (var i in objectivesCompleted)
        {
            if (!i)
            {
                return j;
            }

            j++;
        }

        return 0;
    }
    

    public void setTaskSpiders()
    {
        switch (currentTask())
        {
            case 1:
            {
                doorSpiders.active = true;
                break;
            }
            case 4:
            {
                outSideSpiders.active = true;
                break;
            }
            case 5:
            {
                walls.active = true;
                break;
            }
            case 6:
            {
                apartment.active = true;
                break;
            }
            case 7:
            {
                roomSpider.active = true;
                break;
            }
            case 10:
            {
                bedroom.active = true;
                break;
            }
            case 12:
            {
                more.active = true;
                break;
            }
            case 13:
            {
                downStairs.active = true;
                break;
            }
            default: break;
        }
    }

    public void removeObject()
    {
        objs--;
        if (objs == 0)
        {
            updateTask(12);
        }
    }
}
