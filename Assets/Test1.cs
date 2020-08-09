using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField] Vector2Int size;

    [SerializeField] private Transform cube = default;

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



    }

    // Update is called once per frame
    void Update()
    {

    }
}
