using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.InputValidations
{
    public class NeedThreeAttribute : DataTypeAttribute
    {
        public NeedThreeAttribute() : base(DataType.Text)
        {
            ErrorMessage = "必須輸入 3 個字";
        }

        public override bool IsValid(object value)
        {
            string s = (string)value;
            return s.Length >= 3;
        }
    }
}