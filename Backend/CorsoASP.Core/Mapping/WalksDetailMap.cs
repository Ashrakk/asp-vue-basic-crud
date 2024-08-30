using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Core.Mapping;

public static class WalksDetailMap
{
    public static Walks ViewToModel(WalksDetailView view)
    {
        var model = new Walks
        {
            ID = view.ID,
            Description = view.Description,
            LengthKm = view.LengthKm,
            Image = view.Image,
            Regions = new Regions
            {
                ID = view.Regions.ID,
                Code = view.Regions.Code,
                Name = view.Regions.Name,
                Image = view.Regions.Image
            },
            Difficulty = new Difficulty
            {
                ID = view.Difficulty.ID,
                Name = view.Difficulty.Name
            }
        };

        return model;
    }

    public static WalksDetailView ModelToView(Walks model)
    {
        var view = new WalksDetailView()
        {
            ID = model.ID,
            Description = model.Description,
            LengthKm = model.LengthKm,
            Image = model.Image,
            Regions = new RegionsView
            {
                ID = model.Regions.ID,
                Code = model.Regions.Code,
                Name = model.Regions.Name,
                Image = model.Regions.Image
            },
            Difficulty = new DifficultyView
            {
                ID = model.Difficulty.ID,
                Name = model.Difficulty.Name
            }
        };

        return view;  
    }

    public static IEnumerable<Walks> ViewToModel(IEnumerable<WalksDetailView> viewList)
    {
        foreach (var view in viewList)
        {
            yield return ViewToModel(view);
        }
    }
    
    public static IEnumerable<WalksDetailView> ModelToView(IEnumerable<Walks> modelList)
    {
        foreach (var model in modelList)
        {
            yield return ModelToView(model);
        }
    }
}