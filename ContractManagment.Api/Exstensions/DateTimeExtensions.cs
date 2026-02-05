using System;
using System.Globalization;

namespace ContractManagment.Api.Exstensions;

public static class DateTimeExtensions
{
    private const string Format = "dd-MM-yyyy hh:mm tt";

    extension(DateTime dateTime)
    {
        public string ToCustomDateTime()
        {
            return dateTime.ToString(Format, CultureInfo.InvariantCulture);
        }
    }

    extension(DateTime? dateTime)
    {
        // Nullable DateTime
        public string ToCustomDateTime()
        {
            return dateTime.HasValue
                ? dateTime.Value.ToString(Format, CultureInfo.InvariantCulture)
                : string.Empty; // or null / "--" / "N/A"
        }
    }
}