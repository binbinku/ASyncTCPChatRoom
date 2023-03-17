using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Scripts.Entity
{
    public class User
    {
        private static User instance; 

        public static User Instance
        {
            get
            {
                return instance;
            }
        }

        public string Name
        {
            get;
            set;
        }

        public User()
        {
            if(instance==null)
            {
                instance = this;
            }
        }

    }
}