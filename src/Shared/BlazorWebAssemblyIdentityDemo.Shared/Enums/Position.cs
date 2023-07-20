using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.Enums
{
    public enum Position
    {
        [Description("Chief Executive Officer")]
        CEO = 1,

        [Description("Director")]
        Director = 2,

        [Description("General Manager")]
        GM = 3,

        [Description("Manager")]
        M = 4,

        [Description("Technical Lead")]
        TL = 5,

        [Description("Senior Software Engineer")]
        SSE = 6,

        [Description("Software Engineer")]
        SE = 7,

        [Description("Associate Software Engineer")]
        ASE = 8
    }
}
