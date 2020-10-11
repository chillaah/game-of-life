using System;
using Display;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using static System.Console;
using static System.Convert;
using static System.ConsoleKey;

namespace Life
{
    class Program
    {
        /// <summary>
        /// method for checking the validity 
        /// and storing provided command line arguments
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <param name="success"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="periodicMode"></param>
        /// <param name="randomFactor"></param>
        /// <param name="inputFile"></param>
        /// <param name="generations"></param>
        /// <param name="maxUpdateRate"></param>
        /// <param name="stepMode"></param>
        static void PerformingChecks(string[] args, int index, ref bool success, ref int rows,
                                     ref int columns, ref bool periodicMode, ref double randomFactor,
                                     ref string inputFile, ref int generations, ref double maxUpdateRate,
                                     ref bool stepMode)
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
        }

        // method for counting alive neighboring cells of the current cell
        /// <summary>
        /// method to count alive or dead neighbors of a cell
        /// </summary>
        /// <param name="lifeGen"></param>
        /// <param name="rowCheck"></param>
        /// <param name="columnCheck"></param>
        /// <param name="periodicMode"></param>
        /// <returns> number of alive nieghbors if a given cell </returns>
        static int GetNeighbors(ref int[,] lifeGen, ref int rowCheck, ref int columnCheck, 
                                ref bool periodicMode)
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

                    string fileContents = "";

                    List<int> rowsList = new List<int>();

                    List<int> columnsList = new List<int>();

                    int listCount = 0;

                    int rowsMax = 0;

                    int columnsMax = 0;

                    bool tooBig = false;

                    seedFile.ReadLine();

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
                // else display error and execute randomly generated cells
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
                                           ref bool stepMode, ref bool success)
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
            // generations
            WriteLine(String.Format("{0, 15} : {1, -10}", "Generations", generations));

            // update rate
            WriteLine(String.Format("{0, 15} : {1, -10}", "Refresh Rate", maxUpdateRate + " updates/s"));

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
                                ref int columnCheck, ref bool periodicMode, int TwoLiveNeighbors,
                                int ThreeLiveNeighbors)
        {
            // calling from the GetNeighbors method
            // obtaining nieghbors in current cell
            int neighbors = GetNeighbors(ref lifeGen, ref rowCheck, ref columnCheck, ref periodicMode);

            // final check for cell alive or dead
            // if cell alive, will it survive?
            if (lifeGen[rowCheck, columnCheck] == (int)CellConstants.Alive)
            {
                // if cell has exactly 2 or 3 live neighbours, it survives
                if (neighbors == TwoLiveNeighbors || neighbors == ThreeLiveNeighbors)
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
                if (neighbors == ThreeLiveNeighbors)
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

        // enumeration of repeating constants used in Main
        enum CellConstants
        {
            Alive = 1,
            Dead = 0,
            TwoLiveNeighbors = 2,
            ThreeLiveNeighbors = 3
        }

        /// <summary>
        /// main function takes in all methods
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

            // success variable
            bool success = true;

            // performing checks
            for (int index = 0; index < args.Length; ++index)
            {
                // program should run in default values if no arguments are provided
                if (args.Length == 0)
                {
                    WriteLine("No command line arguments provided. Program will run on default mode");
                }
                // perform according to args
                else
                {
                    PerformingChecks(args, index, ref success, ref rows, ref columns, 
                                     ref periodicMode, ref randomFactor, ref inputFile, 
                                     ref generations, ref maxUpdateRate, ref stepMode);
                }
            }

            // the game's main 2 dimensional array 
            // stores all cell alive and dead statuses
            int[,] lifeGen = new int[rows, columns];

            // seed file reading and checking
            FileContents(ref lifeGen, ref inputFile, ref rows,
                         ref columns, ref randomFactor, (int)CellConstants.Alive);

            // displaying runtime settings
            DisplayRuntimeSettings(ref inputFile, ref generations, ref maxUpdateRate, ref periodicMode,
                                   ref rows, ref columns, ref randomFactor, ref stepMode, ref success);

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

            // game's main while loop
            // updating grid alive and dead cells according to rules of Life
            // until generations limit is reached
            while (currentGeneration < generations)
            {
                for (int rowCheck = 0; rowCheck < lifeGen.GetLength(0); ++rowCheck)
                {
                    for (int columnCheck = 0; columnCheck < lifeGen.GetLength(1); ++columnCheck)
                    {
                        // checking cell alive and dead status according rules of life
                        RulesOfLife(ref lifeGen, ref tempGen, ref rowCheck,
                               ref columnCheck, ref periodicMode, (int)CellConstants.TwoLiveNeighbors,
                               (int)CellConstants.ThreeLiveNeighbors);
                    }
                }

                // step mode funtionality
                StepModeSpacebar(ref stepMode);

                // update generation grid after rules of life
                UpdateGenerationGrid(ref lifeGen, ref tempGen, grid, (int)CellConstants.Dead);

                // frame rate according update rate
                FrameRatePerGen(ref maxUpdateRate, watch);

                // adding to the next generation
                ++currentGeneration;

                // displaying the generation number at the bottom right of the grid
                grid.SetFootnote("GENERATION : " + currentGeneration);

                grid.Render();
            }

            // clearing amnd resetting grid
            GameOver(grid);
        }
    }
}