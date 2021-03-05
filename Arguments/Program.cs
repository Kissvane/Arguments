using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Arguments
{
    class Program
    {
        public struct Circle
        {
            public int Radius { get; set; }
            public bool Modifiable { get; set; }
            public string Name { get; set; }

            public Circle(int radius, bool modifiable = true, string name = "Circle")
            {
                Radius = radius;
                Modifiable = modifiable;
                Name = name;
            }

            public void Test()
            {
                Console.WriteLine("Name");
            }
        }

        class CircleWrapper
        {
            public Circle circle;

            public CircleWrapper()
            {
                circle = new Circle(5);
            }

            public CircleWrapper Clone()
            {
                return (CircleWrapper)MemberwiseClone();
            }
        }

        static void Main(string[] args)
        {
            #region slide REF

            Circle circle = new Circle(5,name:"C0");
            ModifyCircle(ref circle, 10);
            Console.WriteLine(circle.Radius);

            #endregion

            #region slide OUT
            CircleWrapper wrapper = new CircleWrapper();
            CircleWrapper copy = CopyAndModifyCircleWrapper(wrapper, 50, wrapper.circle.Name+"_Copy");
            
            if (copy != null)
            {
                Console.WriteLine("Le radius de " + wrapper.circle.Name + " est de " + wrapper.circle.Radius);
                Console.WriteLine("Le radius de " + copy.circle.Name + " est de " + copy.circle.Radius);
            }

            wrapper.circle.Modifiable = false;
            CircleWrapper copy2 = CopyAndModifyCircleWrapper(wrapper, 30, wrapper.circle.Name + "_Copy2");

            if (copy2 != null)
            {
                Console.WriteLine("Le radius de copy2 est de " + copy.circle.Radius);
            }
            else
            {
                Console.WriteLine("La copy2 est nulle");
            }

            //circle.Modifiable = false;
            bool copyDone = CopyAndModifyCircle(circle, 100, "MODIFIED", out Circle copiedCircle);
            if (copyDone)
            {
                Console.WriteLine("La copie a fonctionné");
                Console.WriteLine("Le radius de " + copiedCircle.Name + " est de " + copiedCircle.Radius);
            }
            else
            {
                Console.WriteLine("La copie a échoué");
            }

            #endregion 
        }

        /*static async Task Main(string[] args)
        {
            //List of tasks representing lifetime of characters
            List<Task> lives = new List<Task>();
            //character list initialisation
            List<Character> characters = new List<Character>();
            //name list initialisation
            List<string> names = new List<string> { "Diego", "Adrien", "Simon", "Pierre", "Paul", "Jacques", "Michel", "Uriel", "Achille", "Tom", "Shérine", "Athena", "Jeanne", "Laura" };
            //characters creation
            foreach (string name in names)
            {
                characters.Add(new Character(name));
            }

            //government creation
            Government government = new Government(characters);

            foreach (Character character in characters)
            {
                //Life start and setting life in life list
                lives.Add(character.StartLife());

                //choosing enemies and friends
                character.ChooseEnemiesAndFriends(characters);

                //governement subscribe on character death event to produce death certificate
                character.IsDead += government.DeathCertificate;
            }

            //organisation of the first election
            government.Election();

            //wait for all lives to end
            await Task.WhenAll(lives);

            Console.WriteLine("Everybody is dead");
        }*/

        static void smallTest()
        {
            Circle circle = new Circle();
            circle.Radius = 6;
            CircleWrapper wrapper = new CircleWrapper { circle = circle }; // Copie par valeur de circle 
            wrapper.circle.Radius = 66; // Pas de copie, la copie de circle est modifiée directement 
            Circle[] circles = new Circle[] { circle }; // Copie par valeur 
            circles[0].Radius = 7; // Pas de copie, la copie dans le tableau est modifiée directement 
            Circle newCircle = circles[0]; // ATTENTION: une copie est effectuée.
            newCircle.Radius = 8;
            Console.WriteLine(circle.Radius + " " + circles[0].Radius + " " + newCircle.Radius);
        }

        #region REF functions

        static void ModifyCircle(ref Circle toModify, int newRadius)
        {
            toModify.Radius = newRadius;
        }

        #endregion

        #region OUT functions

        #region Q1

        static CircleWrapper CopyAndModifyCircleWrapper(CircleWrapper wrapper, int newRadius, string newName)
        {
            CircleWrapper result = null;
            
            if (wrapper.circle.Modifiable)
            {
                result = wrapper.Clone();
                result.circle.Radius = newRadius;
                result.circle.Name = newName;
            }
            return result;
        }

        #endregion

        #region Q2

        static bool CopyAndModifyCircle(Circle circle, int newRadius, string newName, out Circle copy)
        {
            copy = circle;
            copy.Radius = newRadius;
            copy.Name = newName;

            return circle.Modifiable;
        }
        
        #endregion

        #endregion
    }


}
