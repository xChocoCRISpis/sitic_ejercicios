using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace sitic_gtp
{
    #region class_error
    public class CustomExceptions : Exception
    {
        private EErrors ErrorResponse { get; set; } = EErrors.Unknown;


        //La clase base, sirve para usar el metodo del padre.
        //En este ejemplo el constructor de la clase "Exception"
        public CustomExceptions() : base() { }

        public CustomExceptions(string message, EErrors error) : base(message)
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
        Unknown = 500
    }
    #endregion
    #region main
    class Program
    {
        #region dictionary-headers
        private static readonly Dictionary<string, string> headers = new Dictionary<string, string> {
                {"OrderByPriority","///////////////////////    ORDENADO POR PRIORIDAD    /////////////////////////////" },
                {"OrderByState", "///////////////////////    ORDENADO POR ESTADO    /////////////////////////////" },
                {"OrderByPriorityAndState", "///////////////////////    ORDENADO POR PRIORIDAD Y ESTADO   /////////////////////////////"},
                {"FilterByToDo" ,"///////////////////////    FILTRADO POR TO DO  /////////////////////////////"},
                {"FilterByHP" ,"///////////////////////    FILTRADO POR HP  /////////////////////////////"},
                {"FilterByIPAndHP" ,"///////////////////////    FILTRADO POR HP Y IP  /////////////////////////////"},
                {"GroupByState", "///////////////////////    AGRUPADO POR ESTADO   /////////////////////////////" },
                {"GroupByPriority", "///////////////////////   AGRUPADO POR PRIORIDAD   /////////////////////////////" },
                {"Default"," ////////////////////////////////////////////////////" }
            };
        #endregion 
        static void Main(string[] args)
        {
            Tasks tasks = new Tasks();
            
            Console.WriteLine("Start");
            try
            {


                AddTask(null, new Tb_tasks("Nueva tarea", new Priorities().priorities[1], new States().states[2]));

                OrderByPriority(tasks.tasks, GetDictionaryValue("OrderByPriority"));
                OrderByState(tasks.tasks, GetDictionaryValue("OrderByState"));
                OrderByPriorityAndState(tasks.tasks, GetDictionaryValue("OrderByPriorityAndState"));
                GroupByState(tasks.tasks, GetDictionaryValue("GroupByState"));
                GroupByPriority(tasks.tasks, GetDictionaryValue("GroupByPriority"));
                FilterByToDo(tasks.tasks, GetDictionaryValue("FilterByToDo"));
                FilterByHP(tasks.tasks, GetDictionaryValue("FilterByHP"));
                FilterByIPAndHP(tasks.tasks, GetDictionaryValue("FilterByIPAndHP"));
            }
            catch (CustomExceptions ex) {
                Console.WriteLine($"Ocurrio un error: {ex.ToString()}");

                OrderByPriority(tasks.tasks);
                OrderByState(tasks.tasks);
                OrderByPriorityAndState(tasks.tasks);
                GroupByState(tasks.tasks);
                GroupByPriority(tasks.tasks);
                FilterByToDo(tasks.tasks);
                FilterByHP(tasks.tasks);
                FilterByIPAndHP(tasks.tasks);
            } catch (Exception ex) {
                Console.WriteLine("Error desconocido: ");
                Console.WriteLine(ex.ToString());
            }


            Console.WriteLine("End");

        }

        // TIPOS DE CALLBACK
        // Func<T, TReturn> Recibe y retorna
        // Action<T> Recibe pero puede ser void

        private static string GetDictionaryValue(string key) {

            if(headers.TryGetValue(key, out string value))
            {
                return value;
            }

            GetDictionaryValue("Default");
            throw new CustomExceptions("Header no existe", EErrors.Validation);
        }

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

        //No es necesario retornar, debido a que el objeto se modifica por referencia;
        private static void AddTask(Tasks tasks,Tb_tasks task) {
            if (tasks == null)
                throw new CustomExceptions("La lista de Tasks es null", EErrors.Null);

            if (task == null)
                throw new CustomExceptions("La tarea es null", EErrors.Null);


            tasks.tasks.Add(task);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Tarea insertada con éxito");
            Console.ResetColor();
            PrintTasks(tasks.tasks, "insertado");
        }
    }
    #endregion
}
