using CorsoASP.Core.Interfaces.IService;
using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Core.Mapping;

public static class WalksMap
{
    public static Walks ViewToModel(WalksView view)
    {
        var model = new Walks
        {
            ID = view.ID,
            Description = view.Description,
            LengthKm = view.LengthKm,
            Image = view.Image,
            RegionFK = view.RegionFK,
            DifficultyFK = view.DifficultyFK
        };

        return model;
    }

    public static WalksView ModelToView(Walks model)
    {
        var view = new WalksView
        {
            ID = model.ID,
            Description = model.Description,
            LengthKm = model.LengthKm,
            Image = model.Image,
            RegionFK = model.RegionFK,
            DifficultyFK = model.DifficultyFK
        };

        return view;  
    }

    public static IEnumerable<Walks> ViewToModel(IEnumerable<WalksView> viewList)
    {
        foreach (var view in viewList)
        {
            yield return ViewToModel(view);
        }
    }
    
    public static IEnumerable<WalksView> ModelToView(IEnumerable<Walks> modelList)
    {
        foreach (var model in modelList)
        {
            yield return ModelToView(model);
        }
    }
}