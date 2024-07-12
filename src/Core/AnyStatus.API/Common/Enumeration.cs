using System;
using System.Linq;
using System.Reflection;

namespace AnyStatus.API.Common;

public abstract class Enumeration<TEnumeration, TValue> : NotifyPropertyChanged, IComparable<TEnumeration>, IEquatable<TEnumeration>
    where TEnumeration : Enumeration<TEnumeration, TValue>
    where TValue : IComparable
{
    private static readonly Lazy<TEnumeration[]> Enumerations = new(GetEnumerations);

    protected Enumeration(TValue value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public TValue Value { get; }

    public int CompareTo(TEnumeration other) => Value.CompareTo(other == default(TEnumeration)
                                                                    ? default
                                                                    : other.Value);

    public bool Equals(TEnumeration other) => other != null && ValueEquals(other.Value);

    public sealed override string ToString() => Value.ToString();

    public static TEnumeration[] GetAll() => Enumerations.Value;

    private static TEnumeration[] GetEnumerations()
    {
        var enumerationType = typeof(TEnumeration);

        return enumerationType
              .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
              .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
              .Select(info => info.GetValue(null))
              .Cast<TEnumeration>()
              .ToArray();
    }

    public override bool Equals(object obj) => Equals(obj as TEnumeration);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Enumeration<TEnumeration, TValue> left
                                 , Enumeration<TEnumeration, TValue> right) => Equals(left, right);

    public static bool operator !=(Enumeration<TEnumeration, TValue> left
                                 , Enumeration<TEnumeration, TValue> right) => !Equals(left, right);

    private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
    {
        result = GetAll().FirstOrDefault(predicate);

        return result != null;
    }

    public static bool TryParse(TValue value, out TEnumeration result) => TryParse(e => e.ValueEquals(value), out result);

    private bool ValueEquals(TValue value) => Value.Equals(value);
}