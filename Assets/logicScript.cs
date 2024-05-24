using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
{

    //public logicScript logic;
    public Vector4[] grid = { new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0) };
    private float[][][] pos = new float[4][][];
    public GameObject[][] refe = new GameObject[4][];
    private int emptyCellCount = 16;
    public bool gameOver = false;
    public GameObject cell;
    public GameObject gameOverMessage;
    public Text scoreText;
    private int scoreNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        //logic = GameObject.FindGameObjectWithTag("logicTag").GetComponent<logicScript>();
        gameOverMessage.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            pos[i] = new float[4][];
            for (int j = 0; j < 4; j++)
            {
                pos[i][j] = new float[2];
            }
        }
        initPos();
        for (int i = 0; i < 4; i++)
        {
            refe[i] = new GameObject[4];
            /*
            for (int k = 0; k < 4; k++)
            {
                refe[i][k] = null;
            }*/
            
        }
        fillRandomCell();
        fillRandomCell();
    }

    // Update is called once per frame
    void initPos()
    {
        //x coordinates
        pos[0][0][0] = -7.05f;
        pos[1][0][0] = -7.05f;
        pos[2][0][0] = -7.05f;
        pos[3][0][0] = -7.05f;
        pos[0][1][0] = -2.24f;
        pos[1][1][0] = -2.24f;
        pos[2][1][0] = -2.24f;
        pos[3][1][0] = -2.24f;
        pos[0][2][0] = 2.64f;
        pos[1][2][0] = 2.64f;
        pos[2][2][0] = 2.64f;
        pos[3][2][0] = 2.64f;
        pos[0][3][0] = 7.42f;
        pos[1][3][0] = 7.42f;
        pos[2][3][0] = 7.42f;
        pos[3][3][0] = 7.42f;
        //y coordinates
        pos[0][0][1] = 8.77f;
        pos[0][1][1] = 8.77f;
        pos[0][2][1] = 8.77f;
        pos[0][3][1] = 8.77f;
        pos[1][0][1] = 3.97f;
        pos[1][1][1] = 3.97f;
        pos[1][2][1] = 3.97f;
        pos[1][3][1] = 3.97f;
        pos[2][0][1] = -0.84f;
        pos[2][1][1] = -0.84f;
        pos[2][2][1] = -0.84f;
        pos[2][3][1] = -0.84f;
        pos[3][0][1] = -5.65f;
        pos[3][1][1] = -5.65f;
        pos[3][2][1] = -5.65f;
        pos[3][3][1] = -5.65f;
    }
    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
            {
                moveLeft();
                fillRandomCell();
                updateScore();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) == true)
            {
                moveRight();
                fillRandomCell();
                updateScore();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                moveUp();
                fillRandomCell();
                updateScore();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
            {
                moveDown();
                fillRandomCell();
                updateScore();
            }
        }
        else
        {
            gameOverMessage.SetActive(true);
        }
    }
    void fillRandomCell()
    {
        if (emptyCellCount == 0)
        {
            gameOver = true;
            return;
        }
        int r = Random.Range(0, 4);
        int c = Random.Range(0, 4);
        while (grid[r][c] != 0)
        {
            r = Random.Range(0, 4);
            c = Random.Range(0, 4);
        }
        int num = Random.Range(1, 3);
        if (num==1)
        {
            grid[r][c] = 2;
            refe[r][c] = Instantiate(cell, new Vector3(pos[r][c][0], pos[r][c][1], 0), transform.rotation);
            //Debug.Log(refe[r][c]);
            //Instantiate(cell, new Vector3(pos[r][c][0], pos[r][c][1], 0), transform.rotation).GetComponent<cellScript>().updateSprite(2);
            refe[r][c].GetComponent<cellScript>().updateSprite(2);
            emptyCellCount--;
        }
        else
        {
            grid[r][c] = 4;
            refe[r][c] = Instantiate(cell, new Vector3(pos[r][c][0], pos[r][c][1], 0), transform.rotation);
            //Debug.Log(refe[r][c]);
            refe[r][c].GetComponent<cellScript>().updateSprite(4);
            emptyCellCount--;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void resetScore()
    {
        scoreNum = 0;
        scoreText.text = scoreNum.ToString();
    }

    void updateScore()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int k = 0; k < 4; k++)
            {
                if (grid[i][k] > scoreNum) scoreNum = (int)grid[i][k];
            }

        }
        scoreText.text = scoreNum.ToString();
    }

    public void restartGame()
    {
        gameOverMessage.SetActive(false);
        gameOver = false;
        emptyCellCount = 16;
        for (int i = 0; i < 4; i++)
        {
            for (int k = 0; k < 4; k++)
            {
                grid[i][k] = 0;
                if (refe[i][k] != null)
                {
                    Destroy(refe[i][k]);
                    refe[i][k] = null;
                }
            }

        }
    }

    void moveUp()
    {
        for (int i = 0; i < 4; i++)
        {
            //2
            if (grid[1][i] != 0)
            {
                if (grid[0][i] == 0)
                {
                    grid[0][i] = grid[1][i];
                    grid[1][i] = 0;
                    refe[0][i] = Instantiate(cell, new Vector3(pos[0][i][0], pos[0][i][1], 0), transform.rotation);
                    refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                    Destroy(refe[1][i]);
                    refe[1][i] = null;
                    //Debug.Log("1");
                }
                else if (grid[0][i] == grid[1][i])
                {
                    grid[0][i] += grid[1][i];
                    grid[1][i] = 0;
                    refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                    Destroy(refe[1][i]);
                    refe[1][i] = null;
                    emptyCellCount++;
                    //Debug.Log("2");
                }
            }
            //3
            if (grid[2][i] != 0)
            {
                if (grid[1][i] == 0)
                {
                    if (grid[0][i] == 0)
                    {
                        grid[0][i] = grid[2][i];
                        grid[2][i] = 0;
                        refe[0][i] = Instantiate(cell, new Vector3(pos[0][i][0], pos[0][i][1], 0), transform.rotation);
                        refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                        Destroy(refe[2][i]);
                        refe[2][i] = null;
                        //Debug.Log("3");
                    }
                    else if (grid[0][i] == grid[2][i])
                    {
                        grid[0][i] += grid[2][i];
                        grid[2][i] = 0;
                        refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                        Destroy(refe[2][i]);
                        refe[2][i] = null;
                        emptyCellCount++;
                        //Debug.Log("4");
                    }
                    else
                    {
                        grid[1][i] = grid[2][i];
                        grid[2][i] = 0;
                        refe[1][i] = Instantiate(cell, new Vector3(pos[1][i][0], pos[1][i][1], 0), transform.rotation);
                        refe[1][i].GetComponent<cellScript>().updateSprite((int)grid[1][i]);
                        Destroy(refe[2][i]);
                        refe[2][i] = null;
                        //Debug.Log("5");
                    }
                }
                else if (grid[1][i] == grid[2][i])
                {
                    grid[1][i] += grid[2][i];
                    grid[2][i] = 0;
                    refe[1][i].GetComponent<cellScript>().updateSprite((int)grid[1][i]);
                    Destroy(refe[2][i]);
                    refe[2][i] = null;
                    emptyCellCount++;
                    //Debug.Log("6");
                }
            }

            //4
            if (grid[3][i] != 0)
            {
                if (grid[2][i] == 0)
                {
                    if (grid[1][i] == 0)
                    {
                        if (grid[0][i] == 0)
                        {
                            grid[0][i] = grid[3][i];
                            grid[3][i] = 0;
                            refe[0][i] = Instantiate(cell, new Vector3(pos[0][i][0], pos[0][i][1], 0), transform.rotation);
                            refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                            Destroy(refe[3][i]);
                            refe[3][i] = null;
                            //Debug.Log("7");
                        }
                        else if (grid[0][i] == grid[3][i])
                        {
                            grid[0][i] += grid[3][i];
                            grid[3][i] = 0;
                            refe[0][i].GetComponent<cellScript>().updateSprite((int)grid[0][i]);
                            Destroy(refe[3][i]);
                            refe[3][i] = null;
                            emptyCellCount++;
                            //Debug.Log("8");
                        }
                        else
                        {
                            grid[1][i] = grid[3][i];
                            grid[3][i] = 0;
                            refe[1][i] = Instantiate(cell, new Vector3(pos[1][i][0], pos[1][i][1], 0), transform.rotation);
                            refe[1][i].GetComponent<cellScript>().updateSprite((int)grid[1][i]);
                            Destroy(refe[3][i]);
                            refe[3][i] = null;
                            //Debug.Log("9");
                        }
                    }
                    else if (grid[1][i] == grid[3][i])
                    {
                        grid[1][i] += grid[3][i];
                        grid[3][i] = 0;
                        refe[1][i].GetComponent<cellScript>().updateSprite((int)grid[1][i]);
                        Destroy(refe[3][i]);
                        refe[3][i] = null;
                        emptyCellCount++;
                        //Debug.Log("10");
                    }
                    else
                    {
                        grid[2][i] = grid[3][i];
                        grid[3][i] = 0;
                        refe[2][i] = Instantiate(cell, new Vector3(pos[2][i][0], pos[2][i][1], 0), transform.rotation);
                        refe[2][i].GetComponent<cellScript>().updateSprite((int)grid[2][i]);
                        Destroy(refe[3][i]);
                        refe[3][i] = null;
                        //Debug.Log("11");
                    }
                }
                else if (grid[2][i] == grid[3][i])
                {
                    grid[2][i] += grid[3][i];
                    grid[3][i] = 0;
                    refe[2][i].GetComponent<cellScript>().updateSprite((int)grid[2][i]);
                    Destroy(refe[3][i]);
                    refe[3][i] = null;
                    emptyCellCount++;
                    //Debug.Log("12");
                }
            }
        }
    }

    void moveLeft()
    {
        for (int i = 0; i < 4; i++)
        {
            //2
            if (grid[i][1] != 0)
            {
                if (grid[i][0] == 0)
                {
                    grid[i][0] = grid[i][1];
                    grid[i][1] = 0;
                    refe[i][0] = Instantiate(cell, new Vector3(pos[i][0][0], pos[i][0][1], 0), transform.rotation);
                    refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                    Destroy(refe[i][1]);
                    refe[i][1] = null;
                }
                else if (grid[i][0] == grid[i][1])
                {
                    grid[i][0] += grid[i][1];
                    grid[i][1] = 0;
                    refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                    Destroy(refe[i][1]);
                    refe[i][1] = null;
                    emptyCellCount++;
                }
            }
            //3
            if (grid[i][2] != 0)
            {
                if (grid[i][1] == 0)
                {
                    if (grid[i][0] == 0)
                    {
                        grid[i][0] = grid[i][2];
                        grid[i][2] = 0;
                        refe[i][0] = Instantiate(cell, new Vector3(pos[i][0][0], pos[i][0][1], 0), transform.rotation);
                        refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                        Destroy(refe[i][2]);
                        refe[i][2] = null;
                    }
                    else if (grid[i][0] == grid[i][2])
                    {
                        grid[i][0] += grid[i][2];
                        grid[i][2] = 0;
                        refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                        Destroy(refe[i][2]);
                        refe[i][2] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[i][1] = grid[i][2];
                        grid[i][2] = 0;
                        refe[i][1] = Instantiate(cell, new Vector3(pos[i][1][0], pos[i][1][1], 0), transform.rotation);
                        refe[i][1].GetComponent<cellScript>().updateSprite((int)grid[i][1]);
                        Destroy(refe[i][2]);
                        refe[i][2] = null;
                    }
                }
                else if (grid[i][1] == grid[i][2])
                {
                    grid[i][1] += grid[i][2];
                    grid[i][2] = 0;
                    refe[i][1].GetComponent<cellScript>().updateSprite((int)grid[i][1]);
                    Destroy(refe[i][2]);
                    refe[i][2] = null;
                    emptyCellCount++;
                }
            }
            
            //4
            if (grid[i][3] != 0)
            {
                if (grid[i][2] == 0)
                {
                    if (grid[i][1] == 0)
                    {
                        if (grid[i][0] == 0)
                        {
                            grid[i][0] = grid[i][3];
                            grid[i][3] = 0;
                            refe[i][0] = Instantiate(cell, new Vector3(pos[i][0][0], pos[i][0][1], 0), transform.rotation);
                            refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                            Destroy(refe[i][3]);
                            refe[i][3] = null;
                        }
                        else if (grid[i][0] == grid[i][3])
                        {
                            grid[i][0] += grid[i][3];
                            grid[i][3] = 0;
                            refe[i][0].GetComponent<cellScript>().updateSprite((int)grid[i][0]);
                            Destroy(refe[i][3]);
                            refe[i][3] = null;
                            emptyCellCount++;
                        }
                        else
                        {
                            grid[i][1] = grid[i][3];
                            grid[i][3] = 0;
                            refe[i][1] = Instantiate(cell, new Vector3(pos[i][1][0], pos[i][1][1], 0), transform.rotation);
                            refe[i][1].GetComponent<cellScript>().updateSprite((int)grid[i][1]);
                            Destroy(refe[i][3]);
                            refe[i][3] = null;
                        }
                    }
                    else if (grid[i][1] == grid[i][3])
                    {
                        grid[i][1] += grid[i][3];
                        grid[i][3] = 0;
                        refe[i][1].GetComponent<cellScript>().updateSprite((int)grid[i][1]);
                        Destroy(refe[i][3]);
                        refe[i][3] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[i][2] = grid[i][3];
                        grid[i][3] = 0;
                        refe[i][2] = Instantiate(cell, new Vector3(pos[i][2][0], pos[i][2][1], 0), transform.rotation);
                        refe[i][2].GetComponent<cellScript>().updateSprite((int)grid[i][2]);
                        Destroy(refe[i][3]);
                        refe[i][3] = null;
                    }
                }
                else if (grid[i][2] == grid[i][3])
                {
                    grid[i][2] += grid[i][3];
                    grid[i][3] = 0;
                    refe[i][2].GetComponent<cellScript>().updateSprite((int)grid[i][2]);
                    Destroy(refe[i][3]);
                    refe[i][3] = null;
                    emptyCellCount++;
                }
            }
            
        }
    }

    void moveDown()
    {
        for (int i = 0; i < 4; i++)
        {
            // Process the second row from the bottom (index 2)
            if (grid[2][i] != 0)
            {
                if (grid[3][i] == 0)
                {
                    grid[3][i] = grid[2][i];
                    grid[2][i] = 0;
                    refe[3][i] = Instantiate(cell, new Vector3(pos[3][i][0], pos[3][i][1], 0), transform.rotation);
                    refe[3][i].GetComponent<cellScript>().updateSprite((int)grid[3][i]);
                    Destroy(refe[2][i]);
                    refe[2][i] = null;
                }
                else if (grid[3][i] == grid[2][i])
                {
                    grid[3][i] += grid[2][i];
                    grid[2][i] = 0;
                    refe[3][i].GetComponent<cellScript>().updateSprite((int)(grid[3][i]));
                    Destroy(refe[2][i]);
                    refe[2][i] = null;
                    emptyCellCount++;
                }
            }

            // Process the third row from the bottom (index 1)
            if (grid[1][i] != 0)
            {
                if (grid[2][i] == 0)
                {
                    if (grid[3][i] == 0)
                    {
                        grid[3][i] = grid[1][i];
                        grid[1][i] = 0;
                        refe[3][i] = Instantiate(cell, new Vector3(pos[3][i][0], pos[3][i][1], 0), transform.rotation);
                        refe[3][i].GetComponent<cellScript>().updateSprite((int)grid[3][i]);
                        Destroy(refe[1][i]);
                        refe[1][i] = null;
                    }
                    else if (grid[3][i] == grid[1][i])
                    {
                        grid[3][i] += grid[1][i];
                        grid[1][i] = 0;
                        refe[3][i].GetComponent<cellScript>().updateSprite((int)(grid[3][i]));
                        Destroy(refe[1][i]);
                        refe[1][i] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[2][i] = grid[1][i];
                        grid[1][i] = 0;
                        refe[2][i] = Instantiate(cell, new Vector3(pos[2][i][0], pos[2][i][1], 0), transform.rotation);
                        refe[2][i].GetComponent<cellScript>().updateSprite((int)grid[2][i]);
                        Destroy(refe[1][i]);
                        refe[1][i] = null;
                    }
                }
                else if (grid[2][i] == grid[1][i])
                {
                    grid[2][i] += grid[1][i];
                    grid[1][i] = 0;
                    refe[2][i].GetComponent<cellScript>().updateSprite((int)(grid[2][i]));
                    Destroy(refe[1][i]);
                    refe[1][i] = null;
                    emptyCellCount++;
                }
            }

            // Process the fourth row from the bottom (index 0)
            if (grid[0][i] != 0)
            {
                if (grid[1][i] == 0)
                {
                    if (grid[2][i] == 0)
                    {
                        if (grid[3][i] == 0)
                        {
                            grid[3][i] = grid[0][i];
                            grid[0][i] = 0;
                            refe[3][i] = Instantiate(cell, new Vector3(pos[3][i][0], pos[3][i][1], 0), transform.rotation);
                            refe[3][i].GetComponent<cellScript>().updateSprite((int)grid[3][i]);
                            Destroy(refe[0][i]);
                            refe[0][i] = null;
                        }
                        else if (grid[3][i] == grid[0][i])
                        {
                            grid[3][i] += grid[0][i];
                            grid[0][i] = 0;
                            refe[3][i].GetComponent<cellScript>().updateSprite((int)(grid[3][i]));
                            Destroy(refe[0][i]);
                            refe[0][i] = null;
                            emptyCellCount++;
                        }
                        else
                        {
                            grid[2][i] = grid[0][i];
                            grid[0][i] = 0;
                            refe[2][i] = Instantiate(cell, new Vector3(pos[2][i][0], pos[2][i][1], 0), transform.rotation);
                            refe[2][i].GetComponent<cellScript>().updateSprite((int)grid[2][i]);
                            Destroy(refe[0][i]);
                            refe[0][i] = null;
                        }
                    }
                    else if (grid[2][i] == grid[0][i])
                    {
                        grid[2][i] += grid[0][i];
                        grid[0][i] = 0;
                        refe[2][i].GetComponent<cellScript>().updateSprite((int)(grid[2][i]));
                        Destroy(refe[0][i]);
                        refe[0][i] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[1][i] = grid[0][i];
                        grid[0][i] = 0;
                        refe[1][i] = Instantiate(cell, new Vector3(pos[1][i][0], pos[1][i][1], 0), transform.rotation);
                        refe[1][i].GetComponent<cellScript>().updateSprite((int)grid[1][i]);
                        Destroy(refe[0][i]);
                        refe[0][i] = null;
                    }
                }
                else if (grid[1][i] == grid[0][i])
                {
                    grid[1][i] += grid[0][i];
                    grid[0][i] = 0;
                    refe[1][i].GetComponent<cellScript>().updateSprite((int)(grid[1][i]));
                    Destroy(refe[0][i]);
                    refe[0][i] = null;
                    emptyCellCount++;
                }
            }
        }
    }

    void moveRight()
    {
        for (int i = 0; i < 4; i++)
        {
            // Process the second column from the right (index 2)
            if (grid[i][2] != 0)
            {
                if (grid[i][3] == 0)
                {
                    grid[i][3] = grid[i][2];
                    grid[i][2] = 0;
                    refe[i][3] = Instantiate(cell, new Vector3(pos[i][3][0], pos[i][3][1], 0), transform.rotation);
                    refe[i][3].GetComponent<cellScript>().updateSprite((int)grid[i][3]);
                    Destroy(refe[i][2]);
                    refe[i][2] = null;
                }
                else if (grid[i][3] == grid[i][2])
                {
                    grid[i][3] += grid[i][2];
                    grid[i][2] = 0;
                    refe[i][3].GetComponent<cellScript>().updateSprite((int)(grid[i][3]));
                    Destroy(refe[i][2]);
                    refe[i][2] = null;
                    emptyCellCount++;
                }
            }

            // Process the third column from the right (index 1)
            if (grid[i][1] != 0)
            {
                if (grid[i][2] == 0)
                {
                    if (grid[i][3] == 0)
                    {
                        grid[i][3] = grid[i][1];
                        grid[i][1] = 0;
                        refe[i][3] = Instantiate(cell, new Vector3(pos[i][3][0], pos[i][3][1], 0), transform.rotation);
                        refe[i][3].GetComponent<cellScript>().updateSprite((int)grid[i][3]);
                        Destroy(refe[i][1]);
                        refe[i][1] = null;
                    }
                    else if (grid[i][3] == grid[i][1])
                    {
                        grid[i][3] += grid[i][1];
                        grid[i][1] = 0;
                        refe[i][3].GetComponent<cellScript>().updateSprite((int)(grid[i][3]));
                        Destroy(refe[i][1]);
                        refe[i][1] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[i][2] = grid[i][1];
                        grid[i][1] = 0;
                        refe[i][2] = Instantiate(cell, new Vector3(pos[i][2][0], pos[i][2][1], 0), transform.rotation);
                        refe[i][2].GetComponent<cellScript>().updateSprite((int)grid[i][2]);
                        Destroy(refe[i][1]);
                        refe[i][1] = null;
                    }
                }
                else if (grid[i][2] == grid[i][1])
                {
                    grid[i][2] += grid[i][1];
                    grid[i][1] = 0;
                    refe[i][2].GetComponent<cellScript>().updateSprite((int)(grid[i][2]));
                    Destroy(refe[i][1]);
                    refe[i][1] = null;
                    emptyCellCount++;
                }
            }

            // Process the fourth column from the right (index 0)
            if (grid[i][0] != 0)
            {
                if (grid[i][1] == 0)
                {
                    if (grid[i][2] == 0)
                    {
                        if (grid[i][3] == 0)
                        {
                            grid[i][3] = grid[i][0];
                            grid[i][0] = 0;
                            refe[i][3] = Instantiate(cell, new Vector3(pos[i][3][0], pos[i][3][1], 0), transform.rotation);
                            refe[i][3].GetComponent<cellScript>().updateSprite((int)grid[i][3]);
                            Destroy(refe[i][0]);
                            refe[i][0] = null;
                        }
                        else if (grid[i][3] == grid[i][0])
                        {
                            grid[i][3] += grid[i][0];
                            grid[i][0] = 0;
                            refe[i][3].GetComponent<cellScript>().updateSprite((int)(grid[i][3]));
                            Destroy(refe[i][0]);
                            refe[i][0] = null;
                            emptyCellCount++;
                        }
                        else
                        {
                            grid[i][2] = grid[i][0];
                            grid[i][0] = 0;
                            refe[i][2] = Instantiate(cell, new Vector3(pos[i][2][0], pos[i][2][1], 0), transform.rotation);
                            refe[i][2].GetComponent<cellScript>().updateSprite((int)grid[i][2]);
                            Destroy(refe[i][0]);
                            refe[i][0] = null;
                        }
                    }
                    else if (grid[i][2] == grid[i][0])
                    {
                        grid[i][2] += grid[i][0];
                        grid[i][0] = 0;
                        refe[i][2].GetComponent<cellScript>().updateSprite((int)(grid[i][2]));
                        Destroy(refe[i][0]);
                        refe[i][0] = null;
                        emptyCellCount++;
                    }
                    else
                    {
                        grid[i][1] = grid[i][0];
                        grid[i][0] = 0;
                        refe[i][1] = Instantiate(cell, new Vector3(pos[i][1][0], pos[i][1][1], 0), transform.rotation);
                        refe[i][1].GetComponent<cellScript>().updateSprite((int)grid[i][1]);
                        Destroy(refe[i][0]);
                        refe[i][0] = null;
                    }
                }
                else if (grid[i][1] == grid[i][0])
                {
                    grid[i][1] += grid[i][0];
                    grid[i][0] = 0;
                    refe[i][1].GetComponent<cellScript>().updateSprite((int)(grid[i][1]));
                    Destroy(refe[i][0]);
                    refe[i][0] = null;
                    emptyCellCount++;
                }
            }
        }
    }

}
