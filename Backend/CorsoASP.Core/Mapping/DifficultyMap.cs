using CorsoASP.Core.Models;
using CorsoASP.Core.Views;

namespace CorsoASP.Core.Mapping;

public static class DifficultyMap
{
    public static Difficulty ViewToModel(DifficultyView view)
    {
        var model = new Difficulty()
        {
            ID = view.ID,
            Name = view.Name,
        };

        return model;
    }

    public static DifficultyView ModelToView(Difficulty model)
    {
        var view = new DifficultyView()
        {
            ID = model.ID,
            Name = model.Name,
        };

        return view;
    }

    public static IEnumerable<Difficulty> ViewToModel(IEnumerable<DifficultyView> viewList)
    {
        foreach (var view in viewList)
        {
            yield return ViewToModel(view);
        }
    }
    
    public static IEnumerable<DifficultyView> ModelToView(IEnumerable<Difficulty> modelList)
    {
        foreach (var model in modelList)
        {
            yield return ModelToView(model);
        }
    }
}