using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public GameObject[] _puzzle; 

	public float startPosX = -6f;
	public float startPosY = 6f;

	public float outX = 1.1f;
	public float outY = 1.1f;

	public static GameObject[,] grid;
	public static Vector3[,] position;
	private GameObject[] puzzleRandom;
	public static bool win;
	[SerializeField] MainManager mainManager;
	static GameControl control;
    private void Awake()
    {
		control = this;
    }
    void Start ()
	{ 
		puzzleRandom = new GameObject[_puzzle.Length];
		float posXreset = startPosX;
		position = new Vector3[4,4];
		for(int y = 0; y < 4; y++)
		{
			startPosY -= outY;
			for(int x = 0; x < 4; x++)
			{
				startPosX += outX;
				position[x,y] = new Vector3(startPosX, startPosY, 0);
			}
			startPosX = posXreset;
		}
		StartNewGame();
	}
	public void StartNewGame()
	{
		win = false;
		RandomPuzzle();
	}
	public void ExitGame()
	{
		Save();
		Application.Quit();
	}
	void Save()
	{
		string content = string.Empty;
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(content.Length > 0) content += "|";
				if(grid[x,y]) content += grid[x,y].GetComponent<Puzzle>().ID.ToString(); else content += "null";
			}
		}
		
	}

	void CreatePuzzle()
	{
		if(transform.childCount > 0)
		{
			for(int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		int i = 0;
		grid = new GameObject[4,4];
		int h = Random.Range(0,3);
		int v = Random.Range(0,3);
		GameObject clone = new GameObject();
		grid[h,v] = clone; 
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(grid[x,y] == null)
				{
					grid[x,y] = Instantiate(puzzleRandom[i], position[x,y], Quaternion.identity) as GameObject;
					grid[x,y].name = "ID-"+i;
					grid[x,y].transform.parent = transform;
					i++;
				}
			}
		}
		Destroy(clone); 
		for(int q = 0; q < _puzzle.Length; q++)
		{
			Destroy(puzzleRandom[q]);
		}
	}
	static public void GameFinish()
	{
		int i = 1;
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(grid[x,y]) { if(grid[x,y].GetComponent<Puzzle>().ID == i) i++; } else i--;
			}
		}
		if(i == 15)
		{
			for(int y = 0; y < 4; y++)
			{
				for(int x = 0; x < 4; x++)
				{
					if(grid[x,y]) Destroy(grid[x,y].GetComponent<Puzzle>());
				}
			}
			win = true;
        }
		if(win) control.mainManager.EndGame();
    }
	void RandomPuzzle()
	{
		int[] tmp = new int[_puzzle.Length];
		for(int i = 0; i < _puzzle.Length; i++)
		{
			tmp[i] = 1;
		}
		int c = 0;
		while(c < _puzzle.Length)
		{
			int r = Random.Range(0, _puzzle.Length);
			if(tmp[r] == 1)
			{ 
				puzzleRandom[c] = Instantiate(_puzzle[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
				tmp[r] = 0;
				c++;
			}
		}
		CreatePuzzle();
	}
}
