using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Patterns.Decorator
{
    class MultipleInheritance
    {
        public class Bird
        {
            public int Weight { get; set; }

            public void Fly()
            {
                Console.Out.WriteLine($"Soaring in the sky with weight {Weight}");
            }
        }

        public class Lizard
        {
            public int Weight { get; set; }

            public void Crawl()
            {
                Console.Out.WriteLine($"Crawling in the dirt with weight {Weight}");
            }
        }
        //C# doesn't allow for multiple inheritance so how to implement both? 
        public class Dragon
        {
            private Bird bird; //DI would make constructor to initialize all the fields
            private Lizard lizard;
            private int weight;

            public Dragon(Bird bird, Lizard lizard)
            {
                this.bird = bird ?? throw new ArgumentNullException(paramName: nameof(bird));
                this.lizard = lizard ?? throw new ArgumentNullException(paramName: nameof(lizard));
            }

            public void Crawl()
            {
                lizard.Crawl();
            }

            public void Fly()
            {
                bird.Fly();
            }

            public int Weight {
                get { return weight; }
                set
                {
                    weight = value;
                    bird.Weight = value;
                    lizard.Weight = value;
                }
            }
        }
    }
}
