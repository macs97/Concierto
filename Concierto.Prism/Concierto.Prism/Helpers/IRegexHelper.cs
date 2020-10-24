using System;
using System.Collections.Generic;
using System.Text;

namespace Concierto.Prism.Helpers
{
    public interface IRegexHelper
    {
        bool IsValidEmail(string emailaddress);
    }

}
