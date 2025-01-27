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
    /// <summary>
    /// interface to count the neighbors of a cell
    /// </summary>
    interface INeighbors
    {
        /// <summary>
        /// method to get the nieghbors
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        /// <returns></returns>
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                         ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount);
    }

    /// <summary>
    /// class of moore neighborhood implemented from neighbor identifying interface
    /// </summary>
    class MooreNeighhborhood : INeighbors
    {
        /// <summary>
        /// method to get the neighbors
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        /// <returns></returns>
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                                ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount)
        {
            // initializing neighbors
            int neighbors = 0;

            // moore neighbhor cell boundaries
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

    /// <summary>
    /// class of von veumann neighborhood implemented from neighbor identifying interface
    /// </summary>
    class VonNeumannNeighhborhood : INeighbors
    {
        /// <summary>
        /// method to get the neighbors
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        /// <returns></returns>
        public int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck,
                                ref bool periodicMode, ref int neighborhoodOrder, ref bool centreCount)
        {
            // initializing neighbors
            int neighbors = 0;

            // von neumann neighbhor cell boundaries
            int PreviousCell = -1 * neighborhoodOrder;
            int NextCell = neighborhoodOrder;

            for (int rowCoordinate = PreviousCell; rowCoordinate <= NextCell; ++rowCoordinate)
            {
                for (int columnCoordinate = PreviousCell; columnCoordinate <= NextCell; ++columnCoordinate)
                {
                    // if opertating in non-periodic mode
                    if (rowCoordinate == 0 && columnCoordinate == 0 && (!centreCount))
                    {
                        continue;
                    }

                    // current row and column number
                    int rowNumberCurrent = rowCheck + rowCoordinate;
                    int columnNumberCurrent = columnCheck + columnCoordinate;

                    // temporary boolean variable for neighbor checking
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

                    // von neumann neighbor counting logic
                    if (proceed == true)
                    {
                        if (neighborhoodOrder != 1)
                        {
                            if ((Abs(rowCoordinate) + Abs(columnCoordinate)) <= neighborhoodOrder)
                            {
                                neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                            }
                        }
                        else if (neighborhoodOrder == 1)
                        {
                            if ((Pow(rowCoordinate, 2) + Pow(columnCoordinate, 2)) <= Pow(neighborhoodOrder, 2))
                            {
                                neighbors += lifeGen[rowNumberCurrent, columnNumberCurrent];
                            }
                        }
                    }
                }
            }

            // returning neighbors to rules of life
            return neighbors;
        }
    }

    /// <summary>
    /// class for counting old neighbors
    /// </summary>
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

    /// <summary>
    /// class for the main program containing multiple checking and functionality methods
    /// </summary>
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
        /// <param name="inputSurvival"></param>
        /// <param name="inputBirth"></param>
        public static void PerformingChecks(string[] args, ref bool success, ref int rows,
                                     ref int columns, ref bool periodicMode,
                                     ref double randomFactor, ref string inputFile,
                                     ref int generations, ref double maxUpdateRate,
                                     ref bool stepMode, ref string neighborhoodType,
                                     ref int neighborhoodOrder, ref bool centreCount,
                                     ref List<int> survivalConstraints,
                                     ref List<int> birthConstraints, ref bool ghostMode,
                                     ref int generationalMemory, ref string outputFile,
                                     ref string inputSurvival, ref string inputBirth,
                                     ref bool surCLIWorks, ref bool birCLIWorks)
        {
            // performing argument check until the end of arguemnts
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
                                 ref success, ref inputSurvival, ref inputBirth,
                                 ref surCLIWorks, ref birCLIWorks);

                // ghost mode check
                GhostMode(args, index, ref ghostMode);

                // generational memroy check
                GenMemory(args, index, ref generationalMemory, ref success);

                // output file check
                OutputFile(args, index, ref outputFile, ref success);
            }
        }

        /// <summary>
        /// method for steady state implementation
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="limit"></param>
        /// <param name="memoryQueue"></param>
        public static bool AddToQueue(int[,] lifeGen, ref int limit, Queue<string> memoryQueue)
        {
            // calling from array to string conversion
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

        /// <summary>
        /// method for checking for the periodicty
        /// </summary>
        /// <param name="memoryQueue"></param>
        /// <returns></returns>
        public static int Periodicty(Queue<string> memoryQueue)
        {
            string last = memoryQueue.ToList()[memoryQueue.Count - 1];

            //WriteLine(memoryQueue.Count);

            for (int x = 0; x < memoryQueue.Count; x++)
            {
                if (memoryQueue.ToList()[x] == last)
                {
                    return x;
                }
            }
            return 0;
        }

        /// <summary>
        /// method for converting from string to array
        /// </summary>
        /// <param name="arrayText"></param>
        /// <param name="upperLen"></param>
        /// <param name="lowerLen"></param>
        /// <returns></returns>
        public static int[,] GetArrayFromString(string arrayText, int upperLen, int lowerLen)         {
            // method souce: - stack overflow
            string[] parsed = arrayText.Split(",");             int[,] output = new int[upperLen, lowerLen];              for (var upper = 0; upper < upperLen; upper++)
            {
                for (var lower = 0; lower < lowerLen; lower++)
                {
                    output[upper, lower] = int.Parse(parsed[(upper * lowerLen) + lower]);
                }
            }                              return output;         }

        /// <summary>
        /// converting from array to string
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static string GetStringFromArray(int[,] lst)         {
            // method source: - stack overflow
            return string.Join(',', lst.Cast<int>());         } 
        /// <summary>
        /// method for output file check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="outputFile"></param>
        /// <param name="success"></param>
        public static void OutputFile(string[] args, int index, ref string outputFile, ref bool success)
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

                        success = false;
                        Error.WriteLine("the value must be a valid absolute or relative file path. " +
                                        "the value must be a valid file path having the.seed file extension.");
                    }
                }
                // no paramter provided
                catch (IndexOutOfRangeException ex)
                {
                    outputFile = "";

                    success = false;
                    Error.WriteLine(ex.Message);
                    Error.WriteLine("1 output file parameter is missing");
                }
            }
        }

        /// <summary>
        /// method for generational memory check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="generationalMemory"></param>
        /// <param name="success"></param>
        public static void GenMemory(string[] args, int index, ref int generationalMemory,
                                              ref bool success)
        {
            // --memory args
            if (args[index] == "--memory")
            {
                // checking if parameter provided
                try
                {
                    // if parameter is integer, store generational memory
                    if (int.TryParse(args[index + 1], out generationalMemory))
                    {
                        // if integer >= 4 and <= 512, keep generational memory
                        // else display error and assign defualt generational memory
                        if (!((4 <= generationalMemory) && (generationalMemory <= 512)))
                        {
                            generationalMemory = 16;

                            success = false;
                            Error.WriteLine("Generational memory must be an integer betwen 4 and 512 (inclusive)");
                        }
                    }
                    // else display error and assign default generational memory
                    else
                    {
                        generationalMemory = 16;

                        success = false;
                        Error.WriteLine("The value must be an integer");
                    }
                }
                // no parameter provided
                // display error and assign defult generational memory
                catch
                {
                    generationalMemory = 16;

                    success = false;
                    Error.WriteLine("1 generational memory parameter is missing");
                }
            }
        }

        /// <summary>
        /// method for ghost mode check
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="ghostMode"></param>
        public static void GhostMode(string[] args, int index, ref bool ghostMode)
        {
            // --ghost args
            if (args[index] == "--ghost")
            {
                ghostMode = true;
            }
        }

        /// <summary>
        /// method for checking survival and birth constraints
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="survivalConstraints"></param>
        /// <param name="birthConstraints"></param>
        /// <param name="success"></param>
        /// <param name="inputSurvival"></param>
        /// <param name="inputBirth"></param>
        public static void SurvivalAndBirth(string[] args, int index, ref List<int> survivalConstraints,
                                     ref List<int> birthConstraints, ref bool success,
                                     ref string inputSurvival, ref string inputBirth,
                                     ref bool surCLIWorks, ref bool birCLIWorks)
        {
            // --survival args
            if (args[index] == "--survival")
            {
                survivalConstraints.Clear();

                int count = 0;

                // loop to include all parameters until the next argument call
                while (index + count < args.Length - 1)
                {
                    int pos = index + count;

                    try
                    {
                        // if the next argument call is not detected
                        if (!args[pos + 1].Contains("--"))
                        {
                            if (index + count == args.Length - 2)
                            {
                                inputSurvival += args[pos + 1];
                            }
                            else
                            {
                                inputSurvival += args[pos + 1] + " ";
                            }
                            // if a range is supplied for survival parameter(s)
                            if (args[pos + 1].Contains("..."))
                            {
                                // seperating first value from range parameter
                                int first = int.Parse(args[(pos - count + 1) + count].Split("...")[0]);

                                // seperating second value from range parameter
                                int last = int.Parse(args[(pos - count + 1) + count].Split("...")[1]);

                                for (int k = first; k <= last; ++k)
                                {
                                    survivalConstraints.Add(k);
                                }
                            }
                            else
                            {
                                // if survival parameter is less than zero
                                if (int.Parse(args[pos + 1]) < 0)
                                {
                                    Error.WriteLine("Survival value(s) must be a positive integer");
                                }
                                else
                                {
                                    int survivalValue = int.Parse(args[pos + 1]);
                                    survivalConstraints.Add(survivalValue);
                                }
                            }
                            ++count;
                        }
                        // if detected, break out
                        else
                        {
                            break;
                        }
                    }
                    // invalid format or missing parameter
                    catch (FormatException ex)
                    {
                        ++count;

                        success = false;
                        Error.WriteLine(ex.Message);
                        Error.WriteLine("1 or more survival parameter(s) " +
                                        "are not in the correct format or missing");
                    }
                }
                // if empty, assign default values
                if (inputSurvival == "")
                {
                    survivalConstraints.Add(2);
                    survivalConstraints.Add(3);

                    inputSurvival = "2...3";

                    success = false;
                    surCLIWorks = false;
                }
            }

            // --birth args
            if (args[index] == "--birth")
            {
                birthConstraints.Clear();

                int count = 0;

                // loop to include all parameters until the next argument call
                while (index + count < args.Length - 1)
                {
                    int pos = index + count;

                    try
                    {
                        // if the next argument call is not detected
                        if (!args[pos + 1].Contains("--"))
                        {
                            if (index + count == args.Length - 2)
                            {
                                inputBirth += args[pos + 1];
                            }
                            else
                            {
                                inputBirth += args[pos + 1] + " ";
                            }
                            // if a range is supplied for birth parameter(s)
                            if (args[pos + 1].Contains("..."))
                            {
                                // seperating first value from range parameter
                                int first = int.Parse(args[(pos - count + 1) + count].Split("...")[0]);

                                // seperating second value from range parameter
                                int last = int.Parse(args[(pos - count + 1) + count].Split("...")[1]);

                                for (int k = first; k <= last; ++k)
                                {
                                    birthConstraints.Add(k);
                                }
                            }
                            else
                            {
                                // if birth parameter is less than zero
                                if (int.Parse(args[pos + 1]) < 0)
                                {
                                    Error.WriteLine("Birth value(s) must be a positive integer");
                                }
                                else
                                {
                                    int birthValue = int.Parse(args[pos + 1]);
                                    birthConstraints.Add(birthValue);
                                }
                            }
                            ++count;
                        }
                        // if detected, break out
                        else
                        {
                            break;
                        }
                    }
                    // invalid format or missing parameter
                    catch (FormatException ex)
                    {
                        ++count;

                        success = false;
                        Error.WriteLine(ex.Message);
                        Error.WriteLine("1 or more birth parameter(s) " +
                                        "are not in the correct format or missing");
                    }
                }
                // if empty, assign default values
                if (inputBirth == "")
                {
                    birthConstraints.Add(3);

                    inputBirth = "3";

                    success = false;
                    birCLIWorks = false;
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
        public static void Neighborhood(string[] args, int index, ref string neighborhoodType,
                                 ref int neighborhoodOrder, ref bool centreCount, ref bool success,
                                 ref int rows, ref int columns)
        {
            int missing = 0;

            // --neighbopr args
            if (args[index] == "--neighbour")
            {
                // checking neighborhood type
                try
                {
                    // if moore neighborhood was passed
                    if (args[index + 1].ToLower().Equals("moore"))
                    {
                        neighborhoodType = args[index + 1].ToLower();
                    }
                    // if von neumann neighborhood was passed
                    else if (args[index + 1].ToLower().Equals("vonneumann"))
                    {
                        neighborhoodType = args[index + 1].ToLower();
                    }
                    // invalid neighborhood passed
                    else
                    {
                        neighborhoodType = "moore";

                        success = false;
                        Error.WriteLine("neighboorhood type must be either moore or vonneumann");
                    }
                }
                // neighborhood arguments have not been provided
                catch (IndexOutOfRangeException ex)
                {
                    neighborhoodType = "moore";

                    success = false;
                    Error.WriteLine(ex.Message);
                    ++missing;
                }

                // checking neighborhood order
                try
                {
                    // if neighborhood order was provided as an integer
                    if (int.TryParse(args[index + 2], out neighborhoodOrder))
                    {
                        // if neighborhood order was inside the specified range
                        if (1 <= neighborhoodOrder && neighborhoodOrder <= 10)
                        {
                            // neighborhood order value requirement
                            int minOrder = Min(rows, columns) / 2;

                            if (!(neighborhoodOrder < minOrder))
                            {
                                neighborhoodOrder = 1;

                                success = false;
                                Error.WriteLine("nieghborhood order must be less than {0}", minOrder);
                            }
                        }
                        // if provided outside the range
                        else
                        {
                            neighborhoodOrder = 1;

                            success = false;
                            Error.WriteLine("nieghborhood order must be a positive integer " +
                                            "between 1 and 10(inclusive).");
                        }
                    }
                    // if not provided as an integer
                    else
                    {
                        neighborhoodOrder = 1;

                        success = false;
                        Error.WriteLine("nieghborhood order must be provided");
                    }
                }
                // neighborhood order argument not provided
                catch (IndexOutOfRangeException ex)
                {
                    neighborhoodOrder = 1;

                    success = false;
                    Error.WriteLine(ex.Message);
                    ++missing;
                }

                // checking centre count
                try
                {
                    // if centre count was provided
                    if (!bool.TryParse(args[index + 3].ToLower(), out centreCount))
                    {
                        centreCount = false;

                        success = false;
                        Error.WriteLine("centre count must be of type boolean (true or false)");
                    }
                }
                // centre count argument was not provided
                catch (IndexOutOfRangeException ex)
                {
                    centreCount = false;

                    success = false;
                    Error.WriteLine(ex.Message);
                    ++missing;
                }

                switch (missing)
                {
                    case 0:

                        break;

                    case 1:

                        Error.WriteLine("1 neighbour parameter is missing");
                        success = false;
                        break;

                    case 2:

                        Error.WriteLine("2 neighbour parameters are missing");
                        success = false;
                        break;

                    case 3:

                        Error.WriteLine("3 neighbour parameters are missing");
                        success = false;
                        break;

                    default:

                        break;
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
        /// <param name="Alive"></param>
        public static void Randomness(ref int[,] lifeGen, double randomFactor, int Alive)
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
        public static void Dimensions(string[] args, int index, ref bool success,
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
                                Error.WriteLine("Rows must be a positive integer between 4 and 48(inclusive).");
                            }
                        }
                        // displaying error and assigning default rows
                        else
                        {
                            rows = 16;

                            success = false;
                            Error.WriteLine("Provided row value must be an integer");
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
                                Error.WriteLine("Columns must be a positive integer between 4 and 48(inclusive).");
                            }
                        }
                        // else displaying error and assigning default rows
                        else
                        {
                            columns = 16;

                            success = false;
                            Error.WriteLine("Provided column value must be an integer");
                        }
                    }
                }
                // no parametrs provided
                catch (IndexOutOfRangeException ex)
                {
                    rows = 16;
                    columns = 16;

                    success = false;
                    Error.WriteLine(ex.Message);
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
        public static void RandomFactor(string[] args, int index, ref double randomFactor,
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
                catch (IndexOutOfRangeException ex)
                {
                    randomFactor = 0.5;

                    success = false;
                    Error.WriteLine(ex.Message);
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
        public static void InputFile(string[] args, int index, ref string inputFile,
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

                        success = false;
                        Error.WriteLine("the value must be a valid absolute or relative file path. " +
                                        "the value must be a valid file path having the.seed file extension.");
                    }
                }
                // no paramter provided
                catch (IndexOutOfRangeException ex)
                {
                    inputFile = "";

                    success = false;
                    Error.WriteLine(ex.Message);
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
        public static void Generations(string[] args, int index, ref int generations,
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
                catch (IndexOutOfRangeException ex)
                {
                    generations = 50;

                    success = false;
                    Error.WriteLine(ex.Message);
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
        public static void MaxUpdateRate(string[] args, int index, ref double maxUpdateRate,
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
                catch (IndexOutOfRangeException ex)
                {
                    maxUpdateRate = 5;

                    success = false;
                    Error.WriteLine(ex.Message);
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
        public static void StepMode(string[] args, int index, ref bool stepMode)
        {
            // --step args
            if (args[index] == "--step")
            {
                stepMode = true;
            }
        }

        /// <summary>
        /// reading cell seeds
        /// </summary>
        /// <param name="array"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="state"></param>
        public static void CellSeed(ref int[,] seedArray, int row, int column, int state)
        {
            // cell logic
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
        public static void RectSeed(ref int[,] seedArray, int row, int column, int width, int height,
                                    int state)
        {
            // rectangle logic
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
        /// <param name="seedArray"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="state"></param>
        public static void EllipseSeed(ref int[,] seedArray, int row, int column, int width, int height,
                                       int state)
        {
            // determining the centre
            double centreX = (double)(row + height) / 2;
            double centreY = (double)(width + column) / 2;

            // ellogic
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
        /// <param name="fileMode"></param>
        /// <param name="success"></param>
        public static void FileContents(ref int[,] lifeGen, ref string inputFile,
                                 ref int rows, ref int columns, ref double randomFactor,
                                 int Alive, ref bool fileMode, ref bool success)
        {
            // reading from seed file coordinates
            if (inputFile.Contains(".seed"))
            {
                bool readCheck = true;

                // checking if the file can successfully be read
                try
                {
                    StreamReader seedFile = new StreamReader(inputFile);
                }

                // if the provided file path is invlaid
                catch (FileNotFoundException ex)
                {
                    success = false;
                    Error.WriteLine(ex.Message);
                    Error.WriteLine("The provided file path is invalid. Cells will be randomly generated");

                    readCheck = false;
                    Randomness(ref lifeGen, randomFactor, Alive);
                }

                if (readCheck == true)
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

                        // seed file row coordinates
                        int[] coordinateX = new int[linesplit.Length];

                        // seed file column coordinates
                        int[] coordinateY = new int[linesplit.Length];

                        // alive/dead with structre of the seed
                        string[] state = new string[linesplit.Length];
                        int[] height = new int[linesplit.Length];
                        int[] width = new int[linesplit.Length];

                        // assigning values from seed to arrays
                        for (int x = 0; x < linesplit.Length; x++)
                        {
                            //splitting each line to y coordinates
                            linesLength[x] = linesplit[x].Split(",")[0];
                            coordinateY[x] = int.Parse(linesplit[x].Split(",")[1]);

                            // splitting each line to x coordinates
                            state[x] = linesLength[x].Split(":")[0];
                            coordinateX[x] = int.Parse(linesLength[x].Split(":")[1]);

                            // splitting each line to x and y i.e height and width
                            if (!linesplit[x].Contains("cell"))
                            {
                                height[x] = int.Parse(linesplit[x].Split(",")[2]);
                                width[x] = int.Parse(linesplit[x].Split(",")[3]);
                            }
                        }

                        // checking through the different structure types
                        for (int x = 0; x < coordinateX.Length; x++)
                        {
                            // if seed state contains cell
                            if (state[x].Contains("cell"))
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    CellSeed(ref lifeGen, coordinateX[x], coordinateY[x],
                                             (int)CellConstants.Alive);
                                }
                                else
                                {
                                    CellSeed(ref lifeGen, coordinateX[x], coordinateY[x],
                                             (int)CellConstants.Dead);
                                }
                            }
                            // if seed state contains ractangle
                            else if (state[x].Contains("rectangle"))
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    RectSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x],
                                             height[x], (int)CellConstants.Alive);
                                }
                                else
                                {
                                    RectSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x],
                                             height[x], (int)CellConstants.Dead);
                                }
                            }
                            // if seed state contains ellipse
                            else
                            {
                                if (state[x].Contains("(o)"))
                                {
                                    EllipseSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x],
                                                height[x], (int)CellConstants.Alive);
                                }
                                else
                                {
                                    EllipseSeed(ref lifeGen, coordinateX[x], coordinateY[x], width[x],
                                                height[x], (int)CellConstants.Dead);
                                }
                            }
                        }
                    }

                    // old version of seed files
                    else
                    {
                        List<int> rowsList = new List<int>();
                        List<int> columnsList = new List<int>();

                        string fileContents = "";

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

                        // if dimensions are good enough
                        else
                        {
                            for (int listNumber = 0; listNumber < rowsList.Count; ++listNumber)
                            {
                                lifeGen[rowsList[listNumber], columnsList[listNumber]] = Alive;
                            }
                        }
                    }
                }

                fileMode = true;
            }

            // else display error and execute randomly generated cells
            else
            {
                Randomness(ref lifeGen, randomFactor, Alive);
            }
        }

        /// <summary>
        /// method to display the runtime settings
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="generations"></param>
        /// <param name="maxUpdateRate"></param>
        /// <param name="periodicMode"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="randomFactor"></param>
        /// <param name="stepMode"></param>
        /// <param name="success"></param>
        /// <param name="outputFile"></param>
        /// <param name="generationalMemory"></param>
        /// <param name="neighborhoodType"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="ghostMode"></param>
        /// <param name="centreCount"></param>
        public static void DisplayRuntimeSettings(ref string inputFile, ref int generations,
                                           ref double maxUpdateRate, ref bool periodicMode,
                                           ref int rows, ref int columns, ref double randomFactor,
                                           ref bool stepMode, ref bool success, ref string outputFile,
                                           ref int generationalMemory, ref string neighborhoodType,
                                           ref int neighborhoodOrder, ref bool ghostMode,
                                           ref bool centreCount, ref bool argsProvided,
                                           ref string inputSurvival, ref string inputBirth)
        {
            // display the game settings
            // success rate of args
            if (argsProvided == true)
            {
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
            if (inputSurvival == "" && inputBirth == "")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Rules", "S( 2...3 ) B( 3 )"));

            }
            else if (inputSurvival == "")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Rules", "S( 2...3 )  B( " + inputBirth + " )"));

            }
            else if (inputBirth == "")
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Rules", "S( " + inputSurvival + " )  B( 3 )"));

            }
            else
            {
                WriteLine(String.Format("{0, 15} : {1, -10}", "Rules", "S( " + inputSurvival + " ) B( " +
                                        inputBirth + " )"));
            }


            // neighborhood
            if (neighborhoodType == "moore")
            {
                if (!centreCount)
                {
                    WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", "Moore"
                          + " (" + neighborhoodOrder + ") (centre-not-counted)"));
                }
                else
                {
                    WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", "Moore"
                          + " (" + neighborhoodOrder + ") (centre-counted)"));
                }
            }
            else
            {
                if (!centreCount)
                {
                    WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", "VonNeumann"
                          + " (" + neighborhoodOrder + ") (centre-not-counted)"));
                }
                else
                {
                    WriteLine(String.Format("{0, 15} : {1, -10}", "Neighborhood", "VonNeumann"
                          + " (" + neighborhoodOrder + ") (centre-counted)"));
                }
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
        public static void StepModeSpacebar(ref bool stepMode)
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
        public static void UpdateInitialGrid(ref int[,] lifeGen, Grid grid)
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
        public static void UpdateGenerationGrid(ref int[,] lifeGen, ref int[,] tempGen,
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
        public static void FrameRatePerGen(ref double maxUpdateRate, Stopwatch watch)
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
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="tempGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <param name="survivalConstraints"></param>
        /// <param name="birthConstraints"></param>
        /// <param name="neighborsnew"></param>
        /// <param name="neighborhoodOrder"></param>
        /// <param name="centreCount"></param>
        public static void RulesOfLife(ref int[,] lifeGen, ref int[,] tempGen, ref int rowCheck,
                                ref int columnCheck, ref bool periodicMode, ref List<int> survivalConstraints,
                                ref List<int> birthConstraints, INeighbors neighborsnew,
                                ref int neighborhoodOrder, ref bool centreCount)
        {
            // calling from the GetNeighbors method
            // obtaining nieghbors in current cell
            int neighbors = neighborsnew.GetNeighbors(ref lifeGen, ref rowCheck, ref columnCheck, ref periodicMode,
                                                      ref neighborhoodOrder, ref centreCount);

            // final check for cell alive or dead
            // if cell alive, will it survive?
            if (lifeGen[rowCheck, columnCheck] == (int)CellConstants.Alive)
            {
                // if cell has exactly live neighbours according to the constraints, it survives
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
                // if dead cell has live neighbours according to the constraints,
                // it resurrects next generation
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
        public static void GameOver(Grid grid, ref bool isSteady)
        {
            // set complete marker as true
            grid.IsComplete = true;

            // render updates to the console window (grid should now display COMPLETE)...
            grid.Render();

            if (isSteady == false)
            {
                // console closes if spacebar is pressed after the program ends
                while (true)
                {
                    if (ReadKey().Key == Spacebar)
                    {
                        break;
                    }
                }

                grid.RevertWindow();

                WriteLine("steady state not detected...");

                // console closes if spacebar is pressed after the program ends
                while (true)
                {
                    if (ReadKey().Key == Spacebar)
                    {
                        break;
                    }
                }
            }

            grid.RevertWindow();

            WriteLine("Press spacebar to close the program...");

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
        /// method to write to the output file
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="outputFile"></param>
        private static void WriteToFile(ref int[,] lifeGen, ref string outputFile)
        {
            // specifying the file path
            string path = outputFile;

            if (outputFile == "")
            {

            }

            else
            {
                // writing the cell coordinates to thge file in the specified format
                if (!File.Exists(path))
                {
                    try
                    {
                        // creating the file path
                        using StreamWriter file = File.CreateText(path);

                        // adding version header
                        file.WriteLine("#version=2.0");

                        for (int rowcheck = 0; rowcheck < lifeGen.GetLength(0); ++rowcheck)
                        {
                            for (int columncheck = 0; columncheck < lifeGen.GetLength(1); ++columncheck)
                            {
                                if (lifeGen[rowcheck, columncheck] == (int)CellConstants.Alive)
                                {
                                    string stringToWrite = "(o) cell : " + rowcheck + ", " + columncheck;

                                    file.WriteLine(stringToWrite);
                                }
                            }
                        }

                        WriteLine("An output file was successfully written to the provided file path");
                    }

                    catch (IndexOutOfRangeException ex)
                    {
                        Error.WriteLine(ex.Message);
                        Error.WriteLine("The provided output file path is invalid");
                    }
                }
            }
        }

        /// <summary>
        /// method for the ghost mode logic
        /// </summary>
        /// <param name="memoryQueue"></param>
        /// <param name="tempGen"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public static void GhostModeCells(Queue<string> memoryQueue, int[,] tempGen, int rows,
                                   int columns)
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
                        // if the cell was dead in the current generation and alive in the previous,
                        // it has a whole new way of coming to the final state now (3 stages)
                        if ((tempGen[rowcheck, columncheck] == (int)CellConstants.Dead) &&
                            (previousLifeGen[rowcheck, columncheck] == (int)CellConstants.Alive))
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

        // enumeration of repeating constants used in Main function
        public enum CellConstants
        {
            Alive = 1,
            Dead = 0,
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
            string inputFile = "";
            int generations = 50;
            double maxUpdateRate = 5;
            bool stepMode = false;
            string neighborhoodType = "moore";
            int neighborhoodOrder = 1;
            bool centreCount = false;
            bool ghostMode = false;
            int generationalMemory = 16;
            string outputFile = "";
            bool isSteady = false;
            bool fileMode = false;
            int survivalFirst = 2;
            int survivalLast = 3;
            int birthOnly = 3;
            bool surCLIWorks = true;
            bool burCLIWorks = true;

            // input survival and birth value string
            string inputSurvival = "";
            string inputBirth = "";

            // survival and birth list variables
            List<int> survivalConstraints = new List<int>();
            List<int> birthConstraints = new List<int>();

            // steady state queue variable
            Queue<string> memoryQueue = new Queue<string>();

            // success variable
            bool success = true;

            // args provided check variable
            bool argsProvided = false;

            // performing checks
            // program should run in default values if no arguments are provided
            if (args.Length == 0)
            {
                WriteLine("No command line arguments provided. Program will run on default mode");
            }
            // perform according to args
            else
            {
                argsProvided = true;

                PerformingChecks(args, ref success, ref rows, ref columns,
                                 ref periodicMode, ref randomFactor, ref inputFile,
                                 ref generations, ref maxUpdateRate, ref stepMode,
                                 ref neighborhoodType, ref neighborhoodOrder, ref centreCount,
                                 ref survivalConstraints, ref birthConstraints, ref ghostMode,
                                 ref generationalMemory, ref outputFile, ref inputSurvival,
                                 ref inputBirth, ref surCLIWorks, ref burCLIWorks);
            }

            if (surCLIWorks == false || inputSurvival == "")
            {
                survivalConstraints.Add(survivalFirst);

                survivalConstraints.Add(survivalLast);

                inputSurvival = "2...3";
            }

            if (burCLIWorks == false || inputBirth == "")
            {
                birthConstraints.Add(birthOnly);

                inputBirth = "3";
            }

            // the game's main 2 dimensional array
            // stores all cell alive and dead statuses
            int[,] lifeGen = new int[rows, columns];

            // seed file reading and checking
            FileContents(ref lifeGen, ref inputFile, ref rows,
                         ref columns, ref randomFactor, (int)CellConstants.Alive, ref fileMode, ref success);

            // displaying runtime settings
            DisplayRuntimeSettings(ref inputFile, ref generations, ref maxUpdateRate, ref periodicMode,
                                   ref rows, ref columns, ref randomFactor, ref stepMode, ref success,
                                   ref outputFile, ref generationalMemory, ref neighborhoodType,
                                   ref neighborhoodOrder, ref ghostMode, ref centreCount,
                                   ref argsProvided, ref inputSurvival, ref inputBirth);


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

            // choosing neighborhood depending on specifications
            // of the neighborhood type variable
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

                        // checking cell alive and dead status according to rules of life
                        RulesOfLife(ref lifeGen, ref tempGen, ref rowcheck,
                                    ref columncheck, ref periodicMode, ref survivalConstraints,
                                    ref birthConstraints, neighborsnew, ref neighborhoodOrder, ref centreCount);
                    }
                }

                // operation according to ghost mode
                if (ghostMode == true)
                {
                    GhostModeCells(memoryQueue, tempGen, rows, columns);
                }

                // step mode funtionality
                StepModeSpacebar(ref stepMode);

                // updating memory
                isSteady = AddToQueue(lifeGen, ref generationalMemory, memoryQueue);

                // update generation grid after rules of life
                UpdateGenerationGrid(ref lifeGen, ref tempGen, grid, (int)CellConstants.Dead);

                // rendering updated cells
                grid.Render();

                // if steady state was detected, complete the game and display the periodicity
                if (isSteady == true)
                {
                    grid.IsComplete = true;
                    grid.Render();

                    while (ReadKey().Key != Spacebar)
                    {
                        continue;
                    }

                    grid.RevertWindow();

                    WriteLine("steady state detected... periodicity = " + Periodicty(memoryQueue));

                    while (ReadKey().Key != Spacebar)
                    {
                        continue;
                    }

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

            // depending on steady state detection,
            // the game is completed and the relavant message is displayed
            GameOver(grid, ref isSteady);

            // calling the function to write to the output file
            // if the file path is provided and valid
            WriteToFile(ref lifeGen, ref outputFile);
        }
    }
}