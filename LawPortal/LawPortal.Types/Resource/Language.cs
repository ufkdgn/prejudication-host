using LawPortal.Types.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LawPortal.Types.Resource
{
    public class Language : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
    }

    public static class LanguageExtension
    {
        public static GenericResponse<Language> SelectByLangCode(this OrdinalDbContext<Language> context, string langCode = Constants.DefaultLanguage)
        {
            var returnObject = new GenericResponse<Language>();
            var response = context.SelectByFilter(x => x.Code == langCode);
            if (response.Success)
            {
                returnObject.Value = response.Value.FirstOrDefault();
            }
            else
            {
                returnObject.Messages.AddRange(response.Messages);
            }

            return returnObject;
        }
    }
}
