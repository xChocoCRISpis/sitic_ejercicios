using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace sitic_gtp
{
    #region enums
    public enum EPriorityCode{HP,MP,LP}
    #endregion

    #region priorities class
    class Tb_priorities
    {
        private int _id;
        private string _priority;
        private EPriorityCode _code;

        public Tb_priorities() { }

        public Tb_priorities(int id,string priority, EPriorityCode code) { 
            this.id= id;
            this.priority= priority;
            this.code= code;
        }

        public Tb_priorities(string priority, EPriorityCode code)
        {
            this.priority = priority;
            this.code = code;
        }

        public int id { get => _id; set => _id = value; }
        public string priority { get => _priority; set => _priority = value; }
        public EPriorityCode code { get => _code; set => _code = value; }
    }
    #endregion

    class Priorities {
        List<Tb_priorities> _priorities;

        public Priorities() {
            _priorities = new List<Tb_priorities>() {
                new Tb_priorities(1,"HIGH PRIORITY",EPriorityCode.HP),
                new Tb_priorities(2,"MEDIUM PRIORITY",EPriorityCode.MP),
                new Tb_priorities(3,"LOW PRIORITY",EPriorityCode.LP)
            };
        }

        public List<Tb_priorities> priorities { get => _priorities;  set => _priorities = value; }
    }
}
