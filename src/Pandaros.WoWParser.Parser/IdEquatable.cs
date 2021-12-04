using System;
using System.Collections.Generic;
using System.Text;

namespace Pandaros.WoWParser.Parser
{
    public interface IdEquatable<T>
    {
        bool EquilIds(T obj);

        bool EquilIds(string id);

        string GetId();
    }
}
