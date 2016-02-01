using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class LanguageHelper
    {
        public List<Language> GetLanguages()
        {
            List<Language> languages = new List<Language>
            {
                new Language{ID=1,Name="English",Code="en"},
                new Language{ID=2,Name="Chinese",Code="zh"}
            };

            return languages;
        }
    }
}