﻿using System.ComponentModel.DataAnnotations;

namespace SkillProfi.Domain.Attributes
{
    /// <summary>
    /// Атрибут, проверяющий имя на корректность.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckNameAttribute : ValidationAttribute
    {
        public CheckNameAttribute()
        {

        }

        public override bool IsValid(object? value)
        {
            if (value != null && GetCountWords(value?.ToString() ?? ""))
            {
                return true;
            }

            return false;
        }

        private bool GetCountWords(string name)
        {
            if (name.Split(' ').Length == 2 || name.Split(' ').Length == 3)
            {
                return true;
            }

            return false;
        }
    }
}
