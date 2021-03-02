﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshsJelliesAndJams.Library.svc
{
    public static class Validator
    {
        public static string StringValidator(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} cannot be null.");
            }

            return value.ToUpper();
        }

        public static string StateValidator(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"{nameof(value)} cannot be null.");
            }
            else if (value.Length != 2)
            {
                throw new ArgumentException("Please use your state abbrivation.");
            }

            return value.ToUpper();
        }

        public static string ZipcodeValidator(this string value)
        {
            if (value.Length != 5)
            {
                throw new ArgumentException("Invalid Zipcode.");
            }

            return value;
        }

        public static int OrderValidator(this int value)
        {
            if (value > 30)
            {
                throw new ArgumentException("Unreasonable amount of products ordered.");
            }

            return value;
        }
    }
}
