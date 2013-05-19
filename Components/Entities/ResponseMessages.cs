using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InspectorIT.QSContent.Components.Entities
{
    public class GetAdminPayload
    {
        public List<Modules> Modules { get; set; }
        public List<CheckInfo>  CheckValues { get; set; }
    }

    public class Modules
    {
        public string Name { get; set; }
        public int ModuleId { get; set; }
    }
}