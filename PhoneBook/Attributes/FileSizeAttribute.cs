﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Attributes
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                return (value as HttpPostedFileBase).ContentLength <= _maxSize;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0} kb", (_maxSize/1024));
        }
    }
}