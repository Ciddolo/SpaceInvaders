using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    enum FieldsValues
    {
        verticalEdges = 50,
        horizontalEdge = 100,
    }
    enum PlayersValues
    {
        lives = 3,
        movementSpeed = 200,
    }
    enum PlayersBulletsValues
    {
        sizeOfArray = 10,
        movementSpeed = 600,
    }
    enum EnemiessArray
    {
        rows = 5,
        columns = 11,
        sizeOfArray = 55,
    }
    enum EnemiessValues
    {
        mothershipsMovementSpeed = 100,
        movementSpeed = 300,
        speedIncrease = 2,
        standardWidth = 26,
        standardHeight = 16,
    }
    enum EnemysBulletsValues
    {
        sizeOfArray = 10,
        movementSpeed = 200,
    }
    enum BricksValues
    {
        side = 10,
    }
    enum BarriersValues
    {
        sizeOfArray = 4,
        side = 80,
        rowsAndColumns = 8,
        padding = 50,
        bricksInOneBarrier = 52,
    }
}