using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenenrator
{
    public readonly MazeData mazedata;

    public MazeGenenrator(Vector2Int size)
    {
        mazedata = new MazeData();
        mazedata.Create(size);
    }

    public MazeData Genarate()
    {
        var startpos = 
            new Vector2Int(Random.Range(0, mazedata.size.x / 2) * 2 + 1, Random.Range(0, mazedata.size.y / 2) * 2 + 1);
        mazedata.mazeArray[startpos.y, startpos.x] = true;

        var backpos = new Stack<Vector2Int>();
        backpos.Push(startpos);

        while (backpos.Count > 0)
        {
            var nowpos = backpos.Peek();
            if (CheckDirection(out Vector2Int outpos, nowpos))
            {
                var tmppos = nowpos + outpos;
                mazedata.mazeArray[tmppos.y, tmppos.x] = true;
                tmppos = nowpos + (outpos * 2);
                mazedata.mazeArray[tmppos.y, tmppos.x] = true;
                backpos.Push(tmppos);
            }
            else
            {
                backpos.Pop();
            }
        }
        return mazedata;
    }

    bool CheckDirection(out Vector2Int _outpos, Vector2Int _nowpos)
    {
        var ans = new List<Vector2Int>();
        foreach (var dvec in MazeData.dirVec)
        {
            var tmp = (_nowpos + dvec);
            if (tmp.x >= 1 && tmp.x <= mazedata.size.x - 2 && tmp.y >= 1 && tmp.y <= mazedata.size.y - 2)
            {
                tmp = _nowpos + (dvec * 2);
                if (!mazedata.mazeArray[tmp.y, tmp.x])
                {
                    ans.Add(dvec);
                }
            }
        }
        if (ans.Count == 0)
        {
            _outpos = new Vector2Int();
            return false;
        }
        _outpos = ans[Random.Range(0, ans.Count)];
        return true;
    }


}