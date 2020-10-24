using System;
using Display;
using System.IO;
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

            if (neighbors > 0)
            {
                // WriteLine(rowCheck + "," + columnCheck + "-" + neighbors);
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
        /// method fo checking validity
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
                               List<int> birth, bool ghostMode)
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
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="state"></param>
        static void cellseed(ref int[,] array, int row, int column, int state) 
        {
            array[row, column] = state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="state"></param>
        static void rectseed(ref int[,] array, int row, int column, int width,  int height, int state)
        {
            for (int x = row; x <= height; x++) 
            {
                for (int y = column; y <= width; y++)
                {
                    array[x, y] = state;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="state"></param>
        static void ellipseseed(ref int[,] array, int row, int column, int width, int height, int state)
        {
            //Console.WriteLine(row);
            //Console.WriteLine(column);
            //Console.WriteLine(width);
            //Console.WriteLine(height);
            //find mid point location if given coordinates
            WriteLine(row + "......" + column + "......" + width + "...." + height + "...." + state);

            //find mid point location if given coordinates
            double midx = (double)(height + row) / 2;
            double midy = (double)(width + column) / 2;

            //checking every cell in the array if the cell fits the required rule
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    /////////////////////ellipse equation///////////////////

                    //////////////////////height section of equation////////
                    double bracketsx = Math.Pow(i - midx, 2);
                    double numerx = (4 * bracketsx);
                    double denomx = Math.Pow(height - row + 1, 2);

                    ///////////////////////width section of equation///////
                    double bracketsy = Math.Pow(j - midy, 2);
                    double numery = (4 * bracketsy);
                    double denomy = Math.Pow(width - column + 1, 2);

                    ////////////////////////////combine both parts////////
                    double result = ((numerx / denomx) + (numery / denomy));

                    //if result is less than or equal to 1 then the cell is within the ellipse
                    if (result <= 1)
                    {
                        array[i, j] = state;
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
                                 int Alive)
        {
            // reading from seed file coordinates
            if (inputFile.Contains(".seed"))
            {
                // checking if the file can successfully be read
                try
                {
                    StreamReader seedFile = new StreamReader(inputFile);

                    //*************
                    seedFile.ReadLine();
                    //Read whole of seed file
                    string readall = seedFile.ReadToEnd().Trim();

                    //split by number of co-ordinates
                    string[] lines = readall.Split('\n');
                    string[] splited = new string[lines.Length];

                    int[] x_cor = new int[lines.Length];
                    int[] y_cor = new int[lines.Length];
                    string[] state = new string[lines.Length];
                    int[] height = new int[lines.Length];
                    int[] width = new int[lines.Length];

                    //Assigning values from seed to arrays
                    for (int x = 0; x < lines.Length; x++)
                    {

                        //split each line to their x and y
                        splited[x] = lines[x].Split(",")[0];
                        y_cor[x] = int.Parse(lines[x].Split(",")[1]);

                        state[x] = splited[x].Split(":")[0];
                        x_cor[x] = int.Parse(splited[x].Split(":")[1]);


                        if (!lines[x].Contains("cell"))
                        {
                            height[x] = int.Parse(lines[x].Split(",")[2]);
                            width[x] = int.Parse(lines[x].Split(",")[3]);
                        }
                    }

                    for (int x = 0; x < x_cor.Length; x++)
                    {
                        if (state[x].Contains("cell"))
                        {

                        }

                        else if (state[x].Contains("rectangle"))
                        {
                            if (state[x].Contains("(o)"))
                            {
                                rectseed(ref lifeGen, x_cor[x], y_cor[x], width[x], height[x], 1);
                            }
                            else
                            {
                                rectseed(ref lifeGen, x_cor[x], y_cor[x], width[x], height[x], 0);
                            }
                        }
                        else
                        {
                            if (state[x].Contains("(o)"))
                            {
                                ellipseseed(ref lifeGen, x_cor[x], y_cor[x], width[x], height[x], 1);
                            }
                            else
                            {
                                ellipseseed(ref lifeGen, x_cor[x], y_cor[x], width[x], height[x], 0);
                            }
                        }
                    }
                }
                //*************




                //    string fileContents = "";

                //    List<int> rowsList = new List<int>();

                //    List<int> columnsList = new List<int>();

                //    List<int> widthList = new List<int>();

                //    List<int> heightList = new List<int>();

                //    List<string> setting = new List<string>();

                //    int listCount = 0;

                //    int rowsMax = 0;

                //    int columnsMax = 0;

                //    int row = 0;
                //    int column = 0;
                //    int width = 0;
                //    int height = 0;

                //    bool tooBig = false;
                //    if (seedFile.ReadLine() == "#version=2.0")
                //    {
                //        while ((fileContents = seedFile.ReadLine()) != null)
                //        {

                //            string ar = fileContents.Trim().Split(":")[0];
                //            string ar1 = fileContents.Trim().Split(":")[1];

                //            setting.Add(ar);

                //             row = int.Parse(ar1.Split(",")[0]);
                //            rowsList.Add(row);

                //             column = int.Parse(ar1.Split(",")[1]);
                //            columnsList.Add(column);

                //            if (!ar.Contains("cell"))
                //            {
                //                 width = int.Parse(ar1.Split(",")[2]);
                //                widthList.Add(width);

                //                 height = int.Parse(ar1.Split(",")[3]);
                //                heightList.Add(height);
                //            }
                //            //WriteLine(row +"......"+ column + "......" + width +"...." + height);
                //        }

                //        for (int x = 0; x < setting.Count; x++)
                //        {

                //            if (setting[x].Contains("cell"))
                //            {
                //                if (setting[x].Contains("(o)"))
                //                {
                //                    cellseed(ref lifeGen, rowsList[x], columnsList[x], 1);
                //                }
                //                else
                //                {
                //                    cellseed(ref lifeGen, rowsList[x], columnsList[x], 0);
                //                }
                //            }

                //            else if (setting[x].Contains("rectangle"))
                //            {
                //                if (setting[x].Contains("(o)"))
                //                {
                //                    rectseed(ref lifeGen, rowsList[x], columnsList[x], widthList[x], heightList[x], 1);
                //                }
                //                else
                //                {
                //                    rectseed(ref lifeGen, rowsList[x], columnsList[x], widthList[x], heightList[x], 0);
                //                }
                //            }

                //            else if (setting[x].Contains("ellipse"))
                //            {
                //                //WriteLine(setting[x] +"......"+rowsList[x] + "......" + columnsList[x] + "......" + widthList[x] + "...." + heightList[x]);
                //                if (setting[x].Contains("(o)"))
                //                {
                //                    ellipseseed(ref lifeGen, rowsList[x], columnsList[x], widthList[x], heightList[x], 1);
                //                }
                //                else
                //                {
                //                    ellipseseed(ref lifeGen, rowsList[x], columnsList[x], widthList[x], heightList[x], 0);
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        // reading till the end of the file
                //        while ((fileContents = seedFile.ReadLine()) != null)
                //        {
                //            // reading row values
                //            rowsList.Add(ToInt32(fileContents.Split(' ')[0]));

                //            // reading column values
                //            columnsList.Add(ToInt32(fileContents.Split(' ')[1]));

                //            // rows are lower than seed row size
                //            if (ToInt32(fileContents.Split(' ')[0]) > rows - 1)
                //            {
                //                tooBig = true;
                //            }

                //            // columns are lower than seed column size
                //            if (ToInt32(fileContents.Split(' ')[1]) > columns - 1)
                //            {
                //                tooBig = true;
                //            }

                //            // recommended max row value
                //            if (ToInt32(fileContents.Split(' ')[0]) > rowsMax)
                //            {
                //                rowsMax = ToInt32(fileContents.Split(' ')[0]);
                //            }

                //            // recommened max column value
                //            if (ToInt32(fileContents.Split(' ')[1]) > columnsMax)
                //            {
                //                columnsMax = ToInt32(fileContents.Split(' ')[1]);
                //            }

                //            ++listCount;
                //        }

                //        // if the dimensions are not enough
                //        if (tooBig == true)
                //        {
                //            if (rowsMax < 3)
                //            {
                //                rowsMax += 3;
                //            }

                //            if (columnsMax < 3)
                //            {
                //                columnsMax += 3;
                //            }
                //            Error.WriteLine(" The dimension size is too small." +
                //                            "the recommended minimum size is {0}, {1}"
                //                            , rowsMax + 1, columnsMax + 1);

                //            Randomness(ref lifeGen, randomFactor, Alive);
                //        }

                //        // if dimensions ar egood enough
                //        else
                //        {
                //            for (int listNumber = 0; listNumber < rowsList.Count; ++listNumber)
                //            {
                //                lifeGen[rowsList[listNumber], columnsList[listNumber]] = Alive;
                //            }
                //        }
                //    }
                //}
                //// else display error and execute randomly generated cells
                catch
                {
                    Error.WriteLine("The provided file path is invalid");

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
            WriteLine(String.Format("{0, 15} : ", "Rules" + "S( " + survivalFirstValue + "..." +
                      survivalLastValue + " ) B( " + birthFirstValue + "..." + birthLastValue + " )"));

            // neighborhood
            WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", UppercaseFirst(neighborhoodType) +
                      " (" + neighborhoodOrder + ")"));

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


        static int periodicCalculation(int generationCountState, int rows, int columns, int[,] memoryarray)
        {
            //making variable at position of start of final array in steadystate array
            int steadystate_x = generationCountState;
            int count = 0;
            int periodicity = 0;

            //calculating generation at which steady state was reached
            for (int x = 0; x < generationCountState + rows; x++)
            {
                if (count == (rows * columns))
                {
                    return periodicity - 1;
                }

                if (x % rows == 0)
                {
                    count = 0;
                    periodicity++;
                }

                if (steadystate_x == generationCountState + (rows))
                {
                    steadystate_x = generationCountState;
                }

                for (int y = 0; y < columns; y++)
                {
                    if (memoryarray[x, y] == memoryarray[steadystate_x, y])
                    {
                        count++;
                    }
                }

                steadystate_x++;
            }

            return 0;
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
            int rows = 48;
            int columns = 48;
            bool periodicMode = true;
            double randomFactor = 0.5;
            string inputFile = "";
            int generations = 300;
            double maxUpdateRate = 15;
            bool stepMode = false;
            string neighborhoodType = "moore";
            int neighborhoodOrder = 5;
            bool centreCount = true;
            bool ghostMode = false;
            int generationalMemory = 16;
            string outputFile = "";
            int survivalFirstValue = 2;
            int survivalLastValue = 3;
            int birthFirstValue = 3;
            int birthLastValue = 3;

            List<int> survivalConstraints = new List<int>();

            // survivalConstraints.Add(2);
            // survivalConstraints.Add(3);

            //if ((survivalFirstValue > survivalLastValue) || (survivalFirstValue < 0))
            //{
            //    throw new Exception("error in first value");
            //}

            //for (int i = survivalFirstValue; i <= survivalLastValue; ++i)
            //{
            //    survivalConstraints.Add(i);
            //}

            //// survivalConstraints.ForEach(Console.WriteLine);

            List<int> birthConstraints = new List<int>();
            //// birthConstraints.Add(3);

            //// int birthFirstValue = 34;
            //// int birthLastValue = 45;

            //if ((birthFirstValue > birthLastValue) || (birthFirstValue < 0))
            //{
            //    throw new Exception("error in first value");
            //}

            //for (int i = birthFirstValue; i <= birthLastValue; ++i)
            //{
            //    birthConstraints.Add(i);
            //}

            // birthConstraints.ForEach(Console.WriteLine);

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
                         ref columns, ref randomFactor, (int)CellConstants.Alive);

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

                // CellStates(ref int state, ref int spot, ref int cell, ref List<int> survival, ref List<int> birth, ref ghostMode)

                // step mode funtionality
                StepModeSpacebar(ref stepMode);

                // update generation grid after rules of life
                UpdateGenerationGrid(ref lifeGen, ref tempGen, grid, (int)CellConstants.Dead);

                // frame rate according update rate
                FrameRatePerGen(ref maxUpdateRate, watch);

                // adding to the next generation
                ++currentGeneration;

                // displaying the generation number at the bottom right of the grid
                grid.SetFootnote("generation : " + currentGeneration);

                grid.Render();
            }

            // clearing amnd resetting grid
            GameOver(grid);
        }
    }
}
