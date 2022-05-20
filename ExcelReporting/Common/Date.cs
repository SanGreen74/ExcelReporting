using System.Globalization;

namespace ExcelReporting.Common
{
    public readonly struct Date : IEquatable<Date>, IComparable<Date>
    {
        private const string DefaultDateFormat = "dd.MM.yyyy";

        public Date(int dayNumber)
        {
            if (dayNumber < 0 || dayNumber > MaxValue.DayNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(dayNumber));
            }

            DayNumber = dayNumber;
        }

        public Date(int year, int month, int day)
            : this(new DateTime(year, month, day))
        {
        }

        public Date(DateTime value)
        {
            DayNumber = (int) (value.Ticks / TimeSpan.TicksPerDay);
        }

        public static Date MinValue { get; } = default;

        public static Date MaxValue { get; } = new Date(9999, 12, 31);

        public int DayNumber { get; }

        public int Year => ToDateTime().Year;

        public int Month => ToDateTime().Month;

        public int Day => ToDateTime().Day;

        public DayOfWeek DayOfWeek => ToDateTime().DayOfWeek;

        public static Date ParseFromAnyFormat(string value)
        {
            if (TryParse(value, out var date))
                return date;

            if (TryParseFromOADate(value, out date))
                return date;

            throw new ArgumentException($"Unexpected value: {value}", nameof(value));
        }
        
        public static bool TryParseFromOADate(string value, out Date date)
        {
            date = default;
            if (long.TryParse(value, out var parsedValue))
            {
                date = new Date(DateTime.FromOADate(parsedValue));
                return true;
            }

            return false;
        }
        
        public static Date Parse(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!TryParse(value, out var date))
            {
                throw new FormatException($"Date format should be '{DefaultDateFormat}', but was {value}");
            }

            return date;
        }

        public static bool TryParse(string value, out Date date)
        {
            if (!DateTime.TryParse(value, out var dateTime))
            {
                date = default;
                return false;
            }

            date = new Date(dateTime);
            return true;
        }

        public DateTime ToDateTime(DateTimeKind kind = DateTimeKind.Unspecified)
        {
            return new DateTime(DayNumber * TimeSpan.TicksPerDay, kind);
        }

        public static explicit operator Date(DateTime dateTime) => new Date(dateTime);

        public static explicit operator Date(DateTimeOffset dateTimeOffset) => new Date(dateTimeOffset.UtcDateTime);

        public static bool operator <=(Date left, Date right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Date left, Date right) => left.CompareTo(right) >= 0;

        public static bool operator ==(Date left, Date right) => left.DayNumber == right.DayNumber;

        public static bool operator !=(Date left, Date right) => left.DayNumber != right.DayNumber;

        public int CompareTo(Date other) => DayNumber.CompareTo(other.DayNumber);

        public override int GetHashCode() => DayNumber.GetHashCode();

        public bool Equals(Date other) => DayNumber == other.DayNumber;

        public override bool Equals(object? obj) => obj is Date other && Equals(other);

        public override string ToString() => ToDateTime().ToString(DefaultDateFormat);
    }
}