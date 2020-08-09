using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test4 : MonoBehaviour
{
    [SerializeField] private Transform cube = default;
    [SerializeField] private LineRenderer lineobj = default;
    [SerializeField] string filename = "mazedata";

    // Start is called before the first frame update
    void Start()
    {
        var maze = new MazeData();
        maze.Load(filename);
        Vector2Int size = maze.size;

        //経路探索
        //var pathdata = new MazePathfinding(maze).Pathfinding(new Vector2Int(1, 1), size - new Vector2Int(2, 2));
        var pathobj = new MazePathfinding(maze);
        var pathdata = pathobj.Pathfinding(new Vector2Int(1, 1), size - new Vector2Int(2, 2));
        var pathcostmap = pathobj.costmap;


        //迷路描画
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                var tmpobj = Instantiate(cube, new Vector3(x, -y, 0), Quaternion.identity);
                if (!maze.mazeArray[y, x])
                {
                    tmpobj.GetComponent<Renderer>().material.color = Color.white;
                }
                else
                {
                    var c = (pathcostmap[y, x] * 0.01f);
                    tmpobj.GetComponent<Renderer>().material.color = new Color(c,c,c);

                }
            }
        }

        //経路表示
        var pvec3 = new List<Vector3>();
        foreach (var p in pathdata)
        {
            pvec3.Add(new Vector3(p.x, -p.y,-0.51f));
        }
        lineobj.positionCount = pvec3.Count;
        lineobj.SetPositions(pvec3.ToArray());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
