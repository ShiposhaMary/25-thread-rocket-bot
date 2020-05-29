using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class My_Bot
    {

        public Rocket GetNextMove(Rocket rocket)
        {
            // TODO: распараллелить запуск SearchBestMove
            //var bestMove = SearchBestMove(rocket, new Random(random.Next()), iterationsCount);
            //var newRocket = rocket.Move(bestMove.Item1, level);
            //return newRocket;
            
            var moves = new List<Tuple< Turn, double>> ();
            var tasks = new Task[threadsCount];
                for (int i = 0; i < threadsCount; i++)
                {
                    tasks[i] = new Task(() =>
                    {
                       Random random = new Random();
                       moves.Add (SearchBestMove(rocket,
                            random, iterationsCount / threadsCount));

                    });
                    tasks[i].Start();    
                }
                Task.WaitAll(tasks);
                var bestMove = moves.OrderBy(x =>x.Item2).First();
                return rocket.Move(bestMove.Item1, level);
        }
    }
}

    
    
