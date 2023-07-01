using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointData
{
    public int points;

    public PointData(PlayerControllerRigid player)
    {
        points = player.points;
    }
}
