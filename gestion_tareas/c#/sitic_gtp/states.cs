﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sitic_gtp
{
    public enum EStateCode { TD,IP,C }


    class Tb_states
    {
        private static int lastId = 1;
        
        private int _id;
        private EStateCode _code;
        private string _status;

        public Tb_states() { }

        public Tb_states(EStateCode code, string status) {
            this.id = lastId++;
            this.code = code;
            this.status = status;
        }


        public int id { get => _id; private set => _id = value; }
        public EStateCode code { get => _code; set => _code = value; }
        public string status { get => _status; set => _status = value; }

    }

    class States {
        private List<Tb_states> _states;

        public States()
        {
            _states = new List<Tb_states>
            {
                new Tb_states(EStateCode.TD, "TO DO"),
                new Tb_states(EStateCode.IP, "IN PROGRESS"),
                new Tb_states(EStateCode.C, "COMPLETED")
            };
        }

        public List<Tb_states> states { get => _states; set => _states = value; }
    }
}
