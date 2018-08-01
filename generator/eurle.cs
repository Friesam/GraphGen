using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generator
{
    class eurle
    {
        int counter = 0;
        double probability = 0.3;

        int Vertex = 0;
        for (i = 1, i >= Vertex; i++){
            for (j= 1, j>= Vertex; j++)
                for (k = 1, k>=Vertex; k++)
            }

        public bool Violation(
            Vertex v1,
            Vertex v2,
            ref int number)
        {
            bool cv = false;

            if (v1.Edge != null)
            {
                bool found = false;

                for (int i = 0; !found && i < v1.Edge.Count; i++)
                    found = v1.Edge[i].Id == v2.Id;

                if (found)
                    cv = v1.Color == v2.Color;
            }

            if (v2.Edge != null)
            {
                bool found = false;

                for (int i = 0; !found && i < v2.Edge.Count; i++)
                    found = v2.Edge[i].Id == v1.Id;

                if (found)
                    cv = v1.Color == v2.Color;
            }

            if (cv)
                number++;

            return cv;
        }

        public int Fitness(List<Vertex> G)
        {
            int fitness = 0;

            for (int i = 0; i < G.Count - 1; i++)
                for (int j = i + 1; j < G.Count; j++)
                    Violation(G[i], G[j], ref fitness);

            return fitness;
        }

        public bool EHC(
            double probability,
            int generations,
            int numberColors,
            int population,
            List<Vertex> G,
            ref int restart,
            ref List<Vertex> solution,
            ref Random random)
        {
            const int maxRestart = 4;
            int n = G.Count;
            int[] fitness = new int[population];
            List<Vertex>[] chromosome = new List<Vertex>[population];

            for (int i = 0; i < population; i++)
            {
                chromosome[i] = new List<Vertex>();

                for (int j = 0; j < G.Count; j++)
                {
                    Vertex v = G[j];

                    v.Color = random.Next(numberColors);
                    chromosome[i].Add(v);
                }

                fitness[i] = Fitness(chromosome[i]);

                if (fitness[i] == 0)
                {
                    solution = chromosome[i];
                    return true;
                }
            }

            int g = 0;

            while (g < generations)
            {
                int parent0 = random.Next(population);
                int parent1 = random.Next(population);

                while (parent0 == parent1)
                {
                    parent0 = random.Next(population);
                    parent1 = random.Next(population);
                }

                int index = fitness[parent0] < fitness[parent1]
                    ? parent0 : parent1;

                List<Vertex> child = chromosome[index];

                for (int i = 0; i < child.Count; i++)
                    if (random.NextDouble() < probability)
                        child[i].Color = random.Next(numberColors);

                int childFitness = Fitness(child);

                if (childFitness == 0)
                {
                    solution = child;
                    return true;
                }

                int maxFitness = fitness[0];

                for (int i = 1; i < population; i++)
                    if (fitness[i] > maxFitness)
                        maxFitness = fitness[i];

                List<int> maxIndex = new List<int>();

                for (int i = 0; i < population; i++)
                    if (fitness[i] == maxFitness)
                        maxIndex.Add(i);

                index = random.Next(maxIndex.Count);

                chromosome[index] = child;
                fitness[index] = childFitness;
                g++;

                bool same = true;
                int min = fitness[0];

                for (int i = 1; same && i < population; i++)
                    same = min == fitness[i];

                if (same)
                {
                    restart++;

                    if (restart < maxRestart)
                        return EHC(
                            probability,
                            generations,
                            numberColors,
                            population + 2,
                            G,
                            ref restart,
                            ref solution,
                            ref random);
                }
            }

            restart++;

            if (restart < maxRestart)
                return EHC(
                    probability,
                    generations,
                    numberColors,
                    population + 2,
                    G,
                    ref restart,
                    ref solution,
                    ref random);

            return false;
        }
    }
}
