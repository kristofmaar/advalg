using AdvancedAlgorithms_ISGK7K.Problems;
using AdvancedAlgorithms_ISGK7K.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedAlgorithms_ISGK7K.Solvers
{
    /// <summary>
    /// Function Approximation solved with Genetic Algorithm
    /// </summary>
    public class FA_With_GA
    {
        private static Random random = new Random();
        private GASettings settings;
        private FunctionApproximation functionApproximation;
        private Chromosome best;

        public FA_With_GA(GASettings settings)
        {
            this.settings = settings;
            functionApproximation = new FunctionApproximation(settings.InputFilePath);
        }

        public void SolveProblem()
        {
            List<Chromosome> population = InitializePopulation();
            best = GetBestChromosome(population);
            for (int iterations = 0; iterations < settings.MaxIterations; iterations++) // StopCondition
            {
                List<Chromosome> newPopulation = Elitism(population);

                while (newPopulation.Count != population.Count)
                {
                    List<Chromosome> parents = GetParents(population);
                    Chromosome afterCrossover = Crossover(parents);
                    Chromosome afterMutation = Mutation(afterCrossover);
                    newPopulation.Add(afterMutation);
                }
                population = newPopulation;
                best = GetBestChromosome(population);

                Console.WriteLine(String.Format("Fittness: {0}\nvalues: {1}", best.CalculateFitness(functionApproximation).ToString(), best.ToString()));
            }
            Console.WriteLine("Best solution found:\n\tFittness:{0}\n\tChromosome: {1}", best.CalculateFitness(functionApproximation), best.ToString());
        }

        private List<Chromosome> InitializePopulation()
        {
            List<Chromosome> initPopulation = new List<Chromosome>();
            for (int i = 0; i < settings.NumberOfPopulation; i++)
            {
                Chromosome chromosome = new Chromosome();
                for (int j = 0; j < 5; j++)
                {
                    chromosome.Add(random.NextDouble());
                }
                initPopulation.Add(chromosome);
            }
            return initPopulation;
        }

        private Chromosome GetBestChromosome(List<Chromosome> population)
        {
            return population.OrderBy(x => x.CalculateFitness(functionApproximation)).FirstOrDefault();
        }

        private List<Chromosome> Elitism(List<Chromosome> population)
        {
            return population.OrderBy(x => x.CalculateFitness(functionApproximation)).Take(settings.ElitismNumber).ToList();
        }

        private List<Chromosome> GetParents(List<Chromosome> population)
        {
            return population.OrderBy(x => x.CalculateFitness(functionApproximation)).Take(settings.NumberOfParents).ToList();
        }

        private Chromosome Crossover(List<Chromosome> parents)
        {
            Chromosome crossoverChromosome = new Chromosome();
            for (int i = 0; i < parents.Count - 1; i++)
            {
                Chromosome firstParent = parents[i];
                Chromosome secondParent = parents[i + 1];
                
                crossoverChromosome = new Chromosome()
                {
                    secondParent[0],
                    secondParent[1],
                    firstParent[2],
                    firstParent[3],
                    firstParent[4]
                };
            }
            return crossoverChromosome;
        }

        private Chromosome Mutation(Chromosome chromosome)
        {
            return new Chromosome()
            {
                chromosome[0] * GenerateMutationConstraint(),
                chromosome[1] * GenerateMutationConstraint(),
                chromosome[2] * GenerateMutationConstraint(),
                chromosome[3] * GenerateMutationConstraint(),
                chromosome[4] * GenerateMutationConstraint(),
            };
        }
        
        private double GenerateMutationConstraint()
        {
            double minRange = 1.00 - (settings.MutationPercent / (double)100);
            double maxRange = 1.00 + (settings.MutationPercent / (double)100);
            return (minRange + (maxRange - minRange) * random.NextDouble());
        }
    }
}
