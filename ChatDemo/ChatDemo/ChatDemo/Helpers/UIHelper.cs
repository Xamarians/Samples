using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace ChatDemo.Helpers
{
    class UIHelper
    {
       const string RegexPhone = @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$";
        const string RegexEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static bool Validate(string fieldName, string value, bool required, int minLength, int maxLength, out string message)
        {
            message = null;
            if (required && string.IsNullOrWhiteSpace(value))
            {
                message = fieldName + " " + "is required";
                return false;
            }
            else if (maxLength > 0 && maxLength == minLength && !string.IsNullOrWhiteSpace(value) && value.Length != maxLength)
            {
                message = string.Format("{0} must be equal to {1} characters.", fieldName, maxLength);
                return false;
            }
            else if (maxLength > 0 && !string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
            {
                message = string.Format("{0} must be less or equal to {1} characters.", fieldName, maxLength);
                return false;
            }
            else if (minLength > 0 && !string.IsNullOrWhiteSpace(value) && value.Length < minLength)
            {
                message = string.Format("{0} must be greator or equal to {1} characters.", fieldName, minLength);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidatePhone(string fieldName, string phoneNumber, bool required, out string message)
        {
            phoneNumber = UIHelper.RemovePhoneNumberFormating(phoneNumber);
            message = null;
            if (required && string.IsNullOrWhiteSpace(phoneNumber))
            {
                message = fieldName + " " + "is required";
                return false;
            }
            if (!Regex.IsMatch(phoneNumber, RegexPhone, RegexOptions.IgnoreCase))
            {
                message = "Invalid" + " " + fieldName;
                return false;
            }
            return true;
        }

        public static string RemovePhoneNumberFormating(string phoneNumber)
        {
            return phoneNumber == null ? phoneNumber : phoneNumber.Replace("-", "");
        }
        public static bool IsValidEmail(string fieldName, string value, bool required, out string message)
        {
            return IsValidRegex(fieldName, value, required, RegexEmail, out message);
        }

        public static bool IsValidRegex(string fieldName, string value, bool required, string regex, out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(value))
            {
                if (!required)
                    return true;
                message = fieldName + " is required";
                return false;
            }
            if (!Regex.IsMatch(value, regex, RegexOptions.IgnoreCase))
            {
                message = string.Format("Invalid {0}.", fieldName);
                return false;
            }
            return true;
        }
    }
}
