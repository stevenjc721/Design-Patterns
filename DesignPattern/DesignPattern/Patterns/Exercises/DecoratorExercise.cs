using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Complete the Dragon's interface(no need to extract IBird/ILizard). Take care when implementing the Age property
 */
namespace DesignPattern.Patterns.Exercises
{
    class DecoratorExercise
    {
        public class Bird
        {
            public int Age { get; set; }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public class Lizard
        {
            public int Age { get; set; }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public class Dragon
        {
            private Bird bird = new Bird();
            private Lizard lizard = new Lizard();
            private int age;


            public string Fly()
            {
                return bird.Fly();
            }

            public string Crawl()
            {
                return lizard.Crawl();
            }

            public int Age
            {
                get { return age; }
                set
                {
                    age = value;
                    bird.Age = value;
                    lizard.Age = value;
                }
            }
        }
    }
}
