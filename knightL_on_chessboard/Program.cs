using System;
using System.Collections;

namespace knightL_on_chessboard
{
    class Coordinates{
        public int x;
        public int y;

        public Coordinates(int x, int y){
            this.x = x;
            this.y = y;
        }

    }

    class Knight{
        int sourceXPosition;
        int sourceYPosition;

        Hashtable posibleCoordinatesToGo;

        public Knight(int sourceXPosition, int sourceYPosition)
        {
            this.sourceXPosition = sourceXPosition;
            this.sourceYPosition = sourceYPosition;
            this.posibleCoordinatesToGo = new Hashtable();
        }

        public Hashtable GenerateAllPosibleCordinatesToGo(int xMovements, int yMovements){
            //first positive posible pair
            int xDestination = sourceXPosition + xMovements;
            int yDestination = sourceYPosition + yMovements;
            AddCoordinateToGo(xDestination, yDestination);

            //first negative posible pair
            xDestination = sourceXPosition - xMovements;
            yDestination = sourceYPosition - yMovements;
            AddCoordinateToGo(xDestination, yDestination);

            //second positive posible pair
            xDestination = sourceXPosition + yMovements;
            yDestination = sourceYPosition + xMovements;
            AddCoordinateToGo(xDestination, yDestination);

            //second negative posible pair
            xDestination = sourceXPosition + yMovements;
            yDestination = sourceYPosition + xMovements;
            AddCoordinateToGo(xDestination, yDestination);

            return posibleCoordinatesToGo;
        }

        public void AddCoordinateToGo(int xDestination, int yDestination)
        {
            if (isValidCoordinate(xDestination, yDestination))
            {
                String key;
                key = xDestination.ToString() + yDestination.ToString();
                if (posibleCoordinatesToGo.Contains(key) == false)
                {
                    this.posibleCoordinatesToGo.Add(key, new Coordinates(xDestination, yDestination));
                }
            }
        }

        public bool isValidCoordinate(int xPosition, int yPosition){
            return (xPosition < 0 || xPosition > 4 || yPosition < 0 || yPosition > 4)? false : true;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var knight = new Knight(0, 0);
            ICollection posibleCoordinatesToGo = knight.GenerateAllPosibleCordinatesToGo(1, 2).Values;


            foreach (Coordinates coordinate in posibleCoordinatesToGo){
                Console.WriteLine(coordinate.x.ToString() + coordinate.y.ToString());
            }

        }
    }
}
