using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    [SerializeField] Vector2Int size;

    [SerializeField] private Transform cube = default;
    [SerializeField] private LineRenderer lineobj = default;

    [SerializeField] bool saveMazeData;
    [SerializeField] string filename = "mazedata";

    // Start is called before the first frame update
    void Start()
    {
        //迷路生成
        var maze = new MazeGenenrator(size).Genarate();

        //迷路描画
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                if (!maze.mazeArray[y, x])
                {
                    Instantiate(cube, new Vector3(x, -y, 0), Quaternion.identity);
                }
            }
        }

        //経路探索
        var pathdata = new MazePathfinding(maze).Pathfinding(new Vector2Int(1, 1), size - new Vector2Int(2, 2));

        //経路表示
        var pvec3 = new List<Vector3>();
        foreach(var p in pathdata)
        {
            pvec3.Add(new Vector3(p.x, -p.y, 0));
        }
        lineobj.positionCount = pvec3.Count;
        lineobj.SetPositions(pvec3.ToArray());

        //迷路データ保存
        if(saveMazeData) {
            maze.Save(filename);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
