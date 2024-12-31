using System.Collections;
using System.Reflection;

namespace Hephaestus.Utilities;

internal class TypeFinder<T> : IEnumerable<Type>
{
    private IEnumerable<Type> types = typeof(T).Assembly.GetTypes();
    public TypeFinder<T> IsAbstract() {
        types = types.Where(t => t.IsAbstract);
        return this;
    }

    public TypeFinder<T> IsNotAbstract() {
        types = types.Where(t => !t.IsAbstract);
        return this;
    }

    public TypeFinder<T> HasAttribute<TAttribute>() where TAttribute : Attribute {
        types = types.Where(e => e.GetCustomAttribute<TAttribute>() is not null);
        return this;
    }

    public TypeFinder<T> Inherits<TInherits>() {
        types = types.Where(e => e.IsAssignableTo(typeof(TInherits)));
        return this;
    }

    public IEnumerable<Type> Resolve() => types;

    public IEnumerator<Type> GetEnumerator() => types.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)types).GetEnumerator();
}