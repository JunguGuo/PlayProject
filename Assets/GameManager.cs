using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Variables
    #endregion

    #region Methods
    bool hideCursor = true;
    public static GameManager Instance;

    public static int DEFAULT = 1, MOVE = 2, HOLE = 3;
    public static int MODE = DEFAULT;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            SceneManager.LoadScene("scene");

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("scene");
            MODE = DEFAULT;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("scene");
            MODE = MOVE;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("scene");
            MODE = HOLE;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hideCursor = !hideCursor;
            Cursor.visible = hideCursor;
        }

    }
    // void Update()
    // {
    //     Input.GetKeyDown(KeyCode.Space){

    //     }

    // }
    #endregion
}
