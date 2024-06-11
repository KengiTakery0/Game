using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public int ID; 
	void ReplaceBlocks(int x, int y, int XX, int YY)
	{
		GameControl.grid[x,y].transform.position = GameControl.position[XX,YY];
		GameControl.grid[XX,YY] = GameControl.grid[x,y];
		GameControl.grid[x,y] = null;
		GameControl.GameFinish();
	}

	void OnMouseDown()
	{
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(GameControl.grid[x,y])
				{
					if(GameControl.grid[x,y].GetComponent<Puzzle>().ID == ID)
					{
						if(x > 0 && GameControl.grid[x-1,y] == null)
						{
							ReplaceBlocks(x,y,x-1,y);
							return;
						}
						else if(x < 3 && GameControl.grid[x+1,y] == null)
						{
							ReplaceBlocks(x,y,x+1,y);
							return;
						}
					}
				}
				if(GameControl.grid[x,y])
				{
					if(GameControl.grid[x,y].GetComponent<Puzzle>().ID == ID)
					{
						if(y > 0 && GameControl.grid[x,y-1] == null)
						{
							ReplaceBlocks(x,y,x,y-1);
							return;
						}
						else if(y < 3 && GameControl.grid[x,y+1] == null)
						{
							ReplaceBlocks(x,y,x,y+1);
							return;
						}
					}
				}
			}
		}
	}
}
