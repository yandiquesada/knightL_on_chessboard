using System;
using System.Collections;
using System.Collections.Generic;

namespace knightL_on_chessboard
{

    class Knight{
        int sourceXPosition;
        int sourceYPosition;
        bool wereGeneratedAllPosibleLocationsToGo;
        Knight parent;
        ArrayList posibleLocationsToGo;
        int locationIndex;

        //TODO: this must be a hasset
        Hashtable posibleLocationsToGoKeys;

        HashSet<string> keyPath;

        public Knight(int sourceXPosition, int sourceYPosition, HashSet<string> keyPath, Knight parent = null)
        {
            this.sourceXPosition = sourceXPosition;
            this.sourceYPosition = sourceYPosition;
            this.wereGeneratedAllPosibleLocationsToGo = false;
            this.parent = parent;
            this.posibleLocationsToGo = new ArrayList();
            this.posibleLocationsToGoKeys = new Hashtable();
            locationIndex = 0;

            this.keyPath = keyPath;
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
            var nextLocation = parent;
            if (locationIndex < this.posibleLocationsToGo.Count){
                nextLocation = (Knight)this.posibleLocationsToGo[this.locationIndex];
                this.locationIndex++;
            }

            return nextLocation;
        }

        public ArrayList GenerateAllPosibleLocationsToGo(int xMovements, int yMovements)
        {
            if(wereGeneratedAllPosibleLocationsToGo == true){
                return this.posibleLocationsToGo;
            }

            int xDestination = sourceXPosition + xMovements;
            int yDestination = sourceYPosition + yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);
            
            xDestination = sourceXPosition - xMovements;
            yDestination = sourceYPosition - yMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

            xDestination = sourceXPosition + yMovements;
            yDestination = sourceYPosition + xMovements;
            AddPosibleLocationsGo(xDestination, yDestination);

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

            this.wereGeneratedAllPosibleLocationsToGo = true;
            return posibleLocationsToGo;
        }

        public void AddPosibleLocationsGo(int xDestination, int yDestination)
        {
            if (IsValidLocation(xDestination, yDestination) == true)
            {
                String key;
                key = xDestination.ToString() + yDestination.ToString();
                bool isNewLocation = this.keyPath.Add(key);
                if (posibleLocationsToGoKeys.Contains(key) == false && isNewLocation == true)
                {
                    var newLocation = new Knight(xDestination, yDestination, this.keyPath, this);
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
            HashSet<string> keyPath = new HashSet<String>();

            var knightInInitialLocation = new Knight(0, 0, keyPath);
            knightInInitialLocation.GenerateAllPosibleLocationsToGo(1, 2);
            Knight nextLocation = knightInInitialLocation.GetNextLocationToGo();

            if(nextLocation.GetSourceXPosition() == targetX && nextLocation.GetSourceYPosition() == targetY){
                targetWasFound = true;
            }

            int count = 0;
            while(targetWasFound == false && count < 100){
                Console.WriteLine(nextLocation.GetSourceXPosition().ToString() + nextLocation.GetSourceYPosition().ToString());

                nextLocation.GenerateAllPosibleLocationsToGo(1,2);
                nextLocation = nextLocation.GetNextLocationToGo();
                if (nextLocation.GetSourceXPosition() == targetX && nextLocation.GetSourceYPosition() == targetY)
                {
                    targetWasFound = true;
                    Console.WriteLine(nextLocation.GetSourceXPosition().ToString() + nextLocation.GetSourceYPosition().ToString());
                }
                count++;
            }
        }

        public static void RunLocationGeneration(){
            HashSet<string> keyPath = new HashSet<String>();
            var knightInInitialLocation = new Knight(0, 0, keyPath);
            ArrayList posibleLocationsToGo = knightInInitialLocation.GenerateAllPosibleLocationsToGo(1, 2);
            foreach (Knight knight in posibleLocationsToGo)
            {
                Console.WriteLine(knight.GetSourceXPosition().ToString() + knight.GetSourceYPosition().ToString());
            }
        }
    }
}
