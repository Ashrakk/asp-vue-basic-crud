using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Core.Mapping;

public static class RegionsMap
{
    public static Regions ViewToModel(RegionsView view)
    {
        var model = new Regions()
        {
            ID = view.ID,
            Name = view.Name,
            Code = view.Code,
            Image = view.Image
        };

        return model;
    }

    public static RegionsView ModelToView(Regions model)
    {
        var view = new RegionsView()
        {
            ID = model.ID,
            Name = model.Name,
            Code = model.Code,
            Image = model.Image,
        };

        return view;
    }

    public static IEnumerable<Regions> ViewToModel(IEnumerable<RegionsView> viewList)
    {
        foreach (var view in viewList)
        {
            yield return ViewToModel(view);
        }
    }
    
    public static IEnumerable<RegionsView> ModelToView(IEnumerable<Regions> modelList)
    {
        foreach (var model in modelList)
        {
            yield return ModelToView(model);
        }
    }
}