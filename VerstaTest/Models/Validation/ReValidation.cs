using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace VerstaTest.Models.Validation
{
	public interface IFieldRevalidator<T>
	{
		bool TryConvert(ModelStateEntry en, out T result);
        bool TryReValidateField<ModelT>(ModelStateDictionary ModelState, ModelT model, string fieldName,
           Action<ModelT, T> modelSetValueAction, ControllerBase controller) where ModelT : class;

    }
	public class DecimalReValidator : IFieldRevalidator<decimal>
	{
        decimal Cnvrt(string source)
        {
            try
            {
                return Convert.ToDecimal(source, CultureInfo.CurrentUICulture);
            }
            catch (FormatException)
            {
                return Convert.ToDecimal(source, CultureInfo.InvariantCulture);
            }
        }

        public bool TryConvert(ModelStateEntry en, out decimal result)
        {
            try
            {
                result = Cnvrt(en.AttemptedValue);
                return true;
            }
            catch (FormatException)
            {
                result = default;
                return false;
            }
            catch (OverflowException)
            {
                en.Errors.Add("The value was too big");
                result = default;
                return false;
            }
        }

        public bool TryReValidateField<T>(ModelStateDictionary ModelState, T model, string fieldName, 
            Action<T, decimal> modelSetValueAction, ControllerBase controller) where T: class
        {
            var pw = ModelState[fieldName];
            if (pw != null)
            {
                decimal newResult = 0;
                if (TryConvert(pw, out newResult))
                {
                    ModelState.ClearValidationState(fieldName);
                    modelSetValueAction(model, newResult);
                    return controller.TryValidateModel(model);
                }
            }
            return false;
        }
	}
}
