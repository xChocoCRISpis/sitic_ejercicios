using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Collections.Immutable;

namespace sitic_gtp
{
    #region class_error
    public class CustomException : Exception
    {
        private EErrors ErrorResponse { get; set; } = EErrors.Unkwnown;

        public CustomException() : base() { }

        public CustomException(string message, EErrors error) : base(message)
        {
            ErrorResponse = error;
        }
    }
    #endregion
    #region enum_error
    public enum EErrors { 
        Empty,
        Validation,
        Null,
        Unkwnown = 500
    }
    #endregion
    class Program
    {
        private static readonly Dictionary<string, string> headers = new Dictionary<string, string> {
                {"OrderByPriority","///////////////////////    ORDENADO POR PRIORIDAD    /////////////////////////////" },
                {"OrderByState", "///////////////////////    ORDENADO POR ESTADO    /////////////////////////////" },
                {"OrderByPriorityAndState", "///////////////////////    ORDENADO POR PRIORIDAD Y ESTADO   /////////////////////////////"},
                {"FilterByToDo" ,"///////////////////////    FILTRADO POR TO DO  /////////////////////////////"},
                {"FilterByHP" ,"///////////////////////    FILTRADO POR HP  /////////////////////////////"},
                {"FilterByIPAndHP" ,"///////////////////////    FILTRADO POR HP Y IP  /////////////////////////////"},
                {"GroupByState", "///////////////////////    AGRUPADO POR ESATDO   /////////////////////////////" },
                {"GroupByPriority", "///////////////////////   AGRUPADO POR PRIORIDAD   /////////////////////////////" },
                {"Default"," ////////////////////////////////////////////////////" }
            };

        static void Main(string[] args)
        {
            Tasks tasks = new Tasks();
            
            Console.WriteLine("Start");
            OrderByPriority(tasks.tasks, headers["OrderByPriority"]);
            OrderByState(tasks.tasks, headers["OrderByState"]);
            OrderByPriorityAndState(tasks.tasks, headers["OrderByPriorityAndState"]);
            GroupByState(tasks.tasks, headers["GroupByState"]);
            GroupByPriority(tasks.tasks);
            FilterByToDo(tasks.tasks, headers["FilterByToDo"]);
            FilterByHP(tasks.tasks,headers["FilterByHP"]);
            FilterByIPAndHP(tasks.tasks, headers["FilterByIPAndHP"]);
            Console.WriteLine("End");

        }

        // TIPOS DE CALLBACK
        // Func<T, TReturn> Recibe y retorna
        // Action<T> Recibe pero puede ser void

        private static void PrintTasks(
            List<Tb_tasks> tasks,
            string header = "/////////////////////////////////////////////////////////////////////////////////"
            ) 
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(header);
            Console.ResetColor();

            if (tasks != null)
                foreach (var task in tasks)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"id: {task.id}, nombre:{task.name}, ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"priority:{task.priority.id}, {task.priority.priority}, {task.priority.code}, ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"state: {task.state.id}, {task.state.status}, {task.state.code}\n");


                    Console.ResetColor();
                }

                Console.WriteLine(headers["Default"]);

        }




        private static List<Tb_tasks> OrderByPriority(List<Tb_tasks> tasks,  string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            List<Tb_tasks> task = tasks.OrderBy(t => t.priority.code).ToList();
            PrintTasks(task,header);
            return task; 
        }

        private static List<Tb_tasks> OrderByState (List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            List<Tb_tasks> task = tasks.OrderBy(t => t.state.code).ToList();
            PrintTasks(task,header);
            return task;
        }

        private static List<Tb_tasks> OrderByPriorityAndState(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            List<Tb_tasks> task = tasks
                .OrderBy(t => t.priority.code)
                .OrderBy(task => task.state.code).ToList();
            PrintTasks(task,header);
            return task;
        }


        private static List<IGrouping<EStateCode, Tb_tasks>> GroupByState(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(header);
            List<IGrouping<EStateCode, Tb_tasks>> groupByState= tasks.GroupBy(t => t.state.code).ToList();

            Action <List<IGrouping< EStateCode, Tb_tasks >>> callback =
                groupByState => {
                    foreach (var group in groupByState)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"Grupo: {group.Key}");
                        PrintTasks(group.ToList(),"");
                    }
                };

            callback(groupByState);

            //PrintTasks(null,callback);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////");
            Console.ResetColor();
            return groupByState;
        }

        private static List<IGrouping<EPriorityCode, Tb_tasks>> GroupByPriority(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(header);
            List<IGrouping<EPriorityCode, Tb_tasks>> groupByPriority = tasks.GroupBy(t => t.priority.code).ToList();

            Action<List<IGrouping<EPriorityCode, Tb_tasks>>> callback =
                groupBy => {
                    foreach (var group in groupBy)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"Grupo: {group.Key}");
                        PrintTasks(group.ToList(),"");
                    }
                };

            callback(groupByPriority);

            //PrintTasks(null,callback);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("/////////////////////////////////////////////////////////////////////////////////");
            Console.ResetColor();
            return groupByPriority;
        }

        private static List<Tb_tasks> FilterByToDo(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////") {
            var filter = tasks.Where(task => task.state.code == EStateCode.TD).ToList();
            PrintTasks(filter,header);

            return filter;
        }

        private static List<Tb_tasks> FilterByHP(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            var filter = tasks.Where(task => task.priority.code == EPriorityCode.HP).ToList();
            PrintTasks(filter, header);

            return filter;
        }

        private static List<Tb_tasks> FilterByIPAndHP(List<Tb_tasks> tasks, string header = "/////////////////////////////////////////////////////////////////////////////////")
        {
            var filter = tasks
                .Where(task => (task.state.code == EStateCode.IP) && (task.priority.code == EPriorityCode.HP))
                .ToList();
            PrintTasks(filter,header);

            return filter;
        }
    }
}
