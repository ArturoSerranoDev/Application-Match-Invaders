// ----------------------------------------------------------------------------
// ScoreCalculator.cs
//
// Author: Arturo Serrano
// Date: 20/02/21
//
// Brief: Static class used to calculate the score for each kill
// ----------------------------------------------------------------------------
public static class ScoreCalculator
{
   const int multiplier = 10;
   
   public static int GetScorePerKill(int deaths)
   {
      return deaths * multiplier * Fibonacci(deaths + 1);
   }

   static int Fibonacci(int index)
   {
      int a = 0;
      int b = 1;
      int aux = 0;
      
      for (int i = 0; i < index + 1; i++)  
      {
         aux = a;
         a = b; 
         b = aux + a; 
      }

      return aux;
   }
}
