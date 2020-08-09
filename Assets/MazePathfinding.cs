using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MazePathfinding
{
    public static readonly uint STEPMAX = 10000;

    private readonly MazeData mazedata;
    public readonly uint[,] costmap;

    public MazePathfinding(MazeData _mazedata)
    {
        mazedata = _mazedata;
        costmap = new uint[mazedata.size.y, mazedata.size.x];
        for (int y = 0; y < mazedata.size.y; ++y)
        {
            for (int x = 0; x < mazedata.size.x; ++x)
            {
                costmap[y, x] = STEPMAX;
            }
        }
    }

    public List<Vector2Int> Pathfinding(Vector2Int _start, Vector2Int _end)
    {
        costmap[_end.y, _end.x] = 0;

        for (uint nowcost = 0; nowcost < STEPMAX; ++nowcost)
        {
            CalclateCost(nowcost);
        }

        if(costmap[_start.y,_start.x] ==STEPMAX )
        {
            throw new Exception("no path exists");
        }

        var result = new List<Vector2Int> { _start };

        var travelingpos = _start;
        while(costmap[travelingpos.y,travelingpos.x] > 0)
        {
            uint costtmp = costmap[travelingpos.y, travelingpos.x];
            Vector2Int resultpos = travelingpos;
            foreach (var dvec in MazeData.dirVec)
            {
                var tmppos = travelingpos + dvec;
                if(costtmp > costmap[tmppos.y,tmppos.x])
                {
                    costtmp = costmap[tmppos.y, tmppos.x];
                    resultpos = tmppos;
                }
            }
            travelingpos = resultpos;
            result.Add(travelingpos);
        }

        return result;
    }

    void CalclateCost(uint _nowcost)
    {
        for (int y = 0; y < mazedata.size.y; ++y)
        {
            for (int x = 0; x < mazedata.size.x; ++x)
            {
                if (costmap[y, x] == _nowcost)
                {
                    SetValueAround(new Vector2Int(x, y), _nowcost);
                }
            }
        }
    }


    void SetValueAround(Vector2Int _pos, uint _nowcost)
    {
        foreach (var dvec in MazeData.dirVec)
        {
            var tmppos = (_pos + dvec);
            if (mazedata.mazeArray[tmppos.y, tmppos.x] && costmap[tmppos.y, tmppos.x] > _nowcost + 1)
            {
                costmap[tmppos.y, tmppos.x] = _nowcost + 1;
            }
        }
    }

}