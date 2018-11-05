using System;
using System.Collections;

namespace knightL_on_chessboard
{

    class Knight{
        int sourceXPosition;
        int sourceYPosition;
        Knight parent;
        ArrayList posibleLocationsToGo;
        int locationIndex;
        Hashtable posibleLocationsToGoKeys;

        public Knight(int sourceXPosition, int sourceYPosition, Knight parent = null)
        {
            this.sourceXPosition = sourceXPosition;
            this.sourceYPosition = sourceYPosition;
            this.parent = parent;
            this.posibleLocationsToGo = new ArrayList();
            this.posibleLocationsToGoKeys = new Hashtable();
            locationIndex = 0;
        }

        public int GetSourceXPosition(){
            return this.sourceXPosition;
        }

        public int GetSourceYPosition()
        {
            return this.sourceYPosition;
        }

        public Knight GetNextLocationToGo()
        {
            var nextLocation = (Knight)this.posibleLocationsToGo[this.locationIndex];
            this.locationIndex++;
            return nextLocation;
        }

        public ArrayList GenerateAllPosibleLocationsToGo(int xMovements, int yMovements)
        {
            /*
             * Stop conditions: 
             * 1- no new movements, the parent must be excluded.
             * 2- we found the target!
             */


            //first positive posible pair
            int xDestination = sourceXPosition + xMovements;
            int yDestination = sourceYPosition + yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            //first negative posible pair
            xDestination = sourceXPosition - xMovements;
            yDestination = sourceYPosition - yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            //second positive posible pair
            xDestination = sourceXPosition + yMovements;
            yDestination = sourceYPosition + xMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            //second negative posible pair
            xDestination = sourceXPosition - yMovements;
            yDestination = sourceYPosition - xMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            xDestination = sourceXPosition - xMovements;
            yDestination = sourceYPosition + yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            xDestination = sourceXPosition + yMovements;
            yDestination = sourceYPosition - xMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            xDestination = sourceXPosition + xMovements;
            yDestination = sourceYPosition - yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            xDestination = sourceXPosition - yMovements;
            yDestination = sourceYPosition + xMovements;
            AddPosibleLocationsGo(xDestination, yDestination); 

            return posibleLocationsToGo;
        }

        public void AddPosibleLocationsGo(int xDestination, int yDestination)
        {
            if (IsValidLocation(xDestination, yDestination) == true && IsParentLocation(xDestination, yDestination) == false)
            {
                String key;
                key = xDestination.ToString() + yDestination.ToString();
                if (posibleLocationsToGoKeys.Contains(key) == false)
                {
                    var newLocation = new Knight(xDestination, yDestination, this);
                    this.posibleLocationsToGoKeys.Add(key, newLocation);
                    this.posibleLocationsToGo.Add(newLocation);

                }
            }
        }

        public bool IsParentLocation(int xPosition, int yPosition)
        {
            return ( this.parent != null && this.parent.GetSourceXPosition() == xPosition && this.parent.GetSourceYPosition() == yPosition)? true : false;
        }

        public bool IsValidLocation(int xPosition, int yPosition)
        {        
            return (xPosition < 0 || xPosition > 4 || yPosition < 0 || yPosition > 4)? false : true;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            int targetX = 4;
            int targetY = 4;
            bool targetWasFound = false;

            var knightInInitialLocation = new Knight(0, 0);
            knightInInitialLocation.GenerateAllPosibleLocationsToGo(1, 2);
            Knight nextLocation = knightInInitialLocation.GetNextLocationToGo();

            if(nextLocation.GetSourceXPosition() == targetX && nextLocation.GetSourceYPosition() == targetY){
                targetWasFound = true;
            }

            int count = 0;
            while(targetWasFound == false && count < 20){
                Console.WriteLine(nextLocation.GetSourceXPosition().ToString() + nextLocation.GetSourceYPosition().ToString());

                nextLocation.GenerateAllPosibleLocationsToGo(1,2);
                nextLocation = nextLocation.GetNextLocationToGo();
                if (nextLocation.GetSourceXPosition() == targetX && nextLocation.GetSourceYPosition() == targetY)
                {
                    targetWasFound = true;
                }

                count++;
            }


        }

        public static void RunLocationGeneration(){
            var knightInInitialLocation = new Knight(0, 0);
            ArrayList posibleLocationsToGo = knightInInitialLocation.GenerateAllPosibleLocationsToGo(1, 2);
            foreach (Knight knight in posibleLocationsToGo)
            {
                Console.WriteLine(knight.GetSourceXPosition().ToString() + knight.GetSourceYPosition().ToString());
            }
        }
    }
}
