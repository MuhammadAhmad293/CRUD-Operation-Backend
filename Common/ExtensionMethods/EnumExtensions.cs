using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute? descriptionAttribute = value.GetType().GetField(value.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }

        public static string GetDisplayName(this Enum value)
        {
            DisplayAttribute? displayNameAttribute = value.GetType().GetField(value.ToString())?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayNameAttribute != null ? displayNameAttribute.Name : value.ToString();
        }
        public static string GetDisplayShortName(this Enum value)
        {
            DisplayAttribute? displayNameAttribute = value.GetType().GetField(value.ToString())?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            return displayNameAttribute != null ? displayNameAttribute.ShortName : value.ToString();
        }
    }
}
