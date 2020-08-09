using System.IO;

using UnityEngine;
using System.Text;
using System;
using UnityEditor.UIElements;

public class MazeData
{
    // true : 道 1
    //falase : 壁 0
    public bool[,] mazeArray = null;

    public Vector2Int size;

    public MazeData()
    {
        size = new Vector2Int(0, 0);
        mazeArray = null;
    }

    public static readonly Vector2Int[] dirVec = {
        new Vector2Int (0, 1),
        new Vector2Int (0, -1),
        new Vector2Int (1, 0),
        new Vector2Int (-1, 0),
    };

    public void Create(Vector2Int _size)
    {
        size = _size;
        mazeArray = new bool[size.y, size.x];
    }

    public void Save(string _filename)
    {
        if (mazeArray == null) throw new NullReferenceException();

        StreamWriter sw = 
            new StreamWriter($@"{_filename}.csv", false, Encoding.GetEncoding("Shift_JIS"));
        sw.WriteLine($"{size.x},{size.y}");

        for (int y = 0; y < size.y; ++y)
        {
            string tmpline = "";
            for (int x = 0; x < size.x; ++x)
            {
                tmpline += (mazeArray[y, x] ? "1" : "0") + ",";
            }
            tmpline = tmpline.TrimEnd(',');
            sw.WriteLine(tmpline);
        }

        sw.Close();
    }

    public void Load(string _filename)
    {
        StreamReader sr = new StreamReader($@"{_filename}.csv");
        var headerline = sr.ReadLine();

        var headrparam = headerline.Split(',');
        size.x = Int32.Parse(headrparam[0]);
        size.y = Int32.Parse(headrparam[1]);
        mazeArray = new bool[size.y, size.x];
        int linecnt = 0;
        while (!sr.EndOfStream)
        {
            var linedata = sr.ReadLine();
            var lineparam = linedata.Split(',');

            int paramcnt = 0;
            foreach (var p in lineparam)
            {
                mazeArray[linecnt, paramcnt] = (p == "1");
                paramcnt += 1;
            }
            linecnt += 1;
        }
        sr.Close();
    }
}
