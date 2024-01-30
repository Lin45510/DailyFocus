using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFocus.Resources.DataTemplateSelectors
{
    public class DataSelectorProperties
    {
        public string Type { get; set; }
    }

    public class NewDailyDataTemplateSeletor : DataTemplateSelector
    {
        public DataTemplate DataTemplateNew { get; set; }
        public DataTemplate DataTemplatePlanning { get; set; }
        public DataTemplate DataTemplateNoTime { get; set; }
        public DataTemplate DataTemplateBillsToPay { get; set; }
        public DataTemplate DataTemplateBillsToReceive { get; set; }
        public DataTemplate DataTemplateFinanceEntry { get; set; }
        public DataTemplate DataTemplateFinanceLost { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataSelectorProperties properties = item as DataSelectorProperties;
            DataTemplate dataTemplate = null;

            switch (properties.Type)
            {
                case "New":
                    dataTemplate = DataTemplateNew;
                    break;
                case "Planning":
                    dataTemplate = DataTemplatePlanning;
                    break;
                case "NoTime":
                    dataTemplate = DataTemplateNoTime;
                    break;
                case "BillsToPay":
                    dataTemplate = DataTemplateBillsToPay;
                    break;
                case "BillsToReceive":
                    dataTemplate = DataTemplateBillsToReceive;
                    break;
                case "FinanceEntry":
                    dataTemplate = DataTemplateFinanceEntry;
                    break;
                case "FinanceLoss":
                    dataTemplate = DataTemplateFinanceLost;
                    break;
            }
            return dataTemplate;
        }
    }
}
