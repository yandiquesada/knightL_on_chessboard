﻿using System;
using System.Collections;

namespace knightL_on_chessboard
{

    class Knight{
        int sourceXPosition;
        int sourceYPosition;
        Knight parent;
        ArrayList posibleLocationsToGo;
        Hashtable posibleLocationsToGoKeys;

        public Knight(int sourceXPosition, int sourceYPosition, Knight parent = null)
        {
            this.sourceXPosition = sourceXPosition;
            this.sourceYPosition = sourceYPosition;
            this.parent = parent;
            this.posibleLocationsToGo = new ArrayList();
            this.posibleLocationsToGoKeys = new Hashtable();
        }

        public int GetSourceXPosition(){
            return this.sourceXPosition;
        }

        public int GetSourceYPosition()
        {
            return this.sourceYPosition;
        }

        public ArrayList GenerateAllPosibleCordinatesToGo(int xMovements, int yMovements){
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
            if (IsValidLocation(xDestination, yDestination))
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

        public bool IsValidLocation(int xPosition, int yPosition){
            //this only validates if is inside the table!
            return (xPosition < 0 || xPosition > 4 || yPosition < 0 || yPosition > 4)? false : true;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var knightInInitialCoordinate = new Knight(2, 2);
            ArrayList posibleLocationsToGo = knightInInitialCoordinate.GenerateAllPosibleCordinatesToGo(1, 2);


            foreach (Knight knight in posibleLocationsToGo)
            {
                Console.WriteLine(knight.GetSourceXPosition().ToString() + knight.GetSourceYPosition().ToString());
            }

            //Console.WriteLine(((Knight)posibleLocationsToGo[0]).GetSourceXPosition().ToString());
        }
    }
}