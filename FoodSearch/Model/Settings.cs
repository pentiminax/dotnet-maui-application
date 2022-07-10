using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Model;

public class Settings
{
    public static bool FirstRun
    {
        get => Preferences.Get(nameof(FirstRun), true);
        set => Preferences.Set(nameof(FirstRun), value);
    }

}
