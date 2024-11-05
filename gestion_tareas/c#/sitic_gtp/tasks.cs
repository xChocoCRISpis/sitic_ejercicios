using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sitic_gtp
{
    class Tb_tasks
    {
        private static int lastId = 0;

        private int _id;
        private string _name;
        private Tb_priorities _priority;
        private Tb_states _state;

        public Tb_tasks(){}

        public Tb_tasks(string name, Tb_priorities priority, Tb_states state){
            this.id = ++lastId;
            this.name = name;
            this.priority = priority;
            this.state = state;
        }
        


        public int id { get => _id; private set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public Tb_priorities priority { get => _priority; set => _priority = value; }
        public Tb_states state { get => _state; set => _state = value; }
    }


    class Tasks {
        private List<Tb_tasks> _tasks;

        public Tasks() {
            Priorities priorities = new Priorities();
            States states = new States();

            _tasks = new List<Tb_tasks>
            {
                new Tb_tasks("Análisis de requerimientos",priorities.priorities[0],states.states[1]),
                new Tb_tasks("Arquitectura",priorities.priorities[1],states.states[0]),
                new Tb_tasks("Diseño",priorities.priorities[1],states.states[1]),
                new Tb_tasks("Códificación",priorities.priorities[0],states.states[2]),
                new Tb_tasks("Tests",priorities.priorities[2],states.states[2])
            };
        }

        public List<Tb_tasks> tasks{ get => _tasks; set => _tasks = value; }
    }
}
