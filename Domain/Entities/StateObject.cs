using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public class StateObject
    {
        public State State { get; set; }

    }

    public enum State
    {
        Added, Unchanged, Modified, Deleted
    }
}
