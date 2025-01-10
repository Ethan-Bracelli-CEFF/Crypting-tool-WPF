using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WpfApp1;

public static class CryptingFactory
{
    public static List<Crypting> GetAllCryptingMethods()
    {
        var cryptingTypes = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Crypting)));

        return cryptingTypes.Select(t => (Crypting)Activator.CreateInstance(t)).ToList();
    }
}
