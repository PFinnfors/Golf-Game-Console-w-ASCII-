/* NOTE: I designed the full game without the use of exception handling by building the limitations into the code.
 * Then when I realized I had to use them, I started modifying the code to fit it in and this turned out to be
 * more difficult than I thought, and has made the code messier. This assignment would've been done faster and
 * better if I had read the instructions more carefully and designed it accordingly from the beginning. */

using System;
using System.Threading;

namespace AssignmentGolf
{
    class Game
    {
        static void Main(string[] args)
        {
            //PREPARATIONS
            int[,] strokeValues = new int[8, 2]; //0,0 = Angle; 1,0 = Velocity/stroke distance;
                                                 //2,0 = Start distance; 3,0 = Hole distance
                                                 //Later used to store for-loop value to give a turn count
            int turns = 0;

            //Creates a new class instance to put methods into, to give code room
            Stroke strokeInputClass = new Stroke();

            //Main game loop, broken or preserved at the end with user input
            char exitKey = 'Y';
            //does while exitKey is Y
            do
            {
                //Used later to check if user has given an exitKey response to break out of exit question
                bool exitBool = false;

                //Generates a pseudo-random golf course between 1.2km-2.4km
                Random rand = new Random();
                strokeValues[3, 0] = rand.Next(1200, 2400);

                //GAME START
                Console.WriteLine("     '\\                   .  .      Art by Joan Stark |>18>>\n"
                                + "       \\              .         ' .                   |\n"
                                + "      O>>         .                 'o                |\n"
                                + "       \\       .       WELCOME                        |\n"
                                + "       /\\    .         TO THE                         |\n"
                                + "      / /  .'         GOLF GAME           P.FINNFORS  |\n"
                                + "jgs^^^^^^^`^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n");

                //TURN STARTS: Loops this gameplay and keeps stroke count through i
                for (int i = 1; i < 20; i++)
                {
                    //Since turn exception comes later, this if keeps following code from executing
                    //after the last turn while strokes left are zero
                    if (i < 9)
                    {
                        Console.WriteLine("IT'S YOUR TURN!");

                        //Shows stroke count only after there have been strokes
                        if (i > 1)
                            Console.Write($" You've hit the ball [{i - 1}] time(s). ");
                        Console.Write($"You have a total of [{9 - i}] stroke(s) left.\n");

                        //getStroke called to take care of the gameplay
                        strokeValues = strokeInputClass.getStroke(strokeValues);
                        //processStroke called to take care of the turn results
                        strokeValues = strokeInputClass.processStroke(strokeValues);

                        Thread.Sleep(1000);
                    }

                    //Stores turn count from the ongoing for-loop
                    turns = i;

                    //CHECKS CONDITIONS FOR WIN OR LOSS
                    //The ball hits the hole when hole distance is zero -- wins
                    if (strokeValues[3, 0] == 0)
                    {
                        Console.WriteLine("-----------------------------------------------------------o\n\n"
                            + "[The ball falls into the hole...] YOU WON!\n"
                            + $"\nIt took you [{i}] stroke(s).\n"
                            + " __          _______ _   _ _   _ ______ _____  \n"
                            + " \\ \\        / /_   _| \\ | | \\ | |  ____|  __ \\ \n"
                            + "  \\ \\  /\\  / /  | | |  \\| |  \\| | |__  | |__) |\n"
                            + "   \\ \\/  \\/ /   | | | . ` | . ` |  __| |  _  / \n"
                            + "    \\  /\\  /   _| |_| |\\  | |\\  | |____| | \\ \\ \n"
                            + "     \\/  \\/   |_____|_| \\_|_| \\_|______|_|  \\_\\\n"
                            + "-----------------------------------------------------------o\n\n");
                        i = 20; //Breaks for loop
                    }

                    //RECORDS STROKES AND USES INDEX OUT OF RANGE EXCEPTION TO SEND GAME OVER MESSAGE
                    try
                    {
                        if (i < 10)
                            strokeValues[i - 1, 1] = strokeValues[1, 0];
                    }
                    catch
                    {
                        Console.WriteLine("----------------------------------------------------------------o\n\n"
                        + "OH NO! You used up all your strokes...\n"
                        + "  _____          __  __ ______    ______      ________ _____  \n"
                        + " / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\ \n"
                        + "| |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) |\n"
                        + "| | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  / \n"
                        + "| |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\ \n"
                        + " \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_\\\n"
                        + "----------------------------------------------------------------o\n\n");
                        i = 20; //Breaks for loop
                    }

                    //CAUSES A DIVISION BY ZERO EXCEPTION IF VALUE 2800 IS REACHED (OUTSIDE COURSE)
                    // (& i != 20) ensures exceptions don't follow each other
                    if (strokeValues[3, 0] >= 2800 & i != 20)
                    {
                        try
                        {
                            //Division by a zero value
                            strokeValues[2, 0] = 2800 / strokeValues[6, 0];
                        }
                        catch
                        {
                            Console.WriteLine("-----------------------------------------------------------o\n\n"
                           + "WHOOPS! You shot the ball out of the course. Poor lady on the sidewalk!\nGAME OVER!\n\n"
                           + "-----------------------------------------------------------o\n"
                           + " ______     _    ___   __  _____         _____ \n"
                           + "|  ____/\\  | |  | \\ \\ / / |  __ \\ /\\    / ____|\n"
                           + "| |__ /  \\ | |  | |\\ V /  | |__) /  \\  | (___  \n"
                           + "|  __/ /\\ \\| |  | | > <   |  ___/ /\\ \\  \\___ \\ \n"
                           + "| | / ____ \\ |__| |/ . \\  | |  / ____ \\ ____) |\n"
                           + "|_|/_/    \\_\\____//_/ \\_\\ |_| /_/    \\_\\_____/ \n\n");

                            i = 20;
                        }
                    }
                }
                //THE GAME IS OVER
                //Loop to write out array values *turns* amount of times
                for (int t = 1; t <= turns; t++)
                {
                    try { Console.WriteLine($"Turn {t}: [{strokeValues[t - 1, 1]}m]"); }
                    catch { /* Doesn't attempt to write out overbound value */ }

                }
                //Asks question until exitBool is true
                while (exitBool == false)
                {
                    Console.WriteLine("\n\nDo you want to play again (Y/N):");
                    exitKey = Console.ReadKey().KeyChar;
                    exitKey = char.ToUpper(exitKey);
                    Console.WriteLine("");

                    if (exitKey == 'Y')
                    {
                        Console.WriteLine("\nNice, go practice those swings!");
                        Thread.Sleep(2000);
                        Console.WriteLine("\n * NEW GAME * \n\n");
                        //Question satisfied
                        exitBool = true;
                    }
                    else if (exitKey == 'N')
                    {
                        Console.WriteLine("\nOK. Goodbye!");
                        Thread.Sleep(1000);
                        //Question satisfied
                        exitBool = true;
                    }
                    else
                    {
                        //Given error message then sent back to question via while-loop
                        if (exitKey != 'Y' && exitKey != 'N')
                            Console.WriteLine("----------------------------o\n"
                                + "Answer must be Yes or No!");
                    }
                }

            } while (exitKey == 'Y');
        }
    }
}
