using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entities
{
    public class Role
    {
        public int Role_Id { get; set; }
        public String Description { get; set; }
        public int State { get; set; }
        public int Deleted { get; set; }
        public List<SystemModule> oListSystemModule { get; set; }
        public String modulos { get; set; }
        public Role()
        {
            oListSystemModule = new List<SystemModule>();
        }

        public void modulesName()
        {
            modulos="";
            for (int i = 0; i < oListSystemModule.Count; i++)
            {
                Entities.SystemModule oModule = oListSystemModule[i];
               modulos += oModule.Description +" ";
            }
        }
    }
}