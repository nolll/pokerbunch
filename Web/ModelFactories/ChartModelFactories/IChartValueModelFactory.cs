using System;
using Web.Models.ChartModels;

namespace Web.ModelFactories.ChartModelFactories
{
    public interface IChartValueModelFactory
    {
        ChartValueModel Create();
        ChartValueModel Create(string val);
        ChartValueModel Create(int? val);
        ChartValueModel Create(DateTime dateTime);
    }
}