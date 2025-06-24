using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OABaseGameSystem
{
    public class State
    {
        public virtual void Enter() { }

        public virtual void Exit() { }
    }
}