using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //spawns asteroid
        spawnAsteroids();

        //Spawn SpaceShip in center
        Instantiate(spaceShip, new Vector3(getCenterGrid().x + 1f, getCenterGrid().y - 2f, getCenterGrid().z), transform.rotation);
    }

    #endregion

    #region  Fields
    [Header("Grid")]
    public int Rows;
    public int Columns;
    public float TileWidth = 1;
    public float TileHeight = 1;
    Vector3 centerGrid;

    [Space(5)]
    [Header("Asteroid Parametrs")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject asteroidSpecial;
    private float offsetPosition = 5f;
    [SerializeField] private Transform parentAsteroid;
    [Space(5)]
    [Header("SpaceShip")]
    [SerializeField] private GameObject spaceShip;

    [Space(5)]
    [Header("UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverPanel;

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;

            //Setting Score to Text
            scoreText.text = "Score:" + score;
        }
    }
    #endregion

    #region  Draw Grid 
    private void OnDrawGizmosSelected()
    {
        // store map width, height and position
        var mapWidth = this.Columns * this.TileWidth;
        var mapHeight = this.Rows * this.TileHeight;
        var position = this.transform.position;

        // draw layer border
        Gizmos.color = Color.white;
        Gizmos.DrawLine(position, position + new Vector3(mapWidth * offsetPosition, 0, 0));
        Gizmos.DrawLine(position, position + new Vector3(0, mapHeight * offsetPosition, 0));
        Gizmos.DrawLine(position + new Vector3(mapWidth * offsetPosition, 0, 0), position + new Vector3(mapWidth * offsetPosition, mapHeight * offsetPosition, 0));
        Gizmos.DrawLine(position + new Vector3(0, mapHeight * offsetPosition, 0), position + new Vector3(mapWidth * offsetPosition, mapHeight * offsetPosition, 0));

        Vector3 widhtLine = position + new Vector3(mapWidth * offsetPosition, 0, 0);
        Vector3 heightLine = position + new Vector3(0, mapHeight * offsetPosition, 0);

        //set center sphere
        centerGrid = new Vector3(widhtLine.x / 2, heightLine.y / 2, 0);

        //draw center grid sphere
        Gizmos.DrawSphere(new Vector3(widhtLine.x / 2, heightLine.y / 2, 0), 1);

        // draw tile cells
        Gizmos.color = Color.grey;
        for (float i = 1; i < this.Columns; i++)
        {
            Gizmos.DrawLine(position + new Vector3(i * this.TileWidth * offsetPosition, 0, 0), position + new Vector3(i * this.TileWidth * offsetPosition, mapHeight * offsetPosition, 0));
            // Debug.Log(i);
        }

        for (float i = 1; i < this.Rows; i++)
        {
            Gizmos.DrawLine(position + new Vector3(0, i * this.TileHeight * offsetPosition, 0), position + new Vector3(mapWidth * offsetPosition, i * this.TileHeight * offsetPosition, 0));
        }

    }

    #endregion

    // Instatiate Objects
    void spawnAsteroids()
    {
        // offset between each objects
        float Xoffset = offsetPosition;
        float Yoffest = offsetPosition;

        //Rows
        for (int i = 0; i < Columns; i++)
        {
            Xoffset = Xoffset + offsetPosition;

            //collums
            for (int a = 0; a < Rows; a++)
            {

                GameObject asteroid = Instantiate(asteroidPrefab);

                // Set position inside each cell center
                asteroid.transform.position = new Vector3(this.transform.position.x + Xoffset - 7.5f, this.transform.position.y + Yoffest - 2.5f, this.transform.position.z);
                Yoffest = Yoffest + offsetPosition;

                //set parent asteroid
                asteroid.transform.SetParent(parentAsteroid);
            }
            Yoffest = offsetPosition;
        }

        //Spawn Special Astetoid in center grid
        GameObject specialAsteroid = Instantiate(asteroidSpecial, getCenterGrid(), transform.rotation);
        specialAsteroid.transform.SetParent(parentAsteroid);

    }

    //Geting center grid
    Vector3 getCenterGrid()
    {
        var mapWidth = this.Columns * this.TileWidth;
        var mapHeight = this.Rows * this.TileHeight;
        var position = this.transform.position;

        Vector3 widhtLine = position + new Vector3(mapWidth * offsetPosition, 0, 0);
        Vector3 heightLine = position + new Vector3(0, mapHeight * offsetPosition, 0);

        return new Vector3(widhtLine.x / 2, heightLine.y / 2 - (offsetPosition / 2), 0);
    }

    

    //game Over
    public void GameOver()
    {
        //stop time
        Time.timeScale = 0;
        //enable UI Panel Game Over 
        gameOverPanel.SetActive(true);
    }

    //restart game
    public void RestartGame ()
    {
        //CleanAll asteroids
        Destroy(parentAsteroid.gameObject);
        
        //Set SpaceShip in center Grid
        GameObject.FindObjectOfType<SpaceShipController>().transform.SetPositionAndRotation(new Vector3(getCenterGrid().x + 1f, getCenterGrid().y - 2f, getCenterGrid().z),transform.rotation);

        //set score to zero
        Score = 0;

        //Collection Astereroids
       GameObject parent = new GameObject("Collection");

       //set parentAsteroids
       parentAsteroid = parent.transform;

       //spawn asteroid
       spawnAsteroids();


       Time.timeScale = 1;
       gameOverPanel.SetActive(false);

    }
}






