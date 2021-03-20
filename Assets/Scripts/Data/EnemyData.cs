// ----------------------------------------------------------------------------
// EnemyData.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Holds necessary data for enemies
// ----------------------------------------------------------------------------

public class EnemyData
{
    public Position position;
    public int lives;
}

public struct Position
{
    public int x;
    public int y;
}