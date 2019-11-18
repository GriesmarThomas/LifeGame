using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{

    public class Zoo
    {
        public Animal GlobalAnimal { get; set; }

        public Zoo()
        {
            GlobalAnimal = new Animal();
            GlobalAnimal.MySpecies = new List<Species>();
        }

        public void Init()
        {
            Queue<Animal> myQueue = new Queue<Animal>();

            //Initiate Global Entity
            GlobalAnimal.MySpecies.Add(new Species() { FirstVar = 12, SecondVar = 32 });
            //Add it right in the queue
            myQueue.Enqueue(GlobalAnimal);

            //Edit once
            Animal otherAnimal = new Animal();
            otherAnimal = GlobalAnimal.DeepCopy();
            var species = otherAnimal.MySpecies.First(x => x.FirstVar == 12);
            species.SecondVar = 33;
            //Add it to the queue
            myQueue.Enqueue(otherAnimal);

            //Edit twice
            Animal wholeNewAnimal = new Animal();
            var otherListOfSpecies2 = new List<Species>(GlobalAnimal.MySpecies.ConvertAll(x => x.DeepCopy()));
            var twice = otherListOfSpecies2.First(x => x.FirstVar == 12);
            twice.SecondVar = 65;
            wholeNewAnimal.MySpecies = otherListOfSpecies2;
            GlobalAnimal.MySpecies = wholeNewAnimal.MySpecies;
            //Add it to the queue
            myQueue.Enqueue(wholeNewAnimal);






            foreach (Animal animal in myQueue)
            {
                Console.WriteLine("Animal");
                foreach (Species currentSpecies in animal.MySpecies)
                {
                    Console.WriteLine(currentSpecies.FirstVar + ", " + currentSpecies.SecondVar);
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }


    public class Animal
    {
        public List<Species> MySpecies { get; set; }

        public Animal()
        {
            MySpecies = new List<Species>();
        }

        public Animal DeepCopy()
        {
            Animal animal = (Animal)this.MemberwiseClone();
            animal.MySpecies = this.MySpecies.ConvertAll(x => x.DeepCopy());
            return animal;
        }
    }

    public class Species
    {
        public int FirstVar { get; set; }
        public int SecondVar { get; set; }


        public Species(Species s)
        {
            this.FirstVar = s.FirstVar;
            this.SecondVar = s.SecondVar;
        }

        public Species()
        {

        }
        
        public Species DeepCopy()
        {
            Species species = (Species)this.MemberwiseClone();
            species.FirstVar = this.FirstVar;
            species.SecondVar = this.SecondVar;
            return species;
        }
    }
}
