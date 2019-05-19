using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Common
{
    public class FieldsTogetherAttribute : ValidationAttribute, IClientValidatable
    {
        public FieldsTogetherAttribute(params string[] propNames)
        {
            this.PopNames = propNames;
        }

        public string[] PopNames { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.PopNames.Select(validationContext.ObjectType.GetProperty);
            if (properties == null)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            //گرفتن مقدار آن ها
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();

            int totalLength = values.Sum(x => x.Length);
            //اگر هیچ کدام پر نشده ، اعتبارسنجی نکند
            if (totalLength == 0)
            {
                return null;
            }
            else
            {
                //اگر حتی یکی از پراپرتی ها مقداردهی شده باشه بررسی کنه که
                //همه پراپرتی های مورد نظر (در اینجا هر چهار فیلد شماره کارت) پر شده باشه

                //اگر پراپرتی ها دارای مقدار نبودند ، نامعتبر باشد
                if (values.Any())
                {
                    if (values.Count() < properties.Count())
                    {
                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                    }
                }
                return null;
                //return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //yield return new ModelClientValidationRequiredRule(ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()));
            string validationtype = metadata.PropertyName.ToLower();

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = validationtype
            };
            yield return rule;
        }
    }
    //public class FieldsTogetherAttribute : ValidationAttribute, IClientValidatable
    //{
    //    public FieldsTogetherAttribute(params string[] otherPropNames)
    //    {
    //        this.OtherPropNames = otherPropNames;
    //    }

    //    public string[] OtherPropNames { get; private set; }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        //گرفتن نام پراپرتی دیگر ، غیر از این پراپرتی
    //        var otherProperties = this.OtherPropNames.Select(validationContext.ObjectType.GetProperty);
    //        if (otherProperties == null)
    //        {
    //            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //        }
    //        //گرفتن مقدار آن ها
    //        var otherValues = otherProperties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();

    //        int totalLength = otherValues.Sum(x => x.Length) + Convert.ToString(value).Length;
    //        //اگر هیچ کدام پر نشده ، اعتبارسنجی نکند
    //        if (totalLength == 0)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            //اگر حتی یکی از پراپرتی ها مقداردهی شده باشه بررسی کنه که
    //            //همه پراپرتی های مورد نظر (در اینجا هر چهار فیلد شماره کارت) پر شده باشه

    //            //اگر این پراپرتی دارای مقدار نبود ، نامعتبر باشد
    //            if (Convert.ToString(value).Length < 0)
    //            {
    //                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //            }

    //            //اگر پراپرتی های دیگر دارای مقدار نبودند ، نامعتبر باشد
    //            if (otherValues.Any())
    //            {
    //                foreach (var v in otherValues)
    //                {
    //                    if (v.Length < 0)
    //                    {
    //                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //                    }
    //                }
    //            }
    //            //return null;
    //            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
    //        }
    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        //yield return new ModelClientValidationRequiredRule(ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()));
    //        string validationtype=metadata.PropertyName.ToLower();

    //        var rule = new ModelClientValidationRule
    //        {
    //            ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
    //            ValidationType = validationtype
    //        };
    //        yield return rule;
    //    }
    //}

}