using System.Collections.Generic;
using hNext.Model;
using hNext.IRepository;

public class Repository<T> : IRepository<T>
{
    protected static Dictionary<string, string> ApiKeys = new Dictionary<string, string>
    {
        {nameof(Person), "people"}
    };

}