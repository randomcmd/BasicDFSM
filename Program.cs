using System.Collections.Generic;
using System;

namespace Finite_Deteministic_State_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = Console.ReadLine();
            //Console.WriteLine("Hello World! " + input);
            //Console.WriteLine(fdsm.ValidateInput("00000010101001"));
            //Console.WriteLine(fdsm.ValidateInput("0000001010100"));
            //Console.WriteLine(fdsm.ValidateInput("0000001010100"));
            for(;;)
            {
                Console.WriteLine("Pick an example!");
                Console.Write("Enter an example number: ");
                string input = Console.ReadLine();
                if (input.Equals("0")) test0();
                if (input.Equals("1")) test1();
                if (input.Equals("2")) test2();
                if (input.Equals("exit")) break;
            }
        }

        public static void test0()
        {
            //Example: Creating a state machine that checks if the last digit of a binary number is 1
            FDSM fdsm = new FDSM(/*"01"*/);
            fdsm.SetStart("q0", StateType.DEFAULT);
            fdsm.NewState("q1", StateType.ACCEPTED);

            //Adding transitions between states
            fdsm.AddTransition("q0","q0",'0');
            fdsm.AddTransition("q0","q1",'1');
            fdsm.AddTransition("q1","q0",'0');
            fdsm.AddTransition("q1","q1",'1');

            //Test cases
            Console.WriteLine("Test case (should be true) : " + fdsm.ValidateInput("00000010101001"));
            Console.WriteLine("Test case (should be false) : " + fdsm.ValidateInput("0000001010100"));

            //Starting test 
            Console.WriteLine("This FDSM checks if a binary number ends with a 1");
            for(;;)
            {
                Console.Write("Enter a binary number: ");
                string input = Console.ReadLine();
                if(input.Equals("exit")) break;
                Console.WriteLine("Binary number ends with 1 : " + fdsm.ValidateInput(input));
            }
        }

        public static void test1()
        {
            //Example: Creating a state machine that checks if the last digit of a binary number is 1
            FDSM fdsm = new FDSM(/*"01"*/);
            fdsm.SetStart("q0", StateType.ACCEPTED);
            fdsm.NewState("q1", StateType.ACCEPTED);
            fdsm.NewState("q2", StateType.ACCEPTED);
            fdsm.NewState("q3", StateType.ACCEPTED);
            fdsm.NewState("q4", StateType.ACCEPTED);
            fdsm.NewState("qF", StateType.DEFAULT);

            //Adding transitions between states
            fdsm.AddTransition("q0","q1",'c');
            fdsm.AddTransition("q1","q2",'c');
            fdsm.AddTransition("q2","q3",'c');
            fdsm.AddTransition("q3","q4",'c');
            
            fdsm.AddTransition("q0","q2",'e');
            fdsm.AddTransition("q1","q3",'e');
            fdsm.AddTransition("q2","q4",'e');


            fdsm.AddTransition("q4","qf",'c');
            fdsm.AddTransition("q3","qf",'e');
            fdsm.AddTransition("q4","qf",'e');

            //Test cases
            Console.WriteLine("Test case (should be true) : " + fdsm.ValidateInput("cc"));
            Console.WriteLine("Test case (should be false) : " + fdsm.ValidateInput("ccccccc"));

            //Starting test 
            Console.WriteLine("Coffe machine that can make coffe (c) and espresso (e)");
            for(;;)
            {
                Console.Write("Enter a series of coffe products: ");
                string input = Console.ReadLine();
                if(input.Equals("exit")) break;
                Console.WriteLine("Coffe machine still clean : " + fdsm.ValidateInput(input));
            }
        }

        public static void test2()
        {
            //Example: Creating a state machine that checks if a word contains oop

            //We use a string with all letters to loop through the "everything but this letter" transitions
            string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            //Example: I can disclude a character with abc.Replace('a',String.Empty)


            FDSM fdsm = new FDSM();
            fdsm.SetStart("q0", StateType.DEFAULT);
            fdsm.NewState("o0", StateType.DEFAULT);
            fdsm.NewState("o1", StateType.DEFAULT);
            fdsm.NewState("p", StateType.ACCEPTED);

            //OOP transitions
            fdsm.AddTransition("q0","o0",'o');
            fdsm.AddTransition("o0","o1",'o');
            fdsm.AddTransition("o1","p",'p');
            //Also for the capital letters
            fdsm.AddTransition("q0","o0",'O');
            fdsm.AddTransition("o0","o1",'O');
            fdsm.AddTransition("o1","p",'P');

            //Now the anything but the letter something transitions
            //We save a lot of work here by using foreach to cycle through the abc string insdead of adding each manually

            //q0 to q0 with any letter but o
            string abcModified = abc.Replace("o",String.Empty);
            abcModified = abcModified.Replace("O",String.Empty);

            //Console.WriteLine(abcModified);
            //test0();
            foreach(char c in abcModified)
            {
                fdsm.AddTransition("q0","q0",c);
            }

            //o0 to q0 with anything but o 
            abcModified = abc.Replace("o",String.Empty);
            abcModified = abcModified.Replace("O",String.Empty);

            foreach(char c in abcModified)
            {
                fdsm.AddTransition("o0","q0",c);
            }

            //o1 to q0 with anything but p , but o is a special case cause double o
            abcModified = abc.Replace("p",String.Empty);
            abcModified = abcModified.Replace("P",String.Empty);
            abcModified = abcModified.Replace("O",String.Empty);
            abcModified = abcModified.Replace("o",String.Empty);

            foreach(char c in abcModified)
            {
                fdsm.AddTransition("o1","q0",c);
            }

            fdsm.AddTransition("o1","o1", 'o');
            fdsm.AddTransition("o1","o1", 'O');

            //p to p with anything
            foreach(char c in abc)
            {
                fdsm.AddTransition("p","p",c);
            }

            //Starting test 
            Console.WriteLine("This FDSM is meant to replicate a spam filter which filters the word 'oop': ");
            for(;;)
            {
                Console.Write("Enter a keyword: ");
                string input = Console.ReadLine();
                if(input.Equals("exit")) break;
                Console.WriteLine("The word contains oop and is filtered : " + fdsm.ValidateInput(input));
            }
        }
    }
}
