using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    public static Pool currentPool;
	//Grid
    public GameObject gridObject;
    public int gridSize = 100;
    public bool gridGrow = true;

	//Bullets
	public GameObject bulletObject;
	public int bulletSize = 100;
	public bool bulletGrow = true;
    
	public List<GameObject> pooledGrids;
	public List<GameObject> pooledBullets;

    void Awake()
    {
        currentPool = this;
    }

    void Start()
    {
		//Grids
        pooledGrids = new List<GameObject>();
        for (int i=0; i<gridSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(gridObject);
            obj.SetActive(false);
            pooledGrids.Add(obj);
        }
		//Bullets
		pooledBullets = new List<GameObject>();
		for (int i=0; i<bulletSize; i++)
		{
			GameObject obj = (GameObject)Instantiate(bulletObject);
			obj.SetActive(false);
			pooledBullets.Add(obj);
		}
    }

    public GameObject GetPooledGrid()
    {
        for (int i=0; i<pooledGrids.Count; i++)
        {
			if (!pooledGrids[i].activeInHierarchy)
            {
				return pooledGrids[i];
            }
        }

        if (gridGrow)
        {
            GameObject obj = (GameObject)Instantiate(gridObject);
			pooledGrids.Add(obj);
            return obj;
        }
        return null;
    }

	public GameObject GetPooledBullet()
	{
		for (int i=0; i<pooledBullets.Count; i++)
		{
			if (!pooledBullets[i].activeInHierarchy)
			{
				return pooledBullets[i];
			}
		}

		if (bulletGrow)
		{
			GameObject obj = (GameObject)Instantiate(bulletObject);
			pooledBullets.Add(obj);
			return obj;
		}
		return null;
	}
}