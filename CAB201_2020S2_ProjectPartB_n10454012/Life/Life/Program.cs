using System;
using Display;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using static System.Convert;
using static System.ConsoleKey;

namespace Life
{
    interface INeighbors
    {
        int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                         ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount);
    }

    class MooreNeighhborhood : INeighbors
    {
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                                ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount)
        {
            // initializing neighbors
            int neighbors = 0;

            // neighbhor cell boundaries
            int PreviousCell = -1 * neighborhoodOrder;
            int NextCell = neighborhoodOrder;

            for (int rowCoordinate = PreviousCell; rowCoordinate <= NextCell; ++rowCoordinate)
            {
                for (int columnCoordinate = PreviousCell; columnCoordinate <= NextCell; ++columnCoordinate)
                {
                    // if opertating in non-periodic mode
                    if ((rowCoordinate == 0 && columnCoordinate == 0) && (!centreCount))
                    {
                        continue;
                    }

                    // current row and column number
                    int rowNumberCurrent = rowCheck + rowCoordinate;
                    int columnNumberCurrent = columnCheck + columnCoordinate;

                    // adding nieghbhors if they exist
                    if (rowNumberCurrent >= 0 && rowNumberCurrent < lifeGen.GetLength(0) &&
                        columnNumberCurrent >= 0 && columnNumberCurrent < lifeGen.GetLength(1))
                    {
                        neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                    }

                    // if operating in periodic mode
                    else if (periodicMode == true)
                    {
                        // larger than row cell boundry
                        if (rowNumberCurrent >= lifeGen.GetLength(0))
                        {
                            rowNumberCurrent -= lifeGen.GetLength(0);
                        }
                        // lerger than column cell boundry
                        if (columnNumberCurrent >= lifeGen.GetLength(1))
                        {
                            columnNumberCurrent -= lifeGen.GetLength(1);
                        }
                        // smaller than row cell boundry
                        if (rowNumberCurrent < 0)
                        {
                            rowNumberCurrent += lifeGen.GetLength(0);
                        }
                        // smaller than column cell boundry
                        if (columnNumberCurrent < 0)
                        {
                            columnNumberCurrent += lifeGen.GetLength(1);
                        }

                        // adding neighbhors if they exist
                        neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                    }
                }
            }

            // returning neighbors to rules of life
            return neighbors;
        }
    }

    class VonNeumannNeighhborhood : INeighbors
    {
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                                ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount)
        {
            // initializing neighbors
            int neighbors = 0;

            // neighbhor cell boundaries
            int PreviousCell = -1 * neighborhoodOrder;
            int NextCell = neighborhoodOrder;

            for (int rowCoordinate = PreviousCell; rowCoordinate <= NextCell; ++rowCoordinate)
            {
                int tempCoordinate = rowCoordinate < 0 ? neighborhoodOrder + rowCoordinate : neighborhoodOrder - rowCoordinate;

                for (int columnCoordinate = PreviousCell; columnCoordinate <= tempCoordinate; ++columnCoordinate)
                {
                    // if opertating in non-periodic mode
                    if (rowCoordinate == 0 && columnCoordinate == 0 && (!centreCount))
                    {
                        continue;
                    }

                    // current row and column number
                    int rowNumberCurrent = rowCheck + rowCoordinate;
                    int columnNumberCurrent = columnCheck + columnCoordinate;

                    bool proceed = false;

                    // adding nieghbhors if they exist
                    if (rowNumberCurrent >= 0 && rowNumberCurrent < lifeGen.GetLength(0) &&
                         columnNumberCurrent >= 0 && columnNumberCurrent < lifeGen.GetLength(1))
                    {
                        proceed = true;
                    }

                    // if operating in periodic mode
                    else if (periodicMode == true)
                    {
                        // larger than row cell boundry
                        if (rowNumberCurrent >= lifeGen.GetLength(0))
                        {
                            rowNumberCurrent -= lifeGen.GetLength(0);
                        }
                        // lerger than column cell boundry
                        if (columnNumberCurrent >= lifeGen.GetLength(1))
                        {
                            columnNumberCurrent -= lifeGen.GetLength(1);
                        }
                        // smaller than row cell boundry
                        if (rowNumberCurrent < 0)
                        {
                            rowNumberCurrent += lifeGen.GetLength(0);
                        }
                        // smaller than column cell boundry
                        if (columnNumberCurrent < 0)
                        {
                            columnNumberCurrent += lifeGen.GetLength(1);
                        }

                        proceed = true;
                    }

                    if (proceed)
                    {
                        if ((Pow(rowCoordinate, 2) + Pow(columnCoordinate, 2)) <= Pow(neighborhoodOrder, 2))
                        {
                            neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                        }
                    }

                }
            }

            // returning neighbors to rules of life
            return neighbors;
        }
    }

    class OldNeighbors : INeighbors
    {
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck, ref bool periodicMode,
                                ref int neighborhoodOrder, ref bool centreCount)
        {
            // initializing neighbors
            int neighbors = 0;

            // neighbhor cell boundaries
            const int PreviousCell = -1;
            const int NextCell = 1;

            for (int rowCoordinate = PreviousCell; rowCoordinate <= NextCell; ++rowCoordinate)
            {
                for (int columnCoordinate = PreviousCell; columnCoordinate <= NextCell; ++columnCoordinate)
                {
                    // if opertating in non-periodic mode
                    if (rowCoordinate == 0 && columnCoordinate == 0)
                    {
                        continue;
                    }

                    // current row and column number
                    int rowNumberCurrent = rowCheck + rowCoordinate;
                    int columnNumberCurrent = columnCheck + columnCoordinate;

                    // adding nieghbhors if they exist
                    if (rowNumberCurrent >= 0 && rowNumberCurrent < lifeGen.GetLength(0) &&
                        columnNumberCurrent >= 0 && columnNumberCurrent < lifeGen.GetLength(1))
                    {
                        neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                    }

                    // if operating in periodic mode
                    else if (periodicMode == true)
                    {
                        // larger than row cell boundry
                        if (rowNumberCurrent >= lifeGen.GetLength(0))
                        {
                            rowNumberCurrent -= lifeGen.GetLength(0);
                        }
                        // lerger than column cell boundry
                        if (columnNumberCurrent >= lifeGen.GetLength(1))
                        {
                            columnNumberCurrent -= lifeGen.GetLength(1);
                        }
                        // smaller than row cell boundry
                        if (rowNumberCurrent < 0)
                        {
                            rowNumberCurrent += lifeGen.GetLength(0);
                        }
                        // smaller than column cell boundry
                        if (columnNumberCurrent < 0)
                        {
                            columnNumberCurrent += lifeGen.GetLength(1);
                        }

                        // adding neighbbhors if they exist
                        neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                    }
                }
            }

            // returning neighbors to rules of life
            return neighbors;
        }
    }

    class Program
    {
        /// <summary>
        /// method for checking validity
        /// </summary>
        /// <param name="args"></param>
        /// <param name="success"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="periodicMode"></param>
        /// <param name="randomFactor"></param>
        /// <param name="inputFile"></param>
        /// <param name="generations"></param>
        /// <param name="maxUpdateRate"></param>
        /// <param name="stepMode"></param>
        /// <param name="neighborhoodType"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        /// <param name="survivalConstraints"></param>
        /// <param name="birthConstraints"></param>
        /// <param name="ghostMode"></param>
        /// <param name="generationalMemory"></param>
        /// <param name="outputFile"></param>
        /// <param name="survivalFirstValue"></param>
        /// <param name="survivalLastValue"></param>
        /// <param name="birthFirstValue"></param>
        /// <param name="birthLastValue"></param>
        static void PerformingChecks(string[] args, ref bool success, ref int rows,
                                     ref int columns, ref bool periodicMode, ref double randomFactor,
                                     ref string inputFile, ref int generations, ref double maxUpdateRate,
                                     ref bool stepMode, ref string neighborhoodType, ref int neighborhoodOrder,
                                     ref bool centreCount, ref List<int> survivalConstraints,
                                     ref List<int> birthConstraints, ref bool ghostMode,
                                     ref int generationalMemory, ref string outputFile, ref int survivalFirstValue,
                                     ref int survivalLastValue, ref int birthFirstValue, ref int birthLastValue)
        {
            for (int index = 0; index < args.Length; ++index)
            {
                //dimensions check
                Dimensions(args, index, ref success, ref rows, ref columns);

                // periodic mode check
                Periodic(args, index, ref periodicMode);

                // random factor check
                RandomFactor(args, index, ref randomFactor, ref success);

                // input file check
                InputFile(args, index, ref inputFile, ref success);

                // generations check
                Generations(args, index, ref generations, ref success);

                // maximum update rate check
                MaxUpdateRate(args, index, ref maxUpdateRate, ref success);

                // step mode check
                StepMode(args, index, ref stepMode);

                // neighbood check
                Neighborhood(args, index, ref neighborhoodType, ref neighborhoodOrder,
                             ref centreCount, ref success, ref rows, ref columns);

                // survival and birth check
                SurvivalAndBirth(args, index, ref survivalConstraints, ref birthConstraints,
                                 ref success, ref survivalFirstValue, ref survivalLastValue,
                                 ref birthFirstValue, ref birthLastValue);

                // ghost mode check
                GhostMode(args, index, ref ghostMode);

                // generational memroy check
                GenerationalMemory(args, index, ref generationalMemory, ref success);

                // output file check
                OutputFile(args, index, ref outputFile, ref success);
            }
        }

        /// <summary>
        /// steady state implementation
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="limit"></param>
        /// <param name="memoryQueue"></param>
        static bool AddToQueue(int[,] lifeGen, ref int limit, Queue<string> memoryQueue)
        {
            string list = GetStringFromArray(lifeGen);
            
            if (memoryQueue.Contains(list))
            {
                return true;
            }

            if (memoryQueue.Count == limit)
            {
                memoryQueue.Dequeue();
            }

            memoryQueue.Enqueue(list);

            return false;
        }

        public static int[,] GetArrayFromString(string arrayText, int upperLen, int lowerLen)         {             string[] parsed = arrayText.Split(",");             int[,] output = new int[upperLen, lowerLen];             for (var upper = 0; upper < upperLen; upper++)                 for (var lower = 0; lower < lowerLen; lower++)                     output[upper, lower] = int.Parse(parsed[(upper * lowerLen) + lower]);             return output;         }

        /// <summary>
        /// converting from string to array
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string GetStringFromArray(int[,] lst)         {             return string.Join(',', lst.Cast<int>());         } 
        /// <summary>
        /// output file check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="outputFile"></param>
        /// <param name="success"></param>
        static void OutputFile(string[] args, int index, ref string outputFile, ref bool success)
        {
            // --output args
            if (args[index] == "--output")
            {
                // checking if parameter provided
                try
                {
                    // if parameter provided, get output file
                    if (args[index + 1].Substring(args[index + 1].Length - 5, (".seed").Length) == ".seed")
                    {
                        outputFile = args[index + 1];
                    }
                    // else displaying error
                    else
                    {
                        outputFile = "";

                        Error.WriteLine("the value must be a valid absolute or relative file path. " +
                                        "the value must be a valid file path having the.seed file extension.");
                    }
                }
                // no paramter provided
                catch
                {
                    outputFile = "";

                    success = false;
                    Error.WriteLine("1 output file parameter is missing");
                }
            }
        }

        /// <summary>
        /// generational memroy check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="generationalMemory"></param>
        /// <param name="success"></param>
        static void GenerationalMemory(string[] args, int index, ref int generationalMemory, ref bool success)
        {
            // --memory args
            if (args[index] == "--generations")
            {
                // checking if paramter provided
                try
                {
                    // if parameter is integer, store generations
                    if (int.TryParse(args[index + 1], out generationalMemory))
                    {
                        // if integer > 0, keep generations
                        // else display error and assign defualt generations
                        if (!((4 <= generationalMemory) && (generationalMemory <= 512)))
                        {
                            generationalMemory = 16;

                            success = false;
                            Error.WriteLine("Generational memory must be an integer betwen 4 and 512 (inclusive)");
                        }
                    }
                    // else display error and assign default generations
                    else
                    {
                        generationalMemory = 16;

                        success = false;
                        Error.WriteLine("The value must be an integer");
                    }
                }
                // else display error and assign defult generations
                catch
                {
                    generationalMemory = 16;

                    success = false;
                    Error.WriteLine("1 generational memory parameter is missing");
                }
            }
        }

        /// <summary>
        /// ghost mode implementation
        /// </summary>
        /// <param name="state"></param>
        /// <param name="spot"></param>
        /// <param name="cell"></param>
        /// <param name="survival"></param>
        /// <param name="birth"></param>
        /// <param name="ghost"></param>
        static void CellStates(int state, ref int spot, int cell, List<int> survival,
                               List<int> birth, ref bool ghostMode)
        {
            //checking for the number of neighbors depending on the cell state
            if (cell != (int)CellConstants.Alive)
            {
                if (birth.Contains(state))
                {
                    spot = (int)CellConstants.Alive; //cell is alive for next generation
                }

                else
                {
                    if (ghostMode == true)
                    {
                        if (cell == (int)CellConstants.Dark)
                        {
                            spot = (int)CellConstants.Medium;
                        }

                        else if (cell == (int)CellConstants.Medium)
                        {
                            spot = (int)CellConstants.Light;
                        }

                        else
                        {
                            spot = (int)CellConstants.Dead;
                        }
                    }

                    else
                    {
                        spot = (int)CellConstants.Dead;
                    }
                }
            }
            else if (cell == (int)CellConstants.Alive)
            {
                if (survival.Contains(state))
                {
                    spot = (int)CellConstants.Alive;
                }

                else
                {
                    if (ghostMode == true)
                    {
                        spot = (int)CellConstants.Dark;
                    }

                    else
                    {
                        spot = (int)CellConstants.Dead;
                    }
                }
            }
        }

        /// <summary>
        /// ghost mode check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="ghostMode"></param>
        static void GhostMode(string[] args, int index, ref bool ghostMode)
        {
            // --ghost args
            if (args[index] == "--ghost")
            {
                ghostMode = true;
            }
        }

        /// <summary>
        /// checking for survival and birth constraints
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="survivalConstraints"></param>
        /// <param name="birthConstraints"></param>
        /// <param name="success"></param>
        /// <param name="survivalFirstValue"></param>
        /// <param name="survivalLastValue"></param>
        /// <param name="birthFirstValue"></param>
        /// <param name="birthLastValue"></param>
        static void SurvivalAndBirth(string[] args, int index, ref List<int> survivalConstraints,
                                     ref List<int> birthConstraints, ref bool success,
                                     ref int survivalFirstValue, ref int survivalLastValue,
                                     ref int birthFirstValue, ref int birthLastValue)
        {
            if (args[index] == "--survival")
            {
                survivalConstraints.Clear();

                if (args[index + 2] == "...")
                {
                    try
                    {
                        if (!int.TryParse(args[index + 1], out survivalFirstValue) && !int.TryParse(args[index + 3], out survivalLastValue))
                        {
                            survivalFirstValue = 2;
                            survivalLastValue = 3;

                            success = false;
                            Error.WriteLine("first value and/or last value of survival must be an integer(s)");
                        }

                        else
                        {
                            if (survivalFirstValue < survivalLastValue)
                            {
                                survivalFirstValue = 2;
                                survivalLastValue = 3;

                                success = false;
                                Error.WriteLine("first value of survival must not be greater than the second value");
                            }

                            else if (survivalFirstValue < 0)
                            {
                                survivalFirstValue = 2;
                                survivalLastValue = 3;

                                success = false;
                                Error.WriteLine("first value of survival must be a positive non-zero value");
                            }
                        }

                        for (int i = survivalFirstValue; i <= survivalLastValue; ++i)
                        {
                            survivalConstraints.Add(i);
                        }
                    }

                    catch
                    {
                        survivalFirstValue = 2;
                        survivalLastValue = 3;

                        for (int i = survivalFirstValue; i <= survivalLastValue; ++i)
                        {
                            survivalConstraints.Add(i);
                        }

                        success = false;
                        Error.WriteLine("first value of survival must be a positive non-zero value");
                    }
                }
            }

            else
            {
                for (int i = index + 1; i < args.Length; ++i)
                {
                    try
                    {
                        if (int.TryParse(args[index + 1], out int survivalValue))
                        {
                            if (!(survivalValue > 1))
                            {
                                survivalFirstValue = 2;
                                survivalLastValue = 3;

                                success = false;
                                Error.WriteLine("survival must be a positive non-zero value");
                            }
                            else
                            {
                                survivalFirstValue = survivalValue;
                                survivalLastValue = survivalValue;
                            }

                            for (int j = survivalFirstValue; j <= survivalLastValue; ++j)
                            {
                                survivalConstraints.Add(i);
                            }
                        }
                        else
                        {
                            survivalFirstValue = 2;
                            survivalLastValue = 3;

                            for (int j = survivalFirstValue; j <= survivalLastValue; ++j)
                            {
                                survivalConstraints.Add(j);
                            }

                            success = false;
                            Error.WriteLine("survival must be an integer");
                        }
                    }

                    catch
                    {
                        survivalFirstValue = 2;
                        survivalLastValue = 3;

                        for (int j = survivalFirstValue; j <= survivalLastValue; ++j)
                        {
                            survivalConstraints.Add(j);
                        }

                        success = false;
                        Error.WriteLine("survival was not povided");
                    }
                }
            }

            if (args[index] == "--birth")
            {
                birthConstraints.Clear();

                if (args[index + 2] == "...")
                {
                    try
                    {
                        if (!int.TryParse(args[index + 1], out birthFirstValue) && !int.TryParse(args[index + 3], out birthLastValue))
                        {
                            birthFirstValue = 3;
                            birthLastValue = 3;

                            success = false;
                            Error.WriteLine("first value and/or last value of birth must be an integer(s)");
                        }

                        else
                        {
                            if (birthFirstValue < birthLastValue)
                            {
                                birthFirstValue = 3;
                                birthLastValue = 3;

                                success = false;
                                Error.WriteLine("first value of birth must not be greater than the second value");
                            }

                            else if (birthFirstValue < 0)
                            {
                                birthFirstValue = 2;
                                birthLastValue = 3;

                                success = false;
                                Error.WriteLine("first value of birth must be a positive non-zero value");
                            }
                        }

                        for (int j = birthFirstValue; j <= birthLastValue; ++j)
                        {
                            birthConstraints.Add(j);
                        }
                    }

                    catch
                    {
                        birthFirstValue = 3;
                        birthLastValue = 3;

                        for (int j = birthFirstValue; j <= birthLastValue; ++j)
                        {
                            birthConstraints.Add(j);
                        }

                        success = false;
                        Error.WriteLine("first value of birth must be a positive non-zero value");
                    }
                }
            }

            else
            {
                for (int i = index + 1; i < args.Length; ++i)
                {
                    try
                    {
                        if (int.TryParse(args[index + 1], out int birthValue))
                        {
                            if (!(birthValue > 1))
                            {
                                birthFirstValue = 3;
                                birthLastValue = 3;

                                success = false;
                                Error.WriteLine("birth must be a positive non-zero value");
                            }
                            else
                            {
                                birthFirstValue = birthValue;
                                birthLastValue = birthValue;

                                for (int j = birthFirstValue; j <= birthLastValue; ++j)
                                {
                                    birthConstraints.Add(j);
                                }
                            }
                        }
                        else
                        {
                            birthFirstValue = 3;
                            birthLastValue = 3;

                            for (int j = birthFirstValue; j <= birthLastValue; ++j)
                            {
                                birthConstraints.Add(j);
                            }

                            success = false;
                            Error.WriteLine("birth must be an integer");
                        }
                    }

                    catch
                    {
                        survivalFirstValue = 2;
                        survivalLastValue = 3;

                        for (int j = birthFirstValue; j <= birthLastValue; ++j)
                        {
                            birthConstraints.Add(j);
                        }

                        success = false;
                        Error.WriteLine("birth was not povided");
                    }
                }
            }
        }

        /// <summary>
        /// method to check the nieghborhood type, size and centre count option
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="neighborhoodType"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        /// <param name="success"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        static void Neighborhood(string[] args, int index, ref string neighborhoodType,
                                 ref int neighborhoodOrder, ref bool centreCount, ref bool success,
                                 ref int rows, ref int columns)
        {
            if (args[index] == "--neighbor")
            {
                try
                {
                    if (args[index + 1].ToLower().Equals("moore"))
                    {

                    }

                    else if (args[index + 1].ToLower().Equals("vonNeumann"))
                    {
                        neighborhoodType = args[index + 1];
                    }

                    else
                    {
                        neighborhoodType = "moore";

                        success = false;
                        Error.WriteLine("neighboorhood type must be either moore or vonneumann");
                    }
                }

                catch (Exception fail)
                {
                    neighborhoodType = "moore";

                    success = false;
                    throw new ArgumentException(fail.Message);
                }

                try
                {
                    if (int.TryParse(args[index + 2], out neighborhoodOrder))
                    {
                        if (1 <= neighborhoodOrder && neighborhoodOrder <= 10)
                        {
                            int minOrder = Min(rows, columns) / 2;

                            if (!(neighborhoodOrder < minOrder))
                            {
                                neighborhoodOrder = 1;

                                success = false;
                                Error.WriteLine("nieghborhood order must be less than {0}", minOrder);
                            }
                        }
                        else
                        {
                            neighborhoodOrder = 1;

                            success = false;
                            Error.WriteLine("nieghborhood order must be a positive integer between 1 and 10(inclusive).");
                        }
                    }
                    else
                    {
                        neighborhoodOrder = 1;

                        success = false;
                        Error.WriteLine("nieghborhood order must be provided");
                    }
                }

                catch (Exception fail)
                {
                    neighborhoodOrder = 1;

                    success = false;
                    throw new ArgumentException(fail.Message);
                }

                try
                {
                    if (!bool.TryParse(args[index + 3].ToLower(), out centreCount))
                    {
                        centreCount = false;

                        success = false;
                        Error.WriteLine("centre count must be of type boolean (true or false)");
                    }
                }

                catch
                {
                    centreCount = false;

                    success = false;
                    Error.WriteLine("centre count must be provided");
                }
            }
        }

        /// <summary>
        /// method to check the randomness
        /// whether a cell is alive or not
        /// depending on the provided or default random factor
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="randomFactor"></param>
        /// <param name="(int)CellConstants.Alive"></param>
        static void Randomness(ref int[,] lifeGen, double randomFactor, int Alive)
        {
            // randomly generating alive and dead cells
            // using random factor
            Random rand = new Random();

            for (int rowNumber = 0; rowNumber < lifeGen.GetLength(0); ++rowNumber)
            {
                for (int columnNumber = 0; columnNumber < lifeGen.GetLength(1); ++columnNumber)
                {
                    // randomess logic
                    if (rand.Next(1, 100) < (100 * randomFactor))
                    {
                        lifeGen[rowNumber, columnNumber] = Alive;
                    }
                }
            }
        }

        /// <summary>
        /// method to check the dimensions of the grid
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="success"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        static void Dimensions(string[] args, int index, ref bool success,
                               ref int rows, ref int columns)
        {
            // --dimensions args
            if (args[index] == "--dimensions")
            {
                // checking if paramters provided
                try
                {
                    // no parameters provided
                    if (args[index + 1].Contains("--"))
                    {
                        rows = 16;
                        columns = 16;

                        success = false;
                        Error.WriteLine("2 dimensions paraters are missing");
                    }
                    // 1 parameter provided
                    else if (args[index + 2].Contains("--"))
                    {
                        columns = 16;

                        success = false;
                        Error.WriteLine("1 dimensions paraters are missing");
                    }
                    // 2 parametrs provided
                    else
                    {
                        // if parameter is an integer, storing rows value
                        if (int.TryParse(args[index + 1], out rows))
                        {
                            // if rows between 4 and 48 (inclusive), keeping rows
                            // else displaying error and assigning default rows
                            if (!(4 <= rows && rows <= 48))
                            {
                                rows = 16;

                                success = false;
                                Error.WriteLine("rows must be a positive integer between 4 and 48(inclusive).");
                            }
                        }
                        // displaying error and assigning default rows
                        else
                        {
                            rows = 16;

                            success = false;
                            Error.WriteLine("provided row value must be an integer");
                        }


                        // if parameter is an integer, storing columns value
                        if (int.TryParse(args[index + 2], out columns))
                        {
                            // if columns between 4 and 48 (inclusive), keeping columns
                            // else displaying error and assigning default columns
                            if (!(4 <= columns && columns <= 48))
                            {
                                columns = 16;

                                success = false;
                                Error.WriteLine("columns must be a positive integer between 4 and 48(inclusive).");
                            }
                        }
                        // else displaying error and assigning default rows
                        else
                        {
                            columns = 16;

                            success = false;
                            Error.WriteLine("provided column value must be an integer");
                        }
                    }
                }
                // no parametrs provided
                catch
                {
                    rows = 16;
                    columns = 16;

                    success = false;
                    Error.WriteLine("2 dimensions parameters are missing");
                }
            }
        }

        /// <summary>
        /// method to check the periodic mode
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="periodicMode"></param>
        static void Periodic(string[] args, int index, ref bool periodicMode)
        {
            // --periodic args
            if (args[index] == "--periodic")
            {
                periodicMode = true;
            }
        }

        /// <summary>
        /// method to check the random factor
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="randomFactor"></param>
        /// <param name="success"></param>
        static void RandomFactor(string[] args, int index, ref double randomFactor,
                                 ref bool success)
        {
            // --random args
            if (args[index] == "--random")
            {
                // checking if paramter provided
                try
                {
                    // if parameter is a double, storing random factor value
                    if (double.TryParse(args[index + 1], out randomFactor))
                    {
                        // if random factor between 0 and 1 (inclusive), keeping random factor
                        // else displaying error and assigning default random factor
                        if (!(0 <= randomFactor && randomFactor <= 1))
                        {
                            randomFactor = 0.5;

                            success = false;
                            Error.WriteLine("random factor must be between 0 and 1(inclusive).");
                        }
                    }
                    // else displaying error and assinging defualt values
                    else
                    {
                        randomFactor = 0.5;

                        success = false;
                        Error.WriteLine("the value must be a float");
                    }
                }
                // no parameter provided
                catch
                {
                    randomFactor = 0.5;

                    success = false;
                    Error.WriteLine("1 random factor parameter is missing");
                }
            }
        }

        /// <summary>
        /// method to check the input seed file
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="inputFile"></param>
        /// <param name="success"></param>
        static void InputFile(string[] args, int index, ref string inputFile,
                              ref bool success)
        {
            // --seed args
            if (args[index] == "--seed")
            {
                // checking if parameter provided
                try
                {
                    // if parameter provided, get input file
                    if (args[index + 1].Substring(args[index + 1].Length - 5, (".seed").Length) == ".seed")
                    {
                        inputFile = args[index + 1];
                    }
                    // else displaying error
                    else
                    {
                        inputFile = "";

                        Error.WriteLine("the value must be a valid absolute or relative file path. " +
                                        "the value must be a valid file path having the.seed file extension.");
                    }
                }
                // no paramter provided
                catch
                {
                    inputFile = "";

                    success = false;
                    Error.WriteLine("1 input file parameter is missing");
                }
            }
        }

        /// <summary>
        /// method to check the generations
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="generations"></param>
        /// <param name="success"></param>
        static void Generations(string[] args, int index, ref int generations,
                                ref bool success)
        {
            // --generation args
            if (args[index] == "--generations")
            {
                // checking if paramter provided
                try
                {
                    // if parameter is integer, store generations
                    if (int.TryParse(args[index + 1], out generations))
                    {
                        // if integer > 0, keep generations
                        // else display error and assign defualt generations
                        if (!(0 < generations))
                        {
                            generations = 50;

                            success = false;
                            Error.WriteLine("Generations must be a positive non-zero value");
                        }
                    }
                    // else display error and assign default generations
                    else
                    {
                        generations = 50;

                        success = false;
                        Error.WriteLine("The value must be an integer");
                    }
                }
                // else display error and assign defult generations
                catch
                {
                    generations = 50;

                    success = false;
                    Error.WriteLine("1 generations parameter is missing");
                }
            }
        }

        /// <summary>
        /// method to check the update rate
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="maxUpdateRate"></param>
        /// <param name="success"></param>
        static void MaxUpdateRate(string[] args, int index, ref double maxUpdateRate,
                                  ref bool success)
        {
            // --max-update args
            if (args[index] == "--max-update")
            {
                // checking if parameter provided
                try
                {
                    // if parameter provided is double, store update rate
                    if (double.TryParse(args[index + 1], out maxUpdateRate))
                    {
                        // if parameter between 1 & 30 (inclusive), keep update rate
                        // else display error and assign defualt update rate
                        if (!(1 <= maxUpdateRate && maxUpdateRate <= 30))
                        {
                            maxUpdateRate = 5;

                            success = false;
                            Error.WriteLine("Update rate must be between 1 and 30 (inclusive).");
                        }
                    }
                    // else display error and assign defualt maximum update rate
                    else
                    {
                        maxUpdateRate = 5;

                        success = false;
                        Error.WriteLine("The value must be a float");
                    }
                }
                // else display error and assign defualt maximum update rate
                catch
                {
                    maxUpdateRate = 5;

                    success = false;
                    Error.WriteLine("1 update rate parameter is missing");
                }
            }
        }

        /// <summary>
        /// method to check the step mode
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="stepMode"></param>
        static void StepMode(string[] args, int index, ref bool stepMode)
        {
            // --step args
            if (args[index] == "--step")
            {
                stepMode = true;
            }
        }

        /// <summary>
        /// convert first letter of string to uppercase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        /// reading cell seeds
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="state"></param>
        static void CellSeed(ref int[,] seedArray, int row, int column, int state) 
        {
            seedArray[row, column] = state;
        }

        /// <summary>
        /// reading rectangular seeds
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="state"></param>
        static void RectSeed(ref int[,] seedArray, int row, int column, int width,  int height, int state)
        {
            for (int x = row; x <= height; ++x) 
            {
                for (int y = column; y <= width; ++y)
                {
                    seedArray[x, y] = state;
                }
            }
        }

        /// <summary>
        /// reading ellipse seeds
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="state"></param>
        static void EllipseSeed(ref int[,] seedArray, int row, int column, int width, int height, int state)
        {
            // determining the centre
            double centreX = (double)(row + height) / 2;
            double centreY = (double)(width + column) / 2;

            // checking with the ellipse rule
            for (int i = 0; i < seedArray.GetLength(0); ++i)
            {
                for (int j = 0; j < seedArray.GetLength(1); ++j)
                {
                    double xPart = 4 * Math.Pow(i - centreX, 2) / Math.Pow(height - row + 1, 2);

                    double yPart = 4 * Math.Pow(j - centreY, 2) / Math.Pow(width - column + 1, 2);

                    double ellipseResult = xPart + yPart;


                    if (ellipseResult <= 1)
                    {
                        seedArray[i, j] = state;
                    }
                }
            }

        }

        /// <summary>
        /// method to check the seed file contents
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="inputFile"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="randomFactor"></param>
        /// <param name="Alive"></param>
        static void FileContents(ref int[,] lifeGen, ref string inputFile,
                                 ref int rows, ref int columns, ref double randomFactor,
                                 int Alive, ref bool fileMode, ref bool success)
        {
            // reading from seed file coordinates
            if (inputFile.Contains(".seed"))
            {
                // checking if the file can successfully be read
                try
                {
                    StreamReader seedFile = new StreamReader(inputFile);

                    if (seedFile.ReadLine() == "#version=2.0")
                    {

                        // reading the whole seed file
                        string readall = seedFile.ReadToEnd().Trim();

                        // split by number of co-ordinates
                        string[] linesplit = readall.Split('\n');

                        // string array with split line length
                        string[] linesLength = new string[linesplit.Length];


                        int[] coordinateX = new int[linesplit.Length];
                        int[] coordinateY = new int[linesplit.Length];
                        string[] state = new string[linesplit.Length];
                        int[] height = new int[linesplit.Length];
                        int[] width = new int[linesplit.Length];

                        //Assigning values from seed to arrays
                        for (int x = 0; x < linesplit.Length; x++)
                        {

                            //split each line to their x and y
                            linesLength[x] = linesplit[x].Split(",")[0];
                            coordinateY[x] = int.Parse(linesplit[x].Split(",")[1]);

                            state[x] = linesLength[x].Split(":")[0];
                            coordinateX[x] = int.Parse(linesLength[x].Split(":")[1]);


                            if (!linesplit[x].Contains("cell"))
                            {
                                height[x] = int.Parse(linesplit[x].Split(",")[2]);
                                width[x] = int.Parse(linesplit[x].Split(",")[3]);
                            }
                        }

                        for (int x = 0; x < coordinateX.Length; x++)
                        {
                            if (state[x].Contains("cell"))
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    CellSeed(ref lifeGen, coordinateX[x], coordinateY[x], 1);
                                }
                                else
                                {
                                    CellSeed(ref lifeGen, coordinateX[x], coordinateY[x], 0);
                                }
                            }

                            else if (state[x].Contains("rectangle"))
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    RectSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x], height[x], 1);
                                }
                                else
                                {
                                    RectSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x], height[x], 0);
                                }
                            }
                            else
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    EllipseSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x], height[x], 1);
                                }
                                else
                                {
                                    EllipseSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x], height[x], 0);
                                }
                            }
                        }
                    }


                    else
                    {
                        string fileContents = "";

                        List<int> rowsList = new List<int>();

                        List<int> columnsList = new List<int>();

                        List<int> widthList = new List<int>();

                        List<int> heightList = new List<int>();

                        List<string> setting = new List<string>();

                        int listCount = 0;

                        int rowsMax = 0;

                        int columnsMax = 0;

                        bool tooBig = false;

                        // reading till the end of the file
                        while ((fileContents = seedFile.ReadLine()) != null)
                        {
                            // reading row values
                            rowsList.Add(ToInt32(fileContents.Split(' ')[0]));

                            // reading column values
                            columnsList.Add(ToInt32(fileContents.Split(' ')[1]));

                            // rows are lower than seed row size
                            if (ToInt32(fileContents.Split(' ')[0]) > rows - 1)
                            {
                                tooBig = true;
                            }

                            // columns are lower than seed column size
                            if (ToInt32(fileContents.Split(' ')[1]) > columns - 1)
                            {
                                tooBig = true;
                            }

                            // recommended max row value
                            if (ToInt32(fileContents.Split(' ')[0]) > rowsMax)
                            {
                                rowsMax = ToInt32(fileContents.Split(' ')[0]);
                            }

                            // recommened max column value
                            if (ToInt32(fileContents.Split(' ')[1]) > columnsMax)
                            {
                                columnsMax = ToInt32(fileContents.Split(' ')[1]);
                            }

                            ++listCount;
                        }

                        // if the dimensions are not enough
                        if (tooBig == true)
                        {
                            if (rowsMax < 3)
                            {
                                rowsMax += 3;
                            }

                            if (columnsMax < 3)
                            {
                                columnsMax += 3;
                            }
                            Error.WriteLine(" The dimension size is too small." +
                                            "the recommended minimum size is {0}, {1}"
                                            , rowsMax + 1, columnsMax + 1);

                            Randomness(ref lifeGen, randomFactor, Alive);
                        }

                        // if dimensions ar egood enough
                        else
                        {
                            for (int listNumber = 0; listNumber < rowsList.Count; ++listNumber)
                            {
                                lifeGen[rowsList[listNumber], columnsList[listNumber]] = Alive;
                            }
                        }
                    }

                    fileMode = true;
                }

                catch(Exception e)
                {
                    success = false;
                    Error.WriteLine("The provided file path is invalid");

                    fileMode = false;
                    Randomness(ref lifeGen, randomFactor, Alive);
                }
            }
            // else display error and execute randomly generated cells
            else
            {
                Randomness(ref lifeGen, randomFactor, Alive);
            }
        }

        /// <summary>
        /// method to display runtime settings
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="generations"></param>
        /// <param name="maxUpdateRate"></param>
        /// <param name="periodicMode"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="randomFactor"></param>
        /// <param name="stepMode"></param>
        static void DisplayRuntimeSettings(ref string inputFile, ref int generations,
                                           ref double maxUpdateRate, ref bool periodicMode,
                                           ref int rows, ref int columns, ref double randomFactor,
                                           ref bool stepMode, ref bool success, ref string outputFile,
                                           ref int generationalMemory, ref int survivalFirstValue,
                                           ref int survivalLastValue, ref int birthFirstValue,
                                           ref int birthLastValue, ref string neighborhoodType,
                                           ref int neighborhoodOrder, ref bool ghostMode)
        {
            // display the game settings
            // success rate of args
            if (success == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteLine("\n Success: all command line arguments correctly processed.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine("Failure: not all command line arguments correctly processed.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            // header
            WriteLine("\n The program will use the following settings: \n");

            // input file
            if (inputFile == "")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Input File", "N/A"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Input File", inputFile));
            }

            //output file
            if (outputFile == "")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Output File", "N/A"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Output File", outputFile));
            }

            // generations
            WriteLine(String.Format("{0, 15} : {1, -10}", "Generations", generations));

            // memory
            WriteLine(String.Format("{0, 15} : {1, -10}", "Memory", generationalMemory));

            // update rate
            WriteLine(String.Format("{0, 15} : {1, -10}", "Refresh Rate", maxUpdateRate + " updates/s"));

            // survival and birth
            WriteLine(String.Format("{0, 15} : {1, -10}", "Rules", "S( " + survivalFirstValue + "..." +
                      survivalLastValue + " ) B( " + birthFirstValue + "..." + birthLastValue + " )"));

            // neighborhood
            if (neighborhoodType == "moore")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", UppercaseFirst(neighborhoodType) +
                          " (" + neighborhoodOrder + ")"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", UppercaseFirst(neighborhoodType).Substring(0,3) +
                          UppercaseFirst(neighborhoodType).Substring(4) + " (" + neighborhoodOrder + ")"));
            }
            

            // periodic mode
            if (periodicMode == true)
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Periodic", "Yes"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Periodic", "No"));
            }

            // rows
            WriteLine(String.Format("{0, 15} : {1, -10}", "Rows", rows));


            // columns
            WriteLine(String.Format("{0, 15} : {1, -10}", "Columns", columns));

            // random factor
            WriteLine(String.Format("{0, 15} : {1, -10}", "Random Factor",
                      randomFactor.ToString("P", CultureInfo.InvariantCulture)));

            // step mode
            if (stepMode == true)
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Step Mode", "Yes"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Step Mode", "No"));
            }

            // ghost mode
            if (ghostMode == true)
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Ghost Mode", "Yes"));
            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Ghost Mode", "No"));
            }

            // display message and wait until spacebar is pressed to start the simulation
            WriteLine("\n Press spacebar to start simulation...");
            while (true)
            {
                if (ReadKey().Key == Spacebar)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// method to check if spacebar should
        /// be pressed depending on step mode
        /// </summary>
        /// <param name="stepMode"></param>
        static void StepModeSpacebar(ref bool stepMode)
        {
            // if step mode is on, wait until space bar is pressed before moving to the next generation
            if (stepMode == true)
            {
                while (true)
                {
                    if (ReadKey().Key == Spacebar)
                    {
                        break;
                    }
                }
            }

            // if step mode if off, automatically continue to the next generation
        }

        /// <summary>
        /// method to update the first generation grid (initial)
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="grid"></param>
        static void UpdateInitialGrid(ref int[,] lifeGen, Grid grid)
        {
            for (int rowNumber = 0; rowNumber < lifeGen.GetLength(0); ++rowNumber)
            {
                for (int columnNumber = 0; columnNumber < lifeGen.GetLength(1); ++columnNumber)
                {
                    // fill the cell if it's alive
                    if (lifeGen[rowNumber, columnNumber] == (int)CellConstants.Alive)
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Full);
                    }
                    // else leave the cell blank
                    else
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Blank);
                    }
                }
            }
        }

        /// <summary>
        /// method to update the grid
        /// after the rules of life have been applied
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="tempGen"></param>
        /// <param name="grid"></param>
        /// <param name="Dead"></param>
        static void UpdateGenerationGrid(ref int[,] lifeGen, ref int[,] tempGen,
                                         Grid grid, int Dead)
        {
            // iterating through the temporary arrays rows and columns
            // updating cells according to their alive/dead status
            for (int rowNumber = 0; rowNumber < lifeGen.GetLength(0); ++rowNumber)
            {
                for (int columnNumber = 0; columnNumber < lifeGen.GetLength(1); ++columnNumber)
                {
                    // update alive cells as full
                    if (tempGen[rowNumber, columnNumber] == (int)CellConstants.Alive)
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Full);
                    }

                    else if (tempGen[rowNumber, columnNumber] == (int)CellConstants.Dark)
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Dark);
                    }

                    else if (tempGen[rowNumber, columnNumber] == (int)CellConstants.Medium)
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Medium);
                    }

                    else if (tempGen[rowNumber, columnNumber] == (int)CellConstants.Light)
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Light);
                    }

                    // update dead cells as blank
                    else
                    {
                        grid.UpdateCell(rowNumber, columnNumber, CellState.Blank);
                    }

                    // assigning cell statuses back to the game's main array
                    lifeGen[rowNumber, columnNumber] = tempGen[rowNumber, columnNumber];

                    // clearing all the cell statuses in the temporary array
                    tempGen[rowNumber, columnNumber] = Dead;
                }
            }
        }

        /// <summary>
        /// method to calculate the frame rate
        /// </summary>
        /// <param name="maxUpdateRate"></param>
        /// <param name="watch"></param>
        static void FrameRatePerGen(ref double maxUpdateRate, Stopwatch watch)
        {
            // 1 second = 1000 milli-seconds
            // converting to milliseconds
            const int ConvertSecondsToMilliseconds = 1000;

            // calculate frame rate
            // from (time between updates) = 1 / (update rate per frequency)
            double timePerGeneration = 1 / maxUpdateRate * ConvertSecondsToMilliseconds;

            // restarting the stopwatch
            watch.Restart();

            while (watch.ElapsedMilliseconds < timePerGeneration)
            {
                continue;
            }
        }

        /// <summary>
        /// method to check a cell's status
        /// in the next generation acocrding to the rules of life
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="tempGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <param name="TwoLiveNeighbors"></param>
        /// <param name="ThreeLiveNeighbors"></param>
        static void RulesOfLife(ref int[,] lifeGen, ref int[,] tempGen, ref int rowCheck,
                                ref int columnCheck, ref bool periodicMode, ref List<int> survivalConstraints,
                                ref List<int> birthConstraints, INeighbors neighborsnew, ref int neighborhoodOrder,
                                ref bool centreCount)
        {

            // calling from the GetNeighbors method
            // obtaining nieghbors in current cell
            int neighbors = neighborsnew.GetNeighbors(ref lifeGen, ref rowCheck, ref columnCheck, ref periodicMode, ref neighborhoodOrder,
                                                      ref centreCount);

            // final check for cell alive or dead
            // if cell alive, will it survive?
            if (lifeGen[rowCheck, columnCheck] == (int)CellConstants.Alive)
            {
                // if cell has exactly 2 or 3 live neighbours, it survives
                if (survivalConstraints.Contains(neighbors))
                {
                    tempGen[rowCheck, columnCheck] = (int)CellConstants.Alive;
                }

                // else the cell dies
                else
                {
                    tempGen[rowCheck, columnCheck] = (int)CellConstants.Dead;
                }
            }

            // if cell already dead, checking if cell alive in the next generation
            else
            {
                // if dead cell has exactly 3 live neighbours, it resurrects next generation
                if (birthConstraints.Contains(neighbors))
                {
                    tempGen[rowCheck, columnCheck] = (int)CellConstants.Alive;
                }

                // else the cell stays dead
                else
                {
                    tempGen[rowCheck, columnCheck] = (int)CellConstants.Dead;
                }
            }
        }

        /// <summary>
        /// survival and birth lists for the ranges
        /// </summary>
        /// <param name="survivalFirstValue"></param>
        /// <param name="survivalLastValue"></param>
        /// <param name="birthFirstValue"></param>
        /// <param name="birthLastValue"></param>
        static void SurvivalAndBirthRange(ref int survivalFirstValue, ref int survivalLastValue,
                                         ref int birthFirstValue, ref int birthLastValue,
                                         ref List<int> survivalConstraints, ref List<int> birthConstraints)
        {
            for (int i = survivalFirstValue; i <= survivalLastValue; ++i)
            {
                survivalConstraints.Add(i);
            }

            for (int i = birthFirstValue; i <= birthLastValue; ++i)
            {
                birthConstraints.Add(i);
            }
        }

        /// <summary>
        /// method to call the ending of the game
        /// </summary>
        /// <param name="grid"></param>
        static void GameOver(Grid grid)
        {
            // set complete marker as true
            grid.IsComplete = true;

            // render updates to the console window (grid should now display COMPLETE)...
            grid.Render();

            // console closes if spacebar is pressed after the program ends
            while (true)
            {
                if (ReadKey().Key == Spacebar)
                {
                    break;
                }
            }

            // revert grid window size and buffer to normal
            grid.RevertWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="fileName"></param>
        static void WriteToFile(ref int[,] lifeGen, ref string outputFile)
        {
            string path = outputFile;

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using StreamWriter sw = File.CreateText(path);
                sw.WriteLine("#version=2.0");

                for (int rowcheck = 0; rowcheck < lifeGen.GetLength(0); ++rowcheck)
                {
                    for (int columncheck = 0; columncheck < lifeGen.GetLength(1); ++columncheck)
                    {
                        if (lifeGen[rowcheck, columncheck] == (int)CellConstants.Alive)
                        {
                            String stringToWrite = "(o) cell: " + rowcheck + ", " + columncheck;

                            sw.WriteLine(stringToWrite);
                        }
                    }
                }
            }
        }

        // enumeration of repeating constants used in Main
        enum CellConstants
        {
            Alive = 1,
            Dead = 0,
            TwoLiveNeighbors = 2,
            ThreeLiveNeighbors = 3,
            Light = 4,
            Medium = 3,
            Dark = 2
        }

        /// <summary>
        /// main takes in all methods
        /// to execute the Game of Life
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // declaring argument variables
            int rows = 16;
            int columns = 16;
            bool periodicMode = false;
            double randomFactor = 0.5;
            string inputFile = "/Users/chilla/Desktop/game-of-life/CAB201_2020S2_ProjectPartB_n10454012/Seeds/figure-eight_14x14.seed";
            int generations = 50;
            double maxUpdateRate = 5;
            bool stepMode = false;
            string neighborhoodType = "moore";
            int neighborhoodOrder = 1;
            bool centreCount = false;
            bool ghostMode = true;
            int generationalMemory = 10;
            string outputFile = "/Users/chilla/Desktop/game-of-life/outputFile/output.seed";
            int survivalFirstValue = 2;
            int survivalLastValue = 3;
            int birthFirstValue = 3;
            int birthLastValue = 3;
            bool isSteady = false;
            bool fileMode = false;

            Queue<string> memoryQueue = new Queue<string>();

            List<int> survivalConstraints = new List<int>();

            List<int> birthConstraints = new List<int>();

            // getting the survival and birth ranges
            SurvivalAndBirthRange(ref survivalFirstValue, ref survivalLastValue,
                                  ref birthFirstValue, ref birthLastValue, ref survivalConstraints,
                                  ref birthConstraints);

            // success variable
            bool success = true;

            // performing checks
            
                // program should run in default values if no arguments are provided
                if (args.Length == 0)
                {
                    WriteLine("No command line arguments provided. Program will run on default mode");
                }
                // perform according to args
                else
                {
                    PerformingChecks(args, ref success, ref rows, ref columns,
                                     ref periodicMode, ref randomFactor, ref inputFile,
                                     ref generations, ref maxUpdateRate, ref stepMode,
                                     ref neighborhoodType, ref neighborhoodOrder, ref centreCount,
                                     ref survivalConstraints, ref birthConstraints, ref ghostMode,
                                     ref generationalMemory, ref outputFile, ref survivalFirstValue,
                                     ref survivalLastValue, ref birthFirstValue, ref birthLastValue);
                }
            

            // the game's main 2 dimensional array
            // stores all cell alive and dead statuses
            int[,] lifeGen = new int[rows, columns];

            // seed file reading and checking
            FileContents(ref lifeGen, ref inputFile, ref rows,
                         ref columns, ref randomFactor, (int)CellConstants.Alive, ref success, ref fileMode);

            // displaying runtime settings
            DisplayRuntimeSettings(ref inputFile, ref generations, ref maxUpdateRate, ref periodicMode,
                                   ref rows, ref columns, ref randomFactor, ref stepMode, ref success,
                                   ref outputFile, ref generationalMemory, ref survivalFirstValue,
                                   ref survivalLastValue, ref birthFirstValue, ref birthLastValue,
                                   ref neighborhoodType, ref neighborhoodOrder, ref ghostMode) ;

            // construct the game grid
            Grid grid = new Grid(rows, columns);

            // starting a new stopwatch timer
            Stopwatch watch = new Stopwatch();

            // initialize the grid window (this will resize the window and buffer)
            grid.InitializeWindow();

            grid.SetFootnote("");

            // temporary 2-dimensional array
            // used to store the cell status of each cell in the grid
            // for each generation
            int[,] tempGen = new int[rows, columns];

            // starting generations
            int currentGeneration = 0;

            // grid before generation rules of life execution
            UpdateInitialGrid(ref lifeGen, grid);

            // rendering updated cells
            grid.Render();

            while (ReadKey().Key != Spacebar) ;

            INeighbors neighborsnew = new OldNeighbors();

            if (neighborhoodType == "moore")
            {
                neighborsnew = new MooreNeighhborhood();
            }
            else if (neighborhoodType == "vonneumann")
            {
                neighborsnew = new VonNeumannNeighhborhood();
            }

            // game's main while loop
            // updating grid alive and dead cells according to rules of life
            // until generations limit is reached
            while (currentGeneration < generations)
            {
                for (int rowcheck = 0; rowcheck < lifeGen.GetLength(0); ++rowcheck)
                {
                    for (int columncheck = 0; columncheck < lifeGen.GetLength(1); ++columncheck)
                    {
                        // checking cell alive and dead status according rules of life
                        RulesOfLife(ref lifeGen, ref tempGen, ref rowcheck,
                                    ref columncheck, ref periodicMode, ref survivalConstraints,
                                    ref birthConstraints, neighborsnew, ref neighborhoodOrder, ref centreCount);
                    }
                }

                // CellStates(state, ref spot, cell, survival, birth, ref ghostMode);

                if (ghostMode)
                {

                    String[] QueueStrings = memoryQueue.ToArray();

                    int history = 1;

                    // getting old generations
                    for (int i = QueueStrings.Length - 1; i >= 0 && history < 4; --i)
                    {
                        int[,] previousLifeGen = GetArrayFromString(QueueStrings[i], rows, columns);

                        for (int rowcheck = 0; rowcheck < tempGen.GetLength(0); ++rowcheck)
                        {
                            for (int columncheck = 0; columncheck < tempGen.GetLength(1); ++columncheck)
                            {

                                if ((tempGen[rowcheck, columncheck] == (int)CellConstants.Dead) && (previousLifeGen[rowcheck, columncheck] == (int)CellConstants.Alive))
                                {

                                    switch (history)
                                    {
                                        case 1:

                                            tempGen[rowcheck, columncheck] = (int)CellConstants.Dark;
                                            break;

                                        case 2:

                                            tempGen[rowcheck, columncheck] = (int)CellConstants.Medium;
                                            break;

                                        case 3:

                                            tempGen[rowcheck, columncheck] = (int)CellConstants.Light;
                                            break;

                                        default:

                                            break;
                                    }
                                }
                            }
                        }

                        ++history;
                    }
                }

                 // step mode funtionality
                 StepModeSpacebar(ref stepMode);

                // update generation grid after rules of life
                UpdateGenerationGrid(ref lifeGen, ref tempGen, grid, (int)CellConstants.Dead);

                // updating memory
                isSteady = AddToQueue(lifeGen, ref generationalMemory, memoryQueue);

                if (isSteady)
                {
                    grid.IsComplete = true;
                    grid.Render();

                    if (fileMode == true)
                    {
                        WriteLine("steady state detected... periodicity = " + currentGeneration);
                    }

                    else
                    {
                        WriteLine("steady state detected... periodicity = N/A");
                    }

                    ReadLine();

                    break;
                }

                // frame rate according update rate
                FrameRatePerGen(ref maxUpdateRate, watch);

                // adding to the next generation
                ++currentGeneration;

                // displaying the generation number at the bottom right of the grid
                grid.SetFootnote("generation : " + currentGeneration);

                grid.Render();
            }

            if (!isSteady)
            {
                // clearing and resetting grid
                GameOver(grid);
            }

            else
            {
                grid.RevertWindow();
            }

            //TODO: CALL TO WRITE FUNCTION

            WriteToFile(ref lifeGen, ref outputFile);
        }
    }
}