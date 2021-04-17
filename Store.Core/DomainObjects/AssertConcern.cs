using System.Text.RegularExpressions;

namespace Store.Core.DomainObjects
{
    public class AssertConcern : DomainException
    {
        public static void Equals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public static void NotEquals(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(message);
            }
        }

        public  static void CharMinAndMax(string value, int min, int max, string message)
        {
            var lenght = value.Length;

            if (lenght < min || lenght > max)
            {
                throw new DomainException(message);
            }
        }

        public static void CharMax(string value, int max, string message)
        {
            var lenght = value.Length;

            if (lenght > max)
            {
                throw new DomainException(message);
            }
        }

        public static void Expression(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(message);
            }
        }

        public static void Empty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(message);
            }
        }

        public static void Null(string value, string message)
        {
            if (value is null)
            {
                throw new DomainException(message);
            }
        }

        public static void MinAndMax(int value, int min, int max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void MinAndMax(double value, double min, double max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void MinAndMax(float value, float min, float max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void MinAndMax(long value, long min, long max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }
        public static void MinAndMax(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallThatlMin(int value, int min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallThatlMin(double value, double min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallThatlMin(long value, long min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallThatlMin(decimal value, decimal min, string message)
        {
            if (value < min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallEquallMin(int value, int min, string message)
        {
            if (value <= min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallEquallMin(double value, double min, string message)
        {
            if (value <= min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallEquallMin(long value, long min, string message)
        {
            if (value <= min)
            {
                throw new DomainException(message);
            }
        }

        public static void SmallEquallMin(decimal value, decimal min, string message)
        {
            if (value <= min)
            {
                throw new DomainException(message);
            }
        }
        

        public static void False(bool value, string message)
        {
            if (value)
            {
                throw new DomainException(message);
            }
        }

        public static void True(bool value, string message)
        {
            if (!value)
            {
                throw new DomainException(message);
            }
        }
    }
}