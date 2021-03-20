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
    public float bulletSpeed;
    public int lives;

    // Using string instead of enum for colors to easily allow extension
    public int colorIndex;
}