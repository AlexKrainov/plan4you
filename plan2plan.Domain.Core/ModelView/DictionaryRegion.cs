using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace plan2plan.Domain.Core.ModelView
{
    public class DictionaryRegion
    {
        /// <summary>
        /// Ключ по которому отображается на карте значение
        /// </summary>
        public string Key { get; set; }

        public int Value { get; set; }
        /// <summary>
        /// Возможные вариации поиска в базе, например МО и Московская область 
        /// </summary>
        [ScriptIgnore]
        public List<string> SearcOptions;
        public DictionaryRegion()
        {
            this.SearcOptions = new List<string>();
        }
    }
}
