using System;
using System.Globalization;
using System.Threading;

namespace AssignmentGolf
{
    public class Stroke
    {
        //METHOD GETS MAIN GAMEPLAY
        public int[,] getStroke(int[,] strokeResult)
        {
            

            //Player is asked to give a stroke angle, code calls getStrokeInput() to receive the value
            Console.WriteLine($"\nYou're swinging [{strokeResult[3,0]}m] from the hole on a random course. "
            + "\n\nWhat angle do you want to strike the ball with?"
                + "\nAngle (1-179):");

                strokeResult[0, 0] = getStrokeInput();
            
            while (strokeResult[0,0] > 179 || strokeResult[0,0] < 1)
            {
                if (strokeResult[0,0] < 0 || strokeResult[0,0] > 180)
                    Console.WriteLine("\n[You spin the club around...] Are you going to strike or what?\nTry again (1-179):");
                else
                    Console.WriteLine("\n[You smash the ball into the grass...] Don't be mad, it's just a game!\nTry again (1-179):");

                strokeResult[0,0] = getStrokeInput();
            }

            //Player is asked to give a stroke velocity, code calls getStrokeInput() again
            Console.WriteLine($"\nOkay, you're swinging at a {strokeResult[0,0]} degree angle. How fast (m/s)?"
                + "\nVelocity (1-94):");
            //Gets input to be calculated into distance
            strokeResult[1,0] = getStrokeInput();
            //Sets velocity constraints and error message
            while (strokeResult[1,0] < 1 || strokeResult[1,0] > 94)
            {
                Console.WriteLine("You can't hit slower than 1 or faster than 94 m/s. This isn't a game! ¯\\_(ツ)_/¯ "
                    + "\nTry again (1-94):");
                strokeResult[1,0] = getStrokeInput();
            }

            return strokeResult;
        }

        //METHOD CALLED DURING GAMEPLAY TO TAKE CARE OF INPUT WITH PARSE CHECK
        public int getStrokeInput()
        {
            string inputString;
            int inputValue = 0;
            bool parseCheck = false, loopCheck = false;

            //Until the parse succeeds, it keeps asking for the input
            while (!parseCheck)
            {
                //Shows error message once there has been a loop and loopCheck is set to true
                if (loopCheck) { Console.WriteLine("Huh, what's that? Try again:"); }

                inputString = Console.ReadLine();
                parseCheck = int.TryParse(inputString, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out inputValue);
                loopCheck = true;
            }
            //Resets checks for next use
            parseCheck = false;
            loopCheck = false;
            return inputValue;
        }

        //METHOD: PROCESSES THE OUTCOME / STROKE VALUES
        public int[,] processStroke(int[,] strokeResults)
        {
            //Calculates new angle in radians
            double angleResult = (Math.PI / 180) * strokeResults[0,0];

            //Calculates the resulting distance
            double distanceResult = System.Convert.ToDouble(strokeResults[1,0]);
            distanceResult = Math.Pow(distanceResult, 2) //Velocity^2
                / 9.8 * Math.Sin(2 * angleResult); //Divided by gravity constant, multiplied by radians arc
            distanceResult = Math.Round(distanceResult, 0, MidpointRounding.ToEven);
            
            //Updates new distance to hole
            strokeResults[3,0] = (strokeResults[3,0] - (Convert.ToInt32(distanceResult)) );
            strokeResults[3,0] = Math.Abs(strokeResults[3,0]);

            //Gives result to player
            Thread.Sleep(350);
            Console.WriteLine("\n*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\n"
                             + $"The ball flew {distanceResult}m...\n"
                             + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\n");
            
            //Converting and sending back to integer array
            strokeResults[1,0] = Convert.ToInt32(distanceResult);
            strokeResults[2,0] = Convert.ToInt32(strokeResults[2,0] + strokeResults[1,0]);

            return strokeResults;
        }


    }
        
}